
namespace Reloj_Windows
{
    partial class FormReloj
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReloj));
            this.panelCentro = new System.Windows.Forms.Panel();
            this.labelHora = new System.Windows.Forms.Label();
            this.labelFecha = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LabelInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSplitDrop = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnu01AcoplarDer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu02AcoplarIzq = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu03AcoplarMinimo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu04AcoplarTransparente = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu05toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu06TamañoPequeño = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu07toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu08RecordarPosicion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu09TopMost = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu10toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu11Salvapantalla = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpacidad = new System.Windows.Forms.ToolStripMenuItem();
            this.cboOpacidad = new System.Windows.Forms.ToolStripComboBox();
            this.mnu12toolSeparatorCerrar = new System.Windows.Forms.ToolStripSeparator();
            this.mnu13AcercaDe = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu14Cerrar = new System.Windows.Forms.ToolStripMenuItem();
            this.timerActualizarFechaHora = new System.Windows.Forms.Timer(this.components);
            this.timerSalvaPantalla = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NotifyMenuRestaurar = new System.Windows.Forms.ToolStripMenuItem();
            this.NotifySeparator = new System.Windows.Forms.ToolStripSeparator();
            this.NotifyMenuCerrar = new System.Windows.Forms.ToolStripMenuItem();
            this.panelCentro.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.notifyContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCentro
            // 
            this.panelCentro.BackColor = System.Drawing.Color.RoyalBlue;
            this.panelCentro.Controls.Add(this.labelHora);
            this.panelCentro.Controls.Add(this.labelFecha);
            this.panelCentro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCentro.Location = new System.Drawing.Point(0, 0);
            this.panelCentro.Name = "panelCentro";
            this.panelCentro.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.panelCentro.Size = new System.Drawing.Size(804, 428);
            this.panelCentro.TabIndex = 2;
            this.panelCentro.DoubleClick += new System.EventHandler(this.LabelFechaHora_DoubleClick);
            this.panelCentro.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseDown);
            this.panelCentro.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseMove);
            this.panelCentro.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseUp);
            // 
            // labelHora
            // 
            this.labelHora.BackColor = System.Drawing.Color.Transparent;
            this.labelHora.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelHora.Font = new System.Drawing.Font("Consolas", 120F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelHora.ForeColor = System.Drawing.Color.Lime;
            this.labelHora.Location = new System.Drawing.Point(0, 0);
            this.labelHora.Margin = new System.Windows.Forms.Padding(1);
            this.labelHora.Name = "labelHora";
            this.labelHora.Size = new System.Drawing.Size(804, 337);
            this.labelHora.TabIndex = 0;
            this.labelHora.Text = "16:13:56";
            this.labelHora.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelHora.DoubleClick += new System.EventHandler(this.LabelFechaHora_DoubleClick);
            this.labelHora.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseDown);
            this.labelHora.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseMove);
            this.labelHora.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseUp);
            // 
            // labelFecha
            // 
            this.labelFecha.BackColor = System.Drawing.Color.RoyalBlue;
            this.labelFecha.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelFecha.Font = new System.Drawing.Font("Consolas", 37F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.labelFecha.ForeColor = System.Drawing.Color.Lime;
            this.labelFecha.Location = new System.Drawing.Point(0, 363);
            this.labelFecha.Name = "labelFecha";
            this.labelFecha.Size = new System.Drawing.Size(804, 55);
            this.labelFecha.TabIndex = 1;
            this.labelFecha.Text = "miércoles, 02 diciembre 2020";
            this.labelFecha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelFecha.DoubleClick += new System.EventHandler(this.LabelFechaHora_DoubleClick);
            this.labelFecha.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseDown);
            this.labelFecha.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseMove);
            this.labelFecha.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(99)))), ((int)(((byte)(177)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LabelInfo,
            this.btnSplitDrop});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(804, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseDown);
            this.statusStrip1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseMove);
            this.statusStrip1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MoverForm_MouseUp);
            // 
            // LabelInfo
            // 
            this.LabelInfo.BackColor = System.Drawing.Color.Transparent;
            this.LabelInfo.ForeColor = System.Drawing.Color.AliceBlue;
            this.LabelInfo.Name = "LabelInfo";
            this.LabelInfo.Size = new System.Drawing.Size(760, 17);
            this.LabelInfo.Spring = true;
            this.LabelInfo.Text = "toolStripStatusLabel1";
            this.LabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LabelInfo.MouseHover += new System.EventHandler(this.LabelInfo_MouseHover);
            // 
            // btnSplitDrop
            // 
            this.btnSplitDrop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSplitDrop.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu01AcoplarDer,
            this.mnu02AcoplarIzq,
            this.mnu03AcoplarMinimo,
            this.mnu04AcoplarTransparente,
            this.mnu05toolStripSeparator1,
            this.mnu06TamañoPequeño,
            this.mnu07toolStripSeparator2,
            this.mnu08RecordarPosicion,
            this.mnu09TopMost,
            this.mnu10toolStripSeparator3,
            this.mnu11Salvapantalla,
            this.mnuOpacidad,
            this.mnu12toolSeparatorCerrar,
            this.mnu13AcercaDe,
            this.mnu14Cerrar});
            this.btnSplitDrop.ForeColor = System.Drawing.Color.AliceBlue;
            this.btnSplitDrop.Image = ((System.Drawing.Image)(resources.GetObject("btnSplitDrop.Image")));
            this.btnSplitDrop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSplitDrop.Name = "btnSplitDrop";
            this.btnSplitDrop.Size = new System.Drawing.Size(29, 20);
            this.btnSplitDrop.Text = "toolStripDropDownButton1";
            this.btnSplitDrop.DropDownOpening += new System.EventHandler(this.BtnSplitDrop_DropDownOpening);
            // 
            // mnu01AcoplarDer
            // 
            this.mnu01AcoplarDer.Name = "mnu01AcoplarDer";
            this.mnu01AcoplarDer.Size = new System.Drawing.Size(281, 22);
            this.mnu01AcoplarDer.Text = "Acoplar en la derecha";
            this.mnu01AcoplarDer.ToolTipText = "Acoplar arriba a la derecha";
            this.mnu01AcoplarDer.Click += new System.EventHandler(this.MnuAcoplarDer_Click);
            // 
            // mnu02AcoplarIzq
            // 
            this.mnu02AcoplarIzq.Name = "mnu02AcoplarIzq";
            this.mnu02AcoplarIzq.Size = new System.Drawing.Size(281, 22);
            this.mnu02AcoplarIzq.Text = "Acoplar en la izquierda";
            this.mnu02AcoplarIzq.ToolTipText = "Acoplar arriba a la izquierda";
            this.mnu02AcoplarIzq.Click += new System.EventHandler(this.MnuAcoplarIzq_Click);
            // 
            // mnu03AcoplarMinimo
            // 
            this.mnu03AcoplarMinimo.Checked = true;
            this.mnu03AcoplarMinimo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnu03AcoplarMinimo.Name = "mnu03AcoplarMinimo";
            this.mnu03AcoplarMinimo.Size = new System.Drawing.Size(281, 22);
            this.mnu03AcoplarMinimo.Text = "Al acoplar hacerlo al tamaño mínimo";
            this.mnu03AcoplarMinimo.Click += new System.EventHandler(this.MnuAcoplarMinimo_Click);
            // 
            // mnu04AcoplarTransparente
            // 
            this.mnu04AcoplarTransparente.Name = "mnu04AcoplarTransparente";
            this.mnu04AcoplarTransparente.Size = new System.Drawing.Size(281, 22);
            this.mnu04AcoplarTransparente.Text = "Al acoplar ponerlo transparente (75%)";
            this.mnu04AcoplarTransparente.Click += new System.EventHandler(this.MnuAcoplarTransparente_Click);
            // 
            // mnu05toolStripSeparator1
            // 
            this.mnu05toolStripSeparator1.Name = "mnu05toolStripSeparator1";
            this.mnu05toolStripSeparator1.Size = new System.Drawing.Size(278, 6);
            // 
            // mnu06TamañoPequeño
            // 
            this.mnu06TamañoPequeño.Name = "mnu06TamañoPequeño";
            this.mnu06TamañoPequeño.Size = new System.Drawing.Size(281, 22);
            this.mnu06TamañoPequeño.Text = "Cambiar al tamaño a pequeño";
            this.mnu06TamañoPequeño.ToolTipText = "Marca esta casilla para mostrar la ventana a tamaño pequeño\r\nsi no está marcada s" +
    "e usará el tamaño que ajustes";
            this.mnu06TamañoPequeño.Click += new System.EventHandler(this.mnuTamañoPequeño_Click);
            // 
            // mnu07toolStripSeparator2
            // 
            this.mnu07toolStripSeparator2.Name = "mnu07toolStripSeparator2";
            this.mnu07toolStripSeparator2.Size = new System.Drawing.Size(278, 6);
            // 
            // mnu08RecordarPosicion
            // 
            this.mnu08RecordarPosicion.Checked = true;
            this.mnu08RecordarPosicion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnu08RecordarPosicion.Name = "mnu08RecordarPosicion";
            this.mnu08RecordarPosicion.Size = new System.Drawing.Size(281, 22);
            this.mnu08RecordarPosicion.Text = "Recordar la posición y tamaño al iniciar";
            this.mnu08RecordarPosicion.Click += new System.EventHandler(this.mnuRecordarPosicion_Click);
            // 
            // mnu09TopMost
            // 
            this.mnu09TopMost.Name = "mnu09TopMost";
            this.mnu09TopMost.Size = new System.Drawing.Size(281, 22);
            this.mnu09TopMost.Text = "Siempre encima";
            this.mnu09TopMost.Click += new System.EventHandler(this.mnuTopMost_Click);
            // 
            // mnu10toolStripSeparator3
            // 
            this.mnu10toolStripSeparator3.Name = "mnu10toolStripSeparator3";
            this.mnu10toolStripSeparator3.Size = new System.Drawing.Size(278, 6);
            // 
            // mnu11Salvapantalla
            // 
            this.mnu11Salvapantalla.Name = "mnu11Salvapantalla";
            this.mnu11Salvapantalla.ShortcutKeyDisplayString = "Ctrl+Shit+P";
            this.mnu11Salvapantalla.Size = new System.Drawing.Size(281, 22);
            this.mnu11Salvapantalla.Text = "Iniciar salvapantalla";
            this.mnu11Salvapantalla.ToolTipText = "No sustituye a ningún salvapantalla, \r\nsimplemente desplaza la ventana por la pan" +
    "talla.\r\nPara detenerlo haz doble-clic en la fecha o la hora.\r\nPulsa Ctrl+Shit+P " +
    "para iniciarlo o detenerlo.";
            this.mnu11Salvapantalla.Click += new System.EventHandler(this.mnuSalvapantalla_Click);
            // 
            // mnuOpacidad
            // 
            this.mnuOpacidad.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboOpacidad});
            this.mnuOpacidad.Name = "mnuOpacidad";
            this.mnuOpacidad.Size = new System.Drawing.Size(281, 22);
            this.mnuOpacidad.Text = "Opacidad";
            // 
            // cboOpacidad
            // 
            this.cboOpacidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOpacidad.Items.AddRange(new object[] {
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100"});
            this.cboOpacidad.Name = "cboOpacidad";
            this.cboOpacidad.Size = new System.Drawing.Size(121, 23);
            this.cboOpacidad.ToolTipText = "Elige el porcentaje de transparencia/opacidad de la ventana (100% totalmente opac" +
    "a no transparente)";
            this.cboOpacidad.SelectedIndexChanged += new System.EventHandler(this.CboOpacidad_SelectedIndexChanged);
            // 
            // mnu12toolSeparatorCerrar
            // 
            this.mnu12toolSeparatorCerrar.Name = "mnu12toolSeparatorCerrar";
            this.mnu12toolSeparatorCerrar.Size = new System.Drawing.Size(278, 6);
            // 
            // mnu13AcercaDe
            // 
            this.mnu13AcercaDe.Image = ((System.Drawing.Image)(resources.GetObject("mnu13AcercaDe.Image")));
            this.mnu13AcercaDe.Name = "mnu13AcercaDe";
            this.mnu13AcercaDe.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.mnu13AcercaDe.Size = new System.Drawing.Size(281, 22);
            this.mnu13AcercaDe.Text = "Acerca de...";
            this.mnu13AcercaDe.Click += new System.EventHandler(this.MnuAcercaDe_Click);
            // 
            // mnu14Cerrar
            // 
            this.mnu14Cerrar.Image = ((System.Drawing.Image)(resources.GetObject("mnu14Cerrar.Image")));
            this.mnu14Cerrar.Name = "mnu14Cerrar";
            this.mnu14Cerrar.Size = new System.Drawing.Size(281, 22);
            this.mnu14Cerrar.Text = "Cerrar el programa";
            this.mnu14Cerrar.Click += new System.EventHandler(this.MnuCerrar_Click);
            // 
            // timerActualizarFechaHora
            // 
            this.timerActualizarFechaHora.Tick += new System.EventHandler(this.TimerActualizarFechaHora_Tick);
            // 
            // timerSalvaPantalla
            // 
            this.timerSalvaPantalla.Tick += new System.EventHandler(this.TimerSalvaPantalla_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.notifyContextMenu;
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // notifyContextMenu
            // 
            this.notifyContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NotifyMenuRestaurar,
            this.NotifySeparator,
            this.NotifyMenuCerrar});
            this.notifyContextMenu.Name = "notifyContextMenu";
            this.notifyContextMenu.Size = new System.Drawing.Size(124, 54);
            // 
            // NotifyMenuRestaurar
            // 
            this.NotifyMenuRestaurar.Image = ((System.Drawing.Image)(resources.GetObject("NotifyMenuRestaurar.Image")));
            this.NotifyMenuRestaurar.Name = "NotifyMenuRestaurar";
            this.NotifyMenuRestaurar.Size = new System.Drawing.Size(123, 22);
            this.NotifyMenuRestaurar.Text = "Restaurar";
            this.NotifyMenuRestaurar.Click += new System.EventHandler(this.NotifyMenuRestaurar_Click);
            // 
            // NotifySeparator
            // 
            this.NotifySeparator.Name = "NotifySeparator";
            this.NotifySeparator.Size = new System.Drawing.Size(120, 6);
            // 
            // NotifyMenuCerrar
            // 
            this.NotifyMenuCerrar.Image = ((System.Drawing.Image)(resources.GetObject("NotifyMenuCerrar.Image")));
            this.NotifyMenuCerrar.Name = "NotifyMenuCerrar";
            this.NotifyMenuCerrar.Size = new System.Drawing.Size(123, 22);
            this.NotifyMenuCerrar.Text = "Cerrar";
            this.NotifyMenuCerrar.Click += new System.EventHandler(this.NotifyMenuCerrar_Click);
            // 
            // FormReloj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 450);
            this.Controls.Add(this.panelCentro);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 180);
            this.Name = "FormReloj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reloj Windows (C#)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.Move += new System.EventHandler(this.Form1_Move);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panelCentro.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.notifyContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.Panel panelAbajo;
        //private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Panel panelCentro;
        private System.Windows.Forms.Label labelHora;
        private System.Windows.Forms.Label labelFecha;
        //private System.Windows.Forms.Button btnOpciones;
        private System.Windows.Forms.Timer timerActualizarFechaHora;
        private System.Windows.Forms.Timer timerSalvaPantalla;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel LabelInfo;
        private System.Windows.Forms.ToolStripDropDownButton btnSplitDrop;
        //private System.Windows.Forms.ToolStripMenuItem mnuAcoplarIzq;
        private System.Windows.Forms.ToolStripMenuItem mnu01AcoplarDer;
        //private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        //private System.Windows.Forms.ToolStripMenuItem mnuRecordarPosicion;
        //private System.Windows.Forms.ToolStripMenuItem mnuTopMost;
        //private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox cboOpacidad;
        //private System.Windows.Forms.ToolStripMenuItem mnuAcoplarMinimo;
        //private System.Windows.Forms.ToolStripSeparator toolSeparatorCerrar;
        //private System.Windows.Forms.ToolStripMenuItem mnuCerrar;
        //private System.Windows.Forms.ToolStripMenuItem mnuSalvapantalla;
        private System.Windows.Forms.ToolStripMenuItem mnuOpacidad;
        //private System.Windows.Forms.ToolStripMenuItem mnuAcoplarTransparente;
        //private System.Windows.Forms.ToolStripMenuItem mnuTamañoPequeño;
        //private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        //private System.Windows.Forms.ToolStripMenuItem mnuAcercaDe;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip notifyContextMenu;
        private System.Windows.Forms.ToolStripMenuItem NotifyMenuRestaurar;
        private System.Windows.Forms.ToolStripSeparator NotifySeparator;
        private System.Windows.Forms.ToolStripMenuItem NotifyMenuCerrar;
        private System.Windows.Forms.ToolStripMenuItem mnu02AcoplarIzq;
        private System.Windows.Forms.ToolStripMenuItem mnu03AcoplarMinimo;
        private System.Windows.Forms.ToolStripMenuItem mnu04AcoplarTransparente;
        private System.Windows.Forms.ToolStripSeparator mnu05toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnu06TamañoPequeño;
        private System.Windows.Forms.ToolStripSeparator mnu07toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnu08RecordarPosicion;
        private System.Windows.Forms.ToolStripMenuItem mnu09TopMost;
        private System.Windows.Forms.ToolStripSeparator mnu10toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnu11Salvapantalla;
        private System.Windows.Forms.ToolStripSeparator mnu12toolSeparatorCerrar;
        private System.Windows.Forms.ToolStripMenuItem mnu13AcercaDe;
        private System.Windows.Forms.ToolStripMenuItem mnu14Cerrar;
    }
}

