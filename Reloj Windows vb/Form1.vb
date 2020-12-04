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
    Public Partial Class Form1
        Inherits Form
        ' 
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
        ' El tamaño y posición de la ventana
        Private tamVentana As (Left As Integer, Top As Integer, Width As Integer, Height As Integer)
        ' Para la posición anterior a acoplar
        ' asignarlo solo si no está acoplado,
        ' por ejemplo, si estaba acoplado a la derecha y
        ' se cambia a la izquierda, no asignar la posición
        Private tamAnt As (Left As Integer, Top As Integer, Width As Integer, Height As Integer)
        ' Los textos informativos según se muestre la ventana más ancha o más estrecha
        Private textoInfo As (Largo As String, Corto As String)
        ' Para el salva pantallas
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

            Me.ActivoDesde = DateTime.Now

            ' Leer los valores de la configuración

            If MySettings.AcoplarDonde = -1 Then
                Me.acoplarDonde = Nothing
            ElseIf MySettings.AcoplarDonde = 0 Then
                Me.acoplarDonde = False
            ElseIf MySettings.AcoplarDonde = 1 Then
                Me.acoplarDonde = True
            End If

            tamVentana = (MySettings.vLeft, MySettings.vTop, MySettings.vWidth, MySettings.vHeight)

            ' Por si modifica manualmente el valor de la opacidad al acoplar y en salvapantallas)
            If MySettings.OpacidadAcopleySalvaP < 0.5 Then
                MySettings.OpacidadAcopleySalvaP = 0.75
            End If
        End Sub
        Private Sub Form1_Load(sender As Object, e As EventArgs)
            labelHora.Text = ActivoDesde.ToString("HH:mm:ss")
            labelFecha.Text = ActivoDesde.ToString("dddd, dd MMMM yyyy")

            Dim s As String = ""
            If ActivoDesde.Year > 2020 Then
                s = $"-{ActivoDesde.Year}"
            End If

            textoInfo.Corto = $"{Application.ProductName} v{Application.ProductVersion}"
            textoInfo.Largo = $"{textoInfo.Corto} - (c)Guillermo Som (elGuille), 2000{s}"

            LabelInfo.Text = textoInfo.Largo

            ' Actualizar el salvapantalla cada 3 segundos
            timerSalvaPantalla.Interval = 3000
            timerSalvaPantalla.Enabled = False

            ' Actualizar la fecha y hora cada 0.8 segundos
            timerActualizarFechaHora.Interval = 800
            timerActualizarFechaHora.Enabled = True

            ' Asignar el valor antes de inicializar
            ' por si se ha acoplado
            Me.tamAnt = (Me.Left, Me.Top, Me.Width, Me.Height)

            Inicializar()

            Me.inicializando = False
        End Sub
        Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs)
            ' Guardar los valores de la configuración

            ' mySettings.AcoplarDonde es un valor int -1 es null, 0 es false y 1 es true
            MySettings.AcoplarDonde = If(acoplarDonde Is Nothing, -1, If(acoplarDonde.Value, 1, 0))
            MySettings.vLeft = tamVentana.Left
            MySettings.vTop = tamVentana.Top
            MySettings.vWidth = tamVentana.Width
            MySettings.vHeight = tamVentana.Height

            MySettings.Save()
        End Sub
        Private Sub Form1_Move(sender As Object, e As EventArgs)
            ' Solo guardar la posición y tamaño si está en modo normal
            ' no está como salvapantallas ni está acoplado
            If Me.WindowState = FormWindowState.Normal AndAlso Not salvaPantallaActivo AndAlso acoplarDonde Is Nothing Then
                tamVentana = (Me.Left, Me.Top, Me.Width, Me.Height)
            End If
        End Sub
        Private Sub Form1_Resize(sender As Object, e As EventArgs)
            If inicializando Then
                Return
            End If

            'var d = DateTime.Now;

            ' Solo guardar la posición y tamaño si está en modo normal
            ' no está como salvapantallas ni está acoplado
            If Me.WindowState = FormWindowState.Normal AndAlso Not salvaPantallaActivo AndAlso _
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
        ''' Ajustar el tamaño de la fecha/hora y el contenido de la fecha
        ''' </summary>
        ''' <param name="d"></param>
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
                LabelInfo.Text = textoInfo.Corto
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
            Me.inicializando = True

            If acoplarDonde Is Nothing Then
                Me.tamAnt = (Left, Top, Width, Height)
            End If

            If donde Is Nothing Then
                ' Dejarla en la posición normal
                ' no acoplar
                mnuAcoplarDer.Checked = False
                mnuAcoplarIzq.Checked = False

                ' Restaurar la posición y tamaño guardados
                If Not (tamAnt.Left < 0 AndAlso tamAnt.Top < 0 AndAlso _
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
            Else
                Me.FormBorderStyle = FormBorderStyle.None

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

            Me.acoplarDonde = donde

            Me.inicializando = False
        End Sub
        ''' <summary>
        ''' Asignar el tamaño de acoplar a la ventana
        ''' </summary>
        Private Sub PonerTamañoAcoplar(acoplarMinimo1 As Boolean)
            If acoplarMinimo1 Then
                Dim tmp As Boolean = inicializando
                Dim tamTmp As (Left As Integer, Top As Integer, Width As Integer, eight As Integer) = tamAnt

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
            If Not (tamVentana.Left < 0 AndAlso tamVentana.Top < 0 AndAlso _
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
                Me.tamAnt = (Left, Top, Width, Height)
            End If

            mnuAcoplarDer.Checked = MySettings.AcoplarDonde = 1
            mnuAcoplarIzq.Checked = MySettings.AcoplarDonde = 0
            mnuAcoplarMinimo.Checked = MySettings.AcoplarMinimo
            mnuRecordarPosicion.Checked = MySettings.RecordarPos
            'mnuSalvapantalla.Checked =
            mnuTopMost.Checked = MySettings.SiempreEncima
            mnuAcoplarTransparente.Checked = MySettings.AcoplarTransparente

            AcoplarVentana(acoplarDonde)

            LabelInfo.ToolTipText = $"Activo desde {ActivoDesde:HH:mm:ss dd.MMMyyyy}"
            btnSplitDrop.ToolTipText = $"{Me.Location} {Me.Size}".Replace("X", "L").Replace("Y", "T")
        End Sub
        '
        ' Los temporizadores
        '
        Private Sub TimerActualizarFechaHora_Tick(sender As Object, e As EventArgs)
            labelHora.Text = DateTime.Now.ToString("HH:mm:ss")
        End Sub
        ''' <summary>
        ''' Este temporizador actúa como un salvapantallas
        ''' </summary>
        Private Sub TimerSalvaPantalla_Tick(sender As Object, e As EventArgs)
            If aleatorio.[Next](0, 2) = 0 Then
                posLeft = -1
            Else posTop = 1
            End If

            If aleatorio.[Next](0, 2) = 0 Then
                posTop = -1
            Else posTop = 1
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
        Private Sub MoverForm_MouseDown(sender As Object, e As MouseEventArgs)
            'if (this.ControlBox) return;

            If e.Button = MouseButtons.Left Then
                Me.ratonPulsado = True
                Me.pX = e.X
                Me.pY = e.Y
            End If
        End Sub
        Private Sub MoverForm_MouseMove(sender As Object, e As MouseEventArgs)
            'if (this.ControlBox) return;

            If ratonPulsado Then
                Me.Left += e.X - pX
                Me.Top += e.Y - pY
            End If
        End Sub
        Private Sub MoverForm_MouseUp(sender As Object, e As MouseEventArgs)
            Me.ratonPulsado = False
        End Sub
        '
        ' Otros métodos de evento
        '
        ''' <summary>
        ''' Mientras se deja sin mover el cursor del ratón encima del control
        ''' </summary>
        Private Sub LabelInfo_MouseHover(sender As Object, e As EventArgs)
            LabelInfo.ToolTipText = $"Activo desde {ActivoDesde:HH:mm:ss dd.MMMyyyy}"
        End Sub
        Private Sub MnuAcoplarDer_Click(sender As Object, e As EventArgs)
            If inicializando Then
                Return
            End If

            mnuAcoplarDer.Checked = Not mnuAcoplarDer.Checked
            ' Acoplar a la derecha
            If mnuAcoplarDer.Checked Then
                AcoplarVentana(True)
            Else AcoplarVentana(Nothing)
            End If
        End Sub
        Private Sub MnuAcoplarIzq_Click(sender As Object, e As EventArgs)
            If inicializando Then
                Return
            End If

            mnuAcoplarIzq.Checked = Not mnuAcoplarIzq.Checked
            ' Acoplar arriba a la izquierda
            If mnuAcoplarIzq.Checked Then
                AcoplarVentana(False)
            Else AcoplarVentana(Nothing)
            End If
        End Sub
        Private Sub CboOpacidad_SelectedIndexChanged(sender As Object, e As EventArgs)
            Dim transp As Double = Nothing
            Double.TryParse(cboOpacidad.SelectedItem.ToString(), transp)

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
        Private Sub BtnSplitDrop_DropDownOpening(sender As Object, e As EventArgs)
            ''' Si está el control box del formulario, ocultar el menú cerrar
            ''' y viceversa
            'toolSeparatorCerrar.Visible = !this.ControlBox;
            'mnuCerrar.Visible = !this.ControlBox;
            If mnuTamañoPequeño.Checked Then
                mnuTamañoPequeño.Text = "Cambiar al tamaño normal"
            Else mnuTamañoPequeño.Text = "Cambiar al tamaño a pequeño"
            End If
        End Sub
        Private Sub MnuRecordarPosicion_Click(sender As Object, e As EventArgs)
            If inicializando Then
                Return
            End If

            mnuRecordarPosicion.Checked = Not mnuRecordarPosicion.Checked
            MySettings.RecordarPos = mnuRecordarPosicion.Checked
        End Sub
        Private Sub MnuTopMost_Click(sender As Object, e As EventArgs)
            If inicializando Then
                Return
            End If

            mnuTopMost.Checked = Not mnuTopMost.Checked

            Me.TopMost = mnuTopMost.Checked
            MySettings.SiempreEncima = Me.TopMost
        End Sub
        Private Sub MnuAcoplarMinimo_Click(sender As Object, e As EventArgs)
            If inicializando Then
                Return
            End If

            mnuAcoplarMinimo.Checked = Not mnuAcoplarMinimo.Checked
            MySettings.AcoplarMinimo = mnuAcoplarMinimo.Checked
            AcoplarVentana(acoplarDonde)
        End Sub
        Private Sub MnuCerrar_Click(sender As Object, e As EventArgs)
            Me.Close()
        End Sub
        Private Sub MnuSalvaPantalla_Click(sender As Object, e As EventArgs)
            If inicializando Then
                Return
            End If

            ' Guardar la posición y tamaño
            tamVentana = (Me.Left, Me.Top, Me.Width, Me.Height)

            mnuSalvapantalla.Checked = Not mnuSalvapantalla.Checked
            MySettings.UsarComoSalvaPantalla = mnuSalvapantalla.Checked

            timerSalvaPantalla.Enabled = MySettings.UsarComoSalvaPantalla

            If MySettings.UsarComoSalvaPantalla Then
                ' Quitar el borde
                Me.FormBorderStyle = FormBorderStyle.None

                topMostAnt = Me.TopMost
                Me.TopMost = True

                opacidadAnt = MySettings.Opacidad
                Me.Opacity = MySettings.OpacidadAcopleySalvaP

                salvaPantallaActivo = True
            Else
            End If
        End Sub
        ''' <summary>
        ''' Desactivar el salva pantalla al hacer doble clic en la fec o la hora
        ''' </summary>
        Private Sub LabelFechaHora_DoubleClick(sender As Object, e As EventArgs)
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
        Private Sub MnuAcoplarTransparente_Click(sender As Object, e As EventArgs)
            If inicializando Then
                Return
            End If

            mnuAcoplarTransparente.Checked = Not mnuAcoplarTransparente.Checked
            MySettings.AcoplarTransparente = mnuAcoplarTransparente.Checked
            AcoplarVentana(acoplarDonde)
        End Sub
        Private Sub MnuTamañoPequeño_Click(sender As Object, e As EventArgs)
            If inicializando Then
                Return
            End If

            'tamAnt = (this.Left, this.Top, this.Width, this.Height);
            mnuTamañoPequeño.Checked = Not mnuTamañoPequeño.Checked
            If mnuTamañoPequeño.Checked Then
                PonerTamañoAcoplar(True)
            Else
                inicializando = True
                LabelFechaHora_DoubleClick(Nothing, Nothing)
                AjustarTamañoFechaHora()
                inicializando = False
                ''' Posicionarlo donde estaba
                'this.Left = tamVentana.Left;
                'this.Top = tamVentana.Top;
                'this.Width = tamVentana.Width;
                'this.Height = tamVentana.Height;
            
            End If
        End Sub
        Private Sub mnuAcercaDe_Click(sender As Object, e As EventArgs)
            Dim msg As String = $"Acerca de {Application.ProductName} v{Application.ProductVersion}

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

¡Disfrútala! :-)"
            MessageBox.Show(msg, $"Acerca de {Application.ProductName} v{Application.ProductVersion}",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Sub
    End Class
End Namespace
