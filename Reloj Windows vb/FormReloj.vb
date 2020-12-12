'-----------------------------------------------------------------------------
' Reloj para Windows                                               (02/Dic/20)
'
' Código convertido de C# a Visual Basic con CSharpToVB de Paul1956
'
' (c) Guillermo (elGuille) Som, 2020
'-----------------------------------------------------------------------------
Option Compare Text
Option Explicit On
Option Infer On
Option Strict On

Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms

Namespace Reloj_Windows
    Partial Public Class FormReloj
        Inherits Form

        Private ReadOnly ensamblado As System.Reflection.Assembly
        Private ReadOnly fvi As FileVersionInfo

        ''' <summary>
        ''' La información de este programa
        ''' </summary>
        Private ReadOnly InfoApp As String

        ''' <summary>
        ''' Para acceder a la propiedades del programa 
        ''' </summary>
        Private ReadOnly MySettings As Properties.Settings = Properties.Settings.[Default]

        ''' <summary>
        ''' Fecha desde que está activo el programa 
        ''' </summary>
        Private ReadOnly ActivoDesde As DateTime

        ''' <summary>
        ''' True para no ejecutar el código de los eventos
        ''' </summary>
        Private inicializando As Boolean

        ''' <summary>
        ''' Los valores para la configuración que necesitan variables aparte
        ''' Si es nulo no acoplar, 
        ''' si es true acoplar a la derecha, si es false acoplar a la izquierda
        ''' </summary>
        Private acoplarDonde As Boolean?

        ''' <summary>
        ''' Asignar true si se va a cambiar el tamaño después de estar acoplada.
        ''' </summary>
        Private estabaAcoplada As Boolean

        ''' <summary>
        ''' El tamaño y posición de la ventana
        ''' </summary>
        Private tamVentana As (Left As Integer, Top As Integer, Width As Integer, Height As Integer)

        ''' <summary>
        ''' Para la posición anterior a acoplar
        ''' asignarlo solo si no está acoplado,
        ''' por ejemplo, si estaba acoplado a la derecha y
        ''' se cambia a la izquierda, no asignar la posición 
        ''' </summary>
        Private tamAnt As (Left As Integer, Top As Integer, Width As Integer, Height As Integer)

        ' Los textos informativos según se muestre la ventana más ancha o más estrecha
        Private textoInfo As (Largo As String, Corto As String)

        ' Para el salvapantallas
        Private posLeft As Integer = -1
        Private posTop As Integer = -1
        Private ReadOnly aleatorio As New Random

        ''' <summary>
        ''' para cuando está el salvapantallas activo dejar siempre encima
        ''' </summary>
        Private topMostAnt As Boolean

        ''' <summary>
        ''' Cuando está el savapantallas, ponerlo más transparente 
        ''' esta variable recuerda el valor que tenía antes de iniciar el salvapantallas
        ''' </summary>
        Private opacidadAnt As Double

        ''' <summary>
        ''' Si está en modo salva pantalla
        ''' </summary>
        Private salvaPantallaActivo As Boolean

        Public Sub New()
            inicializando = True

            InitializeComponent()

            ActivoDesde = DateTime.Now

            ensamblado = System.Reflection.Assembly.GetExecutingAssembly()
            fvi = FileVersionInfo.GetVersionInfo(ensamblado.Location)
            InfoApp = $"{Application.ProductName} v{Application.ProductVersion} ({fvi.FileVersion})"

            ' Leer los valores de la configuración

            If MySettings.AcoplarDonde = -1 Then
                acoplarDonde = Nothing
            ElseIf MySettings.AcoplarDonde = 0 Then
                acoplarDonde = False
            ElseIf MySettings.AcoplarDonde = 1 Then
                acoplarDonde = True
            End If

            tamVentana = (MySettings.vLeft, MySettings.vTop, MySettings.vWidth, MySettings.vHeight)

            ' Por si modifica manualmente el valor de la opacidad al acoplar y en salvapantallas)
            If MySettings.OpacidadAcopleySalvaP < 0.5 Then
                MySettings.OpacidadAcopleySalvaP = 0.75
            End If
        End Sub

        Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            labelHora.Text = ActivoDesde.ToString("HH:mm:ss")
            labelFecha.Text = ActivoDesde.ToString("dddd, dd MMMM yyyy")

            ' Para el icono del área de notificación
            NotifyIcon1.Text = Application.ProductName
            NotifyMenuRestaurar.Text = "Minimizar"
            NotifyIcon1.Icon = Me.Icon
            NotifyIcon1.Visible = True
            Me.ShowInTaskbar = False

            Dim s As String = ""
            If ActivoDesde.Year > 2020 Then
                s = $"-{ActivoDesde.Year}"
            End If

            textoInfo.Corto = $"{InfoApp}"
            textoInfo.Largo = $"{textoInfo.Corto} - (c)Guillermo Som (elGuille), 2020{s}"

            LabelInfo.Text = textoInfo.Largo

            ' Actualizar el salvapantalla cada 3 segundos
            timerSalvaPantalla.Interval = 3000
            timerSalvaPantalla.Enabled = False

            ' Actualizar la fecha y hora cada 0.8 segundos
            timerActualizarFechaHora.Interval = 800
            timerActualizarFechaHora.Enabled = True

            ' Asignar el valor antes de inicializar
            ' por si se ha acoplado
            tamAnt = (Me.Left, Me.Top, Me.Width, Me.Height)

            Inicializar()

            inicializando = False
        End Sub

        Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
            ' Guardar los valores de la configuración

            ' mySettings.AcoplarDonde es un valor int -1 es null, 0 es false y 1 es true
            MySettings.AcoplarDonde = If(acoplarDonde Is Nothing, -1, If(acoplarDonde.Value, 1, 0))
            MySettings.vLeft = tamVentana.Left
            MySettings.vTop = tamVentana.Top
            MySettings.vWidth = tamVentana.Width
            MySettings.vHeight = tamVentana.Height

            MySettings.Save()

            Try
                NotifyIcon1.Visible = False
            Catch
            End Try

        End Sub

        Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles MyBase.KeyUp
            ' Ctrl+Shit+P inicia o cancela el salvapantalla
            If Not e.Alt AndAlso e.Shift AndAlso e.Control AndAlso e.KeyCode = Keys.P Then
                'CancelarSalvapantalla()
                MnuSalvaPantalla_Click(Nothing, Nothing)
                e.Handled = True
                e.SuppressKeyPress = True
            End If
        End Sub

        Private Sub Form1_Move(sender As Object, e As EventArgs) Handles MyBase.Move
            ' Solo guardar la posición y tamaño si está en modo normal
            ' no está como salvapantallas ni está acoplado
            If Me.WindowState = FormWindowState.Normal AndAlso Not salvaPantallaActivo AndAlso
                        acoplarDonde Is Nothing AndAlso Not mnuTamañoPequeño.Checked Then
                tamVentana = (Me.Left, Me.Top, Me.Width, Me.Height)
            End If
        End Sub

        Private Sub Form1_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
            If inicializando Then Return

            If Me.WindowState = FormWindowState.Normal Then
                NotifyMenuRestaurar.Text = "Minimizar"
            Else
                NotifyMenuRestaurar.Text = "Restaurar"
            End If

            ' Solo guardar la posición y tamaño si está en modo normal
            ' no está como salvapantallas ni está acoplado
            If Me.WindowState = FormWindowState.Normal AndAlso Not salvaPantallaActivo AndAlso
                        acoplarDonde Is Nothing AndAlso Not mnuTamañoPequeño.Checked Then
                tamVentana = (Me.Left, Me.Top, Me.Width, Me.Height)
            End If

            AjustarTamañoFechaHora()

            btnSplitDrop.ToolTipText = $"{Me.Location} {Me.Size}".Replace("X", "L").Replace("Y", "T")
        End Sub

        '
        ' Los métodos normales
        '

        ''' <summary>
        ''' Iniciar o detener el salvapantalla.
        ''' Si al detenerlo se quiere cerrar la ventana, indicar true en cerrar.
        ''' </summary>
        ''' <param name="mostrar">True para iniciar el salvapantalla, false para detenerlo.</param>
        ''' <param name="cerrar">Si al detenerlo se quiere cerrar la ventana, indicar true en cerrar. 
        ''' No se tiene en cuenta si mostrar es true.
        ''' </param>
        ''' <remarks>Método público para usarlo desde otra aplicaciones.</remarks>
        Public Sub Salvapantalla(mostrar As Boolean, cerrar As Boolean)
            MySettings.UsarComoSalvaPantalla = mostrar
            MySettings.OpacidadAcopleySalvaP = 0.75

            MostrarSalvapantalla()

            If Not mostrar AndAlso cerrar Then Me.Close()
        End Sub

        ''' <summary>
        ''' Iniciar o detener el salvapantalla
        ''' </summary>
        Private Sub MostrarSalvapantalla()
            If inicializando Then Return

            mnuSalvapantalla.Checked = Not mnuSalvapantalla.Checked
            MySettings.UsarComoSalvaPantalla = mnuSalvapantalla.Checked

            If MySettings.UsarComoSalvaPantalla Then
                timerSalvaPantalla.Enabled = MySettings.UsarComoSalvaPantalla

                ' Guardar la posición y tamaño
                tamVentana = (Me.Left, Me.Top, Me.Width, Me.Height)

                ' Quitar el borde
                Me.FormBorderStyle = FormBorderStyle.None

                topMostAnt = Me.TopMost
                Me.TopMost = True

                opacidadAnt = MySettings.Opacidad
                If MySettings.OpacidadAcopleySalvaP < 0.5 Then
                    MySettings.OpacidadAcopleySalvaP = 0.75
                End If
                Me.Opacity = MySettings.OpacidadAcopleySalvaP

                salvaPantallaActivo = True
            Else
                CancelarSalvapantalla()
            End If
        End Sub

        ''' <summary>
        ''' Ajustar el tamaño de la fecha/hora y el contenido de la fecha
        ''' </summary>
        Private Sub AjustarTamañoFechaHora()
            Dim d As System.DateTime = DateTime.Now

            ' TODO
            ' Ajustar el tamaño de la fuente del reloj si es ancho, pero menos alto

            ' El tamaño mínimo de la ventana es: W=350, H = 180

            If Height > 190 Then
                labelFecha.Font = New Font(labelFecha.Font.FontFamily, 37, FontStyle.Bold)
                labelFecha.Height = 55
                labelHora.Height = Me.ClientSize.Height - labelFecha.Height - statusStrip1.Height - 60
            End If

            If Width < 400 Then
                'If acoplarDonde IsNot Nothing Then
                '    LabelInfo.Text = textoInfo.Corto
                'End If
                LabelInfo.Text = ""
                Me.ControlBox = False
            Else
                LabelInfo.Text = textoInfo.Largo
                Me.ControlBox = True
            End If

            Dim fs As Single = CSng((Width / 7)) ' 6.7
            labelHora.Font = New Font(labelHora.Font.FontFamily, fs, FontStyle.Bold)

            If Width < 500 Then
                labelFecha.Text = d.ToString("dd.MMMyyyy")
            ElseIf Width < 650 Then
                labelFecha.Text = d.ToString("ddd, dd.MMMyyyy")
            ElseIf Width < 750 Then
                labelFecha.Text = d.ToString("dddd, dd.MMMyyyy")
            Else
                labelFecha.Text = d.ToString("dddd, dd MMMM yyyy")
            End If
        End Sub

        ''' <summary>
        ''' Acoplar la ventana a la derecha o izquierda o centrarla.
        ''' </summary>
        ''' <param name="donde">Un valor de tipo Boolean? 
        ''' true acopla a la derecha, false acopla a la izquierda, 
        ''' nulo la pone donde estaba antes de acoplar.
        ''' </param>
        Private Sub AcoplarVentana(donde As Boolean?)
            inicializando = True

            If acoplarDonde Is Nothing Then
                tamAnt = (Left, Top, Width, Height)
            End If

            If donde Is Nothing Then
                ' Dejarla en la posición normal
                ' no acoplar
                mnuAcoplarDer.Checked = False
                mnuAcoplarIzq.Checked = False

                ' Restaurar la posición y tamaño guardados
                If Not (tamAnt.Left < 0 AndAlso tamAnt.Top < 0 AndAlso
                            tamAnt.Width < 0 AndAlso tamAnt.Height < 0) Then
                    ' Debe entrar en el evento Form1_Resize
                    ' para que se cambie el tamaño de la fecha/hora
                    inicializando = False
                    Me.Left = tamAnt.Left
                    Me.Top = tamAnt.Top
                    Me.Width = tamAnt.Width
                    Me.Height = tamAnt.Height
                    inicializando = True
                End If
                Me.FormBorderStyle = FormBorderStyle.Sizable
                Me.Opacity = MySettings.Opacidad
                estabaAcoplada = False
            Else
                Me.FormBorderStyle = FormBorderStyle.None
                estabaAcoplada = True

                If MySettings.AcoplarTransparente Then
                    Me.Opacity = MySettings.OpacidadAcopleySalvaP
                End If

                If donde.Value Then
                    PonerTamañoAcoplar(MySettings.AcoplarMinimo)
                    ' Posicionar la ventana arriba a la derecha
                    Me.Left = Screen.PrimaryScreen.WorkingArea.Width - Width
                    Me.Top = 0
                    mnuAcoplarDer.Checked = True
                    mnuAcoplarIzq.Checked = False
                ElseIf Not donde.Value Then
                    PonerTamañoAcoplar(MySettings.AcoplarMinimo)
                    ' Posicionar la ventana arriba a la izquierda
                    Me.Left = 0
                    Me.Top = 0
                    mnuAcoplarIzq.Checked = True
                    mnuAcoplarDer.Checked = False
                End If
            End If

            acoplarDonde = donde

            inicializando = False
        End Sub

        ''' <summary>
        ''' Asignar el tamaño de acoplar a la ventana
        ''' </summary>
        Private Sub PonerTamañoAcoplar(acoplarMinimo As Boolean)
            If acoplarMinimo Then
                Dim tmp As Boolean = inicializando
                Dim tamTmp = tamAnt

                ' interesa que entre en Form1_Resize
                inicializando = False
                Me.Width = Me.MinimumSize.Width
                Me.Height = Me.MinimumSize.Height

                tamAnt = tamTmp
                inicializando = tmp

                labelFecha.Font = New Font(labelFecha.Font.FontFamily, 24, FontStyle.Bold)
                labelFecha.Height = 35
                labelFecha.Text = DateTime.Now.ToString("ddd, dd.MMMyyyy")
                labelHora.Height = Me.ClientSize.Height - labelFecha.Height - statusStrip1.Height - 20
            End If
        End Sub

        ''' <summary>
        ''' Cancelar el salvapantalla.
        ''' </summary>
        Private Sub CancelarSalvapantalla()
            timerSalvaPantalla.Enabled = False

            mnuSalvapantalla.Checked = False
            Me.TopMost = topMostAnt
            Me.FormBorderStyle = FormBorderStyle.Sizable
            Me.Opacity = opacidadAnt

            ' Posicionarlo donde estaba
            Me.Left = tamVentana.Left
            Me.Top = tamVentana.Top
            Me.Width = tamVentana.Width
            Me.Height = tamVentana.Height

            salvaPantallaActivo = False

            mnuTamañoPequeño.Checked = False
        End Sub

        ''' <summary>
        ''' Inicializar con los valores de la configuración
        ''' </summary>
        Private Sub Inicializar()
            ' Valores predeterminados por si los cambio en diseño
            Me.FormBorderStyle = FormBorderStyle.Sizable
            Me.Opacity = 1
            opacidadAnt = 1

            salvaPantallaActivo = False

            ' Posicionar la ventana solo si los valores no son menores de cero
            ' esto habrá que arreglarlo si se usa en un monitor externo
            If Not (tamVentana.Left < 0 AndAlso tamVentana.Top < 0 AndAlso
                        tamVentana.Width < 0 AndAlso tamVentana.Height < 0) Then
                Me.Left = tamVentana.Left
                Me.Top = tamVentana.Top
                Me.Width = tamVentana.Width
                Me.Height = tamVentana.Height
            End If

            Me.TopMost = MySettings.SiempreEncima

            ' No poner una opacidad menor de 20
            If MySettings.Opacidad < 20 Then
                MySettings.Opacidad = 20
            End If

            Me.Opacity = MySettings.Opacidad / 100
            cboOpacidad.Text = MySettings.Opacidad.ToString()


            ' Solo asignarlo si no está acoplada
            If acoplarDonde Is Nothing Then
                tamAnt = (Left, Top, Width, Height)
            End If

            mnuAcoplarDer.Checked = MySettings.AcoplarDonde = 1
            mnuAcoplarIzq.Checked = MySettings.AcoplarDonde = 0
            mnuAcoplarMinimo.Checked = MySettings.AcoplarMinimo
            mnuRecordarPosicion.Checked = MySettings.RecordarPos
            mnuTopMost.Checked = MySettings.SiempreEncima
            mnuAcoplarTransparente.Checked = MySettings.AcoplarTransparente

            AcoplarVentana(acoplarDonde)

            LabelInfo.ToolTipText = $"Activo desde {ActivoDesde:HH:mm:ss dd.MMMyyyy}"
            btnSplitDrop.ToolTipText = $"{Me.Location} {Me.Size}".Replace("X", "L").Replace("Y", "T")
        End Sub

        '
        ' Los temporizadores
        '

        Private Sub TimerActualizarFechaHora_Tick(sender As Object, e As EventArgs) Handles timerActualizarFechaHora.Tick
            labelHora.Text = DateTime.Now.ToString("HH:mm:ss")
        End Sub

        ''' <summary>
        ''' Este temporizador actúa como un salvapantallas
        ''' </summary>
        Private Sub TimerSalvaPantalla_Tick(sender As Object, e As EventArgs) Handles timerSalvaPantalla.Tick
            If aleatorio.[Next](0, 2) = 0 Then
                posLeft = -1
            Else
                posTop = 1
            End If

            If aleatorio.[Next](0, 2) = 0 Then
                posTop = -1
            Else
                posTop = 1
            End If

            Me.Left += 90 * posLeft
            Me.Top += 90 * posTop

            If Me.Left > Screen.PrimaryScreen.WorkingArea.Width - Me.Width - 120 Then
                Me.Left = Screen.PrimaryScreen.WorkingArea.Width - Me.Width - 120
                posLeft = -1
            End If
            If Me.Left < 0 Then
                Me.Left = 0
                posLeft = 1
            End If

            If Me.Top > Screen.PrimaryScreen.WorkingArea.Height - Me.Height - 30 Then
                Me.Top = Screen.PrimaryScreen.WorkingArea.Height - Me.Height - 30
                posTop = -1
            End If
            If Me.Top < 0 Then
                Me.Top = 0
                posTop = 1
            End If
        End Sub

        '
        ' Para mover el formulario pulsando en los controles que están encima
        ' el código que suelo usar en mis aplicaciones (normalmente en las ventanas AcercaDe)
        ' adaptado de VB a C#
        '

        ' Si se muestra la barra de títulos, no moverlo desde los controles
        Private pX As Integer
        Private pY As Integer
        Private ratonPulsado As Boolean

        Private Sub MoverForm_MouseDown(sender As Object, e As MouseEventArgs) Handles labelHora.MouseDown, labelFecha.MouseDown, statusStrip1.MouseDown, panelCentro.MouseDown
            If e.Button = MouseButtons.Left Then
                ratonPulsado = True
                pX = e.X
                pY = e.Y
            End If
        End Sub

        Private Sub MoverForm_MouseMove(sender As Object, e As MouseEventArgs) Handles labelHora.MouseMove, labelFecha.MouseMove, statusStrip1.MouseMove, panelCentro.MouseMove
            If ratonPulsado Then
                Me.Left += e.X - pX
                Me.Top += e.Y - pY
            End If
        End Sub

        Private Sub MoverForm_MouseUp(sender As Object, e As MouseEventArgs) Handles labelHora.MouseUp, labelFecha.MouseUp, statusStrip1.MouseUp, panelCentro.MouseUp
            ratonPulsado = False
        End Sub

        '
        ' Otros métodos de evento
        '

        ''' <summary>
        ''' Mientras se deja sin mover el cursor del ratón encima del control
        ''' </summary>
        Private Sub LabelInfo_MouseHover(sender As Object, e As EventArgs) Handles LabelInfo.MouseHover
            LabelInfo.ToolTipText = $"Activo desde {ActivoDesde:HH:mm:ss dd.MMMyyyy}"
        End Sub

        Private Sub MnuAcoplarDer_Click(sender As Object, e As EventArgs) Handles mnuAcoplarDer.Click
            If inicializando Then Return

            mnuAcoplarDer.Checked = Not mnuAcoplarDer.Checked
            ' Acoplar a la derecha
            If mnuAcoplarDer.Checked Then
                AcoplarVentana(True)
            Else
                AcoplarVentana(Nothing)
            End If
        End Sub

        Private Sub MnuAcoplarIzq_Click(sender As Object, e As EventArgs) Handles mnuAcoplarIzq.Click
            If inicializando Then Return

            mnuAcoplarIzq.Checked = Not mnuAcoplarIzq.Checked
            ' Acoplar arriba a la izquierda
            If mnuAcoplarIzq.Checked Then
                AcoplarVentana(False)
            Else
                AcoplarVentana(Nothing)
            End If
        End Sub

        Private Sub CboOpacidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboOpacidad.SelectedIndexChanged
            Dim transp As Double = Nothing
            Dim unused = Double.TryParse(cboOpacidad.SelectedItem.ToString(), transp)

            MySettings.Opacidad = transp

            If MySettings.Opacidad > 20 Then
                Me.Opacity = MySettings.Opacidad / 100
            Else
                MySettings.Opacidad = Me.Opacity
                inicializando = True
                cboOpacidad.Text = MySettings.Opacidad.ToString()
                inicializando = False
            End If
        End Sub

        Private Sub BtnSplitDrop_DropDownOpening(sender As Object, e As EventArgs) Handles btnSplitDrop.DropDownOpening
            ' Poner a tamaño pequeño o restaurar
            If mnuTamañoPequeño.Checked Then
                mnuTamañoPequeño.Text = "Cambiar al tamaño normal"
            Else
                mnuTamañoPequeño.Text = "Cambiar al tamaño a pequeño"
            End If
            mnuTamañoPequeño.Enabled = Not estabaAcoplada

            ' El salvapantalla
            If mnuSalvapantalla.Checked Then
                mnuSalvapantalla.Text = "Detener salvapantalla"
            Else
                mnuSalvapantalla.Text = "Iniciar salvapantalla"
            End If

        End Sub

        Private Sub MnuRecordarPosicion_Click(sender As Object, e As EventArgs) Handles mnuRecordarPosicion.Click
            If inicializando Then Return

            mnuRecordarPosicion.Checked = Not mnuRecordarPosicion.Checked
            MySettings.RecordarPos = mnuRecordarPosicion.Checked
        End Sub

        Private Sub MnuTopMost_Click(sender As Object, e As EventArgs) Handles mnuTopMost.Click
            If inicializando Then Return

            mnuTopMost.Checked = Not mnuTopMost.Checked

            Me.TopMost = mnuTopMost.Checked
            MySettings.SiempreEncima = Me.TopMost
        End Sub

        Private Sub MnuAcoplarMinimo_Click(sender As Object, e As EventArgs) Handles mnuAcoplarMinimo.Click
            If inicializando Then Return

            mnuAcoplarMinimo.Checked = Not mnuAcoplarMinimo.Checked
            MySettings.AcoplarMinimo = mnuAcoplarMinimo.Checked
            AcoplarVentana(acoplarDonde)
        End Sub

        Private Sub MnuCerrar_Click(sender As Object, e As EventArgs) Handles mnuCerrar.Click
            Me.Close()
        End Sub

        Private Sub MnuSalvaPantalla_Click(sender As Object, e As EventArgs) Handles mnuSalvapantalla.Click
            MostrarSalvapantalla()
        End Sub

        ''' <summary>
        ''' Desactivar el salva pantalla al hacer doble clic en la fec o la hora
        ''' </summary>
        Private Sub LabelFechaHora_DoubleClick(sender As Object, e As EventArgs) Handles labelHora.DoubleClick, labelFecha.DoubleClick, panelCentro.DoubleClick
            CancelarSalvapantalla()
        End Sub

        Private Sub MnuAcoplarTransparente_Click(sender As Object, e As EventArgs) Handles mnuAcoplarTransparente.Click
            If inicializando Then Return

            mnuAcoplarTransparente.Checked = Not mnuAcoplarTransparente.Checked
            MySettings.AcoplarTransparente = mnuAcoplarTransparente.Checked
            AcoplarVentana(acoplarDonde)
        End Sub

        Private Sub MnuTamañoPequeño_Click(sender As Object, e As EventArgs) Handles mnuTamañoPequeño.Click
            If inicializando Then Return

            mnuTamañoPequeño.Checked = Not mnuTamañoPequeño.Checked
            If estabaAcoplada Then Return

            If mnuTamañoPequeño.Checked Then
                PonerTamañoAcoplar(True)
            Else
                inicializando = True
                CancelarSalvapantalla()
                AjustarTamañoFechaHora()
                inicializando = False
            End If
        End Sub

        Private Sub mnuAcercaDe_Click(sender As Object, e As EventArgs) Handles mnuAcercaDe.Click
            Dim versionWeb = ""
            Dim msgVersion = $"{vbCrLf}{vbCrLf}Esta versión de '{Application.ProductName}' es la más reciente."

            Dim cualVersion = VersionUtilidades.CompararVersionWeb(ensamblado, versionWeb)

            If cualVersion = 1 Then
                msgVersion = $"{vbCrLf}{vbCrLf}En la web hay una versión más reciente: {versionWeb}"
            ElseIf cualVersion = -1 Then
                msgVersion = $"{vbCrLf}{vbCrLf}Esta versión de '{Application.ProductName}' es más reciente que la de la web: {versionWeb}"
            End If
            Dim titulo = $"Acerca de {InfoApp}"

            Dim msg = $"{titulo}

Esta aplicación es un 'simple' reloj que te muestra la fecha y la hora en una ventana.

Te permite controlar dónde verla, si a tamaño normal, a tamaño pequeño, incluso puedes cambiar el tamaño a tu gusto.

Puedes hacer que esté encima del resto de ventanas.

Además puedes hacerla menos opaca (más transparente) para que pueda verse lo que hay debajo de la ventana.
El nivel de transparencia lo puedes controlar a tu gusto, desde casi totalmente transparente a totalmente opaca.

Puedes usarla también como una especie de salva pantallas (se va desplazando por la pantalla), pero con transparencia con idea de que veas lo que hay debajo sin tener que detener la aplicación.
Si quieres dejar de ver cómo se mueve por la pantalla, simplemente haz doble-clic en la fecha o en la hora.
El salva pantallas lo puedes usar a tamaño normal o en tamaño pequeño.
Cuando funciona como salva pantalla siempre está encima del resto de las ventanas y no muestra la barra de título.

La ventana la puedes mover desde la barra de título o bien manteniendo el ratón pulsado en la fecha o en la hora.

También puedes acoplarla en la parte superior izquierda o derecha, en esos casos, se cambia a tamaño mínimo y se hace transparente.

Y esto es casi todo... creo... en el menú de opciones (en el botón que hay abajo a la derecha) están todas las opciones de uso disponibles.

¡Disfrútala! :-)" & msgVersion

            MessageBox.Show(msg, titulo,
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub

        Private Sub NotifyMenuCerrar_Click(sender As Object, e As EventArgs) Handles NotifyMenuCerrar.Click
            Me.Close()
        End Sub

        Private Sub NotifyMenuRestaurar_Click(sender As Object, e As EventArgs) Handles NotifyMenuRestaurar.Click
            If Me.WindowState = FormWindowState.Normal Then
                NotifyMenuRestaurar.Text = "Restaurar"
                Me.WindowState = FormWindowState.Minimized
            Else
                NotifyMenuRestaurar.Text = "Minimizar"
                Me.WindowState = FormWindowState.Normal
            End If
        End Sub
    End Class
End Namespace
