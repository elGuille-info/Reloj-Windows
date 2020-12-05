Option Compare Text
Option Explicit On
Option Infer On
Option Strict On


Namespace Reloj_Windows
    Partial Class FormReloj
        ''' <summary>
        '''  Required designer variable.
        ''' </summary>
        Private components As ComponentModel.IContainer = Nothing
        ''' <summary>
        '''  Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region " Windows Form Designer generated code "

        ''' <summary>
        '''  Required method for Designer support - do not modify
        '''  the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormReloj))
            Me.panelCentro = New System.Windows.Forms.Panel()
            Me.labelHora = New System.Windows.Forms.Label()
            Me.labelFecha = New System.Windows.Forms.Label()
            Me.statusStrip1 = New System.Windows.Forms.StatusStrip()
            Me.LabelInfo = New System.Windows.Forms.ToolStripStatusLabel()
            Me.btnSplitDrop = New System.Windows.Forms.ToolStripDropDownButton()
            Me.mnuAcoplarDer = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuAcoplarIzq = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuAcoplarMinimo = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuAcoplarTransparente = New System.Windows.Forms.ToolStripMenuItem()
            Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
            Me.mnuTamañoPequeño = New System.Windows.Forms.ToolStripMenuItem()
            Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
            Me.mnuRecordarPosicion = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuTopMost = New System.Windows.Forms.ToolStripMenuItem()
            Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
            Me.mnuSalvapantalla = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuOpacidad = New System.Windows.Forms.ToolStripMenuItem()
            Me.cboOpacidad = New System.Windows.Forms.ToolStripComboBox()
            Me.toolSeparatorCerrar = New System.Windows.Forms.ToolStripSeparator()
            Me.mnuAcercaDe = New System.Windows.Forms.ToolStripMenuItem()
            Me.mnuCerrar = New System.Windows.Forms.ToolStripMenuItem()
            Me.timerActualizarFechaHora = New System.Windows.Forms.Timer(Me.components)
            Me.timerSalvaPantalla = New System.Windows.Forms.Timer(Me.components)
            Me.panelCentro.SuspendLayout()
            Me.statusStrip1.SuspendLayout()
            Me.SuspendLayout()
            '
            'panelCentro
            '
            Me.panelCentro.BackColor = System.Drawing.Color.RoyalBlue
            Me.panelCentro.Controls.Add(Me.labelHora)
            Me.panelCentro.Controls.Add(Me.labelFecha)
            Me.panelCentro.Dock = System.Windows.Forms.DockStyle.Fill
            Me.panelCentro.Location = New System.Drawing.Point(0, 0)
            Me.panelCentro.Name = "panelCentro"
            Me.panelCentro.Padding = New System.Windows.Forms.Padding(0, 0, 0, 10)
            Me.panelCentro.Size = New System.Drawing.Size(804, 428)
            Me.panelCentro.TabIndex = 2
            '
            'labelHora
            '
            Me.labelHora.BackColor = System.Drawing.Color.Transparent
            Me.labelHora.Dock = System.Windows.Forms.DockStyle.Top
            Me.labelHora.Font = New System.Drawing.Font("Consolas", 120.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.labelHora.ForeColor = System.Drawing.Color.Lime
            Me.labelHora.Location = New System.Drawing.Point(0, 0)
            Me.labelHora.Margin = New System.Windows.Forms.Padding(1)
            Me.labelHora.Name = "labelHora"
            Me.labelHora.Size = New System.Drawing.Size(804, 337)
            Me.labelHora.TabIndex = 0
            Me.labelHora.Text = "16:13:56"
            Me.labelHora.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'labelFecha
            '
            Me.labelFecha.BackColor = System.Drawing.Color.RoyalBlue
            Me.labelFecha.Dock = System.Windows.Forms.DockStyle.Bottom
            Me.labelFecha.Font = New System.Drawing.Font("Consolas", 37.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
            Me.labelFecha.ForeColor = System.Drawing.Color.Lime
            Me.labelFecha.Location = New System.Drawing.Point(0, 363)
            Me.labelFecha.Name = "labelFecha"
            Me.labelFecha.Size = New System.Drawing.Size(804, 55)
            Me.labelFecha.TabIndex = 1
            Me.labelFecha.Text = "miércoles, 02 diciembre 2020"
            Me.labelFecha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            '
            'statusStrip1
            '
            Me.statusStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(99, Byte), Integer), CType(CType(177, Byte), Integer))
            Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LabelInfo, Me.btnSplitDrop})
            Me.statusStrip1.Location = New System.Drawing.Point(0, 428)
            Me.statusStrip1.Name = "statusStrip1"
            Me.statusStrip1.ShowItemToolTips = True
            Me.statusStrip1.Size = New System.Drawing.Size(804, 22)
            Me.statusStrip1.TabIndex = 3
            Me.statusStrip1.Text = "statusStrip1"
            '
            'LabelInfo
            '
            Me.LabelInfo.BackColor = System.Drawing.Color.Transparent
            Me.LabelInfo.ForeColor = System.Drawing.Color.AliceBlue
            Me.LabelInfo.Name = "LabelInfo"
            Me.LabelInfo.Size = New System.Drawing.Size(760, 17)
            Me.LabelInfo.Spring = True
            Me.LabelInfo.Text = "toolStripStatusLabel1"
            Me.LabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            '
            'btnSplitDrop
            '
            Me.btnSplitDrop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
            Me.btnSplitDrop.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAcoplarDer, Me.mnuAcoplarIzq, Me.mnuAcoplarMinimo, Me.mnuAcoplarTransparente, Me.toolStripSeparator1, Me.mnuTamañoPequeño, Me.toolStripSeparator2, Me.mnuRecordarPosicion, Me.mnuTopMost, Me.toolStripSeparator3, Me.mnuSalvapantalla, Me.mnuOpacidad, Me.toolSeparatorCerrar, Me.mnuAcercaDe, Me.mnuCerrar})
            Me.btnSplitDrop.ForeColor = System.Drawing.Color.AliceBlue
            Me.btnSplitDrop.Image = CType(resources.GetObject("btnSplitDrop.Image"), System.Drawing.Image)
            Me.btnSplitDrop.ImageTransparentColor = System.Drawing.Color.Magenta
            Me.btnSplitDrop.Name = "btnSplitDrop"
            Me.btnSplitDrop.Size = New System.Drawing.Size(29, 20)
            Me.btnSplitDrop.Text = "toolStripDropDownButton1"
            '
            'mnuAcoplarDer
            '
            Me.mnuAcoplarDer.Name = "mnuAcoplarDer"
            Me.mnuAcoplarDer.Size = New System.Drawing.Size(281, 22)
            Me.mnuAcoplarDer.Text = "Acoplar en la derecha"
            Me.mnuAcoplarDer.ToolTipText = "Acoplar arriba a la derecha"
            '
            'mnuAcoplarIzq
            '
            Me.mnuAcoplarIzq.Name = "mnuAcoplarIzq"
            Me.mnuAcoplarIzq.Size = New System.Drawing.Size(281, 22)
            Me.mnuAcoplarIzq.Text = "Acoplar en la izquierda"
            Me.mnuAcoplarIzq.ToolTipText = "Acoplar arriba a la izquierda"
            '
            'mnuAcoplarMinimo
            '
            Me.mnuAcoplarMinimo.Checked = True
            Me.mnuAcoplarMinimo.CheckState = System.Windows.Forms.CheckState.Checked
            Me.mnuAcoplarMinimo.Name = "mnuAcoplarMinimo"
            Me.mnuAcoplarMinimo.Size = New System.Drawing.Size(281, 22)
            Me.mnuAcoplarMinimo.Text = "Al acoplar hacerlo al tamaño mínimo"
            '
            'mnuAcoplarTransparente
            '
            Me.mnuAcoplarTransparente.Name = "mnuAcoplarTransparente"
            Me.mnuAcoplarTransparente.Size = New System.Drawing.Size(281, 22)
            Me.mnuAcoplarTransparente.Text = "Al acoplar ponerlo transparente (75%)"
            '
            'toolStripSeparator1
            '
            Me.toolStripSeparator1.Name = "toolStripSeparator1"
            Me.toolStripSeparator1.Size = New System.Drawing.Size(278, 6)
            '
            'mnuTamañoPequeño
            '
            Me.mnuTamañoPequeño.Name = "mnuTamañoPequeño"
            Me.mnuTamañoPequeño.Size = New System.Drawing.Size(281, 22)
            Me.mnuTamañoPequeño.Text = "Cambiar al tamaño a pequeño"
            Me.mnuTamañoPequeño.ToolTipText = "Marca esta casilla para mostrar la ventana a tamaño pequeño" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "si no está marcada s" &
    "e usará el tamaño que ajustes"
            '
            'toolStripSeparator2
            '
            Me.toolStripSeparator2.Name = "toolStripSeparator2"
            Me.toolStripSeparator2.Size = New System.Drawing.Size(278, 6)
            '
            'mnuRecordarPosicion
            '
            Me.mnuRecordarPosicion.Checked = True
            Me.mnuRecordarPosicion.CheckState = System.Windows.Forms.CheckState.Checked
            Me.mnuRecordarPosicion.Name = "mnuRecordarPosicion"
            Me.mnuRecordarPosicion.Size = New System.Drawing.Size(281, 22)
            Me.mnuRecordarPosicion.Text = "Recordar la posición y tamaño al iniciar"
            '
            'mnuTopMost
            '
            Me.mnuTopMost.Name = "mnuTopMost"
            Me.mnuTopMost.Size = New System.Drawing.Size(281, 22)
            Me.mnuTopMost.Text = "Siempre encima"
            '
            'toolStripSeparator3
            '
            Me.toolStripSeparator3.Name = "toolStripSeparator3"
            Me.toolStripSeparator3.Size = New System.Drawing.Size(278, 6)
            '
            'mnuSalvapantalla
            '
            Me.mnuSalvapantalla.Name = "mnuSalvapantalla"
            Me.mnuSalvapantalla.ShortcutKeyDisplayString = "Ctrl+Shift+P"
            Me.mnuSalvapantalla.Size = New System.Drawing.Size(281, 22)
            Me.mnuSalvapantalla.Text = "Iniciar salvapantalla"
            Me.mnuSalvapantalla.ToolTipText = "No sustituye a ningún salvapantalla, " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "simplemente desplaza la ventana por la pan" &
    "talla." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Para detenerlo haz doble-clic en la fecha o la hora." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Pulsa Ctrl+Shit+P " &
    "para iniciarlo o detenerlo."
            '
            'mnuOpacidad
            '
            Me.mnuOpacidad.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.cboOpacidad})
            Me.mnuOpacidad.Name = "mnuOpacidad"
            Me.mnuOpacidad.Size = New System.Drawing.Size(281, 22)
            Me.mnuOpacidad.Text = "Opacidad"
            '
            'cboOpacidad
            '
            Me.cboOpacidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
            Me.cboOpacidad.Items.AddRange(New Object() {"20", "30", "40", "50", "60", "70", "80", "90", "100"})
            Me.cboOpacidad.Name = "cboOpacidad"
            Me.cboOpacidad.Size = New System.Drawing.Size(121, 23)
            Me.cboOpacidad.ToolTipText = "Elige el porcentaje de transparencia/opacidad de la ventana (100% totalmente opac" &
    "a no transparente)"
            '
            'toolSeparatorCerrar
            '
            Me.toolSeparatorCerrar.Name = "toolSeparatorCerrar"
            Me.toolSeparatorCerrar.Size = New System.Drawing.Size(278, 6)
            '
            'mnuAcercaDe
            '
            Me.mnuAcercaDe.Image = CType(resources.GetObject("mnuAcercaDe.Image"), System.Drawing.Image)
            Me.mnuAcercaDe.Name = "mnuAcercaDe"
            Me.mnuAcercaDe.ShortcutKeys = System.Windows.Forms.Keys.F1
            Me.mnuAcercaDe.Size = New System.Drawing.Size(281, 22)
            Me.mnuAcercaDe.Text = "Acerca de..."
            '
            'mnuCerrar
            '
            Me.mnuCerrar.Image = CType(resources.GetObject("mnuCerrar.Image"), System.Drawing.Image)
            Me.mnuCerrar.Name = "mnuCerrar"
            Me.mnuCerrar.Size = New System.Drawing.Size(281, 22)
            Me.mnuCerrar.Text = "Cerrar el programa"
            '
            'timerActualizarFechaHora
            '
            '
            'timerSalvaPantalla
            '
            '
            'Form1
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(804, 450)
            Me.Controls.Add(Me.panelCentro)
            Me.Controls.Add(Me.statusStrip1)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.MinimumSize = New System.Drawing.Size(350, 180)
            Me.Name = "Form1"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "Reloj Windows (VB)"
            Me.panelCentro.ResumeLayout(False)
            Me.statusStrip1.ResumeLayout(False)
            Me.statusStrip1.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

#End Region

        'private System.Windows.Forms.Panel panelAbajo;
        'private System.Windows.Forms.Label labelInfo;
        Private WithEvents panelCentro As Windows.Forms.Panel
        Private WithEvents labelHora As Windows.Forms.Label
        Private WithEvents labelFecha As Windows.Forms.Label
        'private System.Windows.Forms.Button btnOpciones;
        Private WithEvents timerActualizarFechaHora As Windows.Forms.Timer
        Private WithEvents timerSalvaPantalla As Windows.Forms.Timer
        Private WithEvents statusStrip1 As Windows.Forms.StatusStrip
        Private WithEvents LabelInfo As Windows.Forms.ToolStripStatusLabel
        Private WithEvents btnSplitDrop As Windows.Forms.ToolStripDropDownButton
        Private WithEvents mnuAcoplarIzq As Windows.Forms.ToolStripMenuItem
        Private WithEvents mnuAcoplarDer As Windows.Forms.ToolStripMenuItem
        Private toolStripSeparator1 As Windows.Forms.ToolStripSeparator
        Private WithEvents mnuRecordarPosicion As Windows.Forms.ToolStripMenuItem
        Private WithEvents mnuTopMost As Windows.Forms.ToolStripMenuItem
        Private toolStripSeparator2 As Windows.Forms.ToolStripSeparator
        Private WithEvents cboOpacidad As Windows.Forms.ToolStripComboBox
        Private WithEvents mnuAcoplarMinimo As Windows.Forms.ToolStripMenuItem
        Private toolSeparatorCerrar As Windows.Forms.ToolStripSeparator
        Private WithEvents mnuCerrar As Windows.Forms.ToolStripMenuItem
        Private WithEvents mnuSalvapantalla As Windows.Forms.ToolStripMenuItem
        Private WithEvents mnuOpacidad As Windows.Forms.ToolStripMenuItem
        Private WithEvents mnuAcoplarTransparente As Windows.Forms.ToolStripMenuItem
        Private WithEvents mnuTamañoPequeño As Windows.Forms.ToolStripMenuItem
        Private toolStripSeparator3 As Windows.Forms.ToolStripSeparator
        Private WithEvents mnuAcercaDe As Windows.Forms.ToolStripMenuItem
    End Class
End Namespace
