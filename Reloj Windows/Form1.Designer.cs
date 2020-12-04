﻿
namespace Reloj_Windows
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelCentro = new System.Windows.Forms.Panel();
            this.labelHora = new System.Windows.Forms.Label();
            this.labelFecha = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.LabelInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSplitDrop = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuAcoplarDer = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAcoplarIzq = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAcoplarMinimo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAcoplarTransparente = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTamañoPequeño = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRecordarPosicion = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTopMost = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSalvapantalla = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpacidad = new System.Windows.Forms.ToolStripMenuItem();
            this.cboOpacidad = new System.Windows.Forms.ToolStripComboBox();
            this.toolSeparatorCerrar = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAcercaDe = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCerrar = new System.Windows.Forms.ToolStripMenuItem();
            this.timerActualizarFechaHora = new System.Windows.Forms.Timer(this.components);
            this.timerSalvaPantalla = new System.Windows.Forms.Timer(this.components);
            this.panelCentro.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.mnuAcoplarDer,
            this.mnuAcoplarIzq,
            this.mnuAcoplarMinimo,
            this.mnuAcoplarTransparente,
            this.toolStripSeparator1,
            this.mnuTamañoPequeño,
            this.toolStripSeparator2,
            this.mnuRecordarPosicion,
            this.mnuTopMost,
            this.toolStripSeparator3,
            this.mnuSalvapantalla,
            this.mnuOpacidad,
            this.toolSeparatorCerrar,
            this.mnuAcercaDe,
            this.mnuCerrar});
            this.btnSplitDrop.ForeColor = System.Drawing.Color.AliceBlue;
            this.btnSplitDrop.Image = ((System.Drawing.Image)(resources.GetObject("btnSplitDrop.Image")));
            this.btnSplitDrop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSplitDrop.Name = "btnSplitDrop";
            this.btnSplitDrop.Size = new System.Drawing.Size(29, 20);
            this.btnSplitDrop.Text = "toolStripDropDownButton1";
            this.btnSplitDrop.DropDownOpening += new System.EventHandler(this.BtnSplitDrop_DropDownOpening);
            // 
            // mnuAcoplarDer
            // 
            this.mnuAcoplarDer.Name = "mnuAcoplarDer";
            this.mnuAcoplarDer.Size = new System.Drawing.Size(281, 22);
            this.mnuAcoplarDer.Text = "Acoplar en la derecha";
            this.mnuAcoplarDer.ToolTipText = "Acoplar arriba a la derecha";
            this.mnuAcoplarDer.Click += new System.EventHandler(this.MnuAcoplarDer_Click);
            // 
            // mnuAcoplarIzq
            // 
            this.mnuAcoplarIzq.Name = "mnuAcoplarIzq";
            this.mnuAcoplarIzq.Size = new System.Drawing.Size(281, 22);
            this.mnuAcoplarIzq.Text = "Acoplar en la izquierda";
            this.mnuAcoplarIzq.ToolTipText = "Acoplar arriba a la izquierda";
            this.mnuAcoplarIzq.Click += new System.EventHandler(this.MnuAcoplarIzq_Click);
            // 
            // mnuAcoplarMinimo
            // 
            this.mnuAcoplarMinimo.Checked = true;
            this.mnuAcoplarMinimo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuAcoplarMinimo.Name = "mnuAcoplarMinimo";
            this.mnuAcoplarMinimo.Size = new System.Drawing.Size(281, 22);
            this.mnuAcoplarMinimo.Text = "Al acoplar hacerlo al tamaño mínimo";
            this.mnuAcoplarMinimo.Click += new System.EventHandler(this.MnuAcoplarMinimo_Click);
            // 
            // mnuAcoplarTransparente
            // 
            this.mnuAcoplarTransparente.Name = "mnuAcoplarTransparente";
            this.mnuAcoplarTransparente.Size = new System.Drawing.Size(281, 22);
            this.mnuAcoplarTransparente.Text = "Al acoplar ponerlo transparente (75%)";
            this.mnuAcoplarTransparente.Click += new System.EventHandler(this.MnuAcoplarTransparente_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(278, 6);
            // 
            // mnuTamañoPequeño
            // 
            this.mnuTamañoPequeño.Name = "mnuTamañoPequeño";
            this.mnuTamañoPequeño.Size = new System.Drawing.Size(281, 22);
            this.mnuTamañoPequeño.Text = "Cambiar al tamaño a pequeño";
            this.mnuTamañoPequeño.ToolTipText = "Marca esta casilla para mostrar la ventana a tamaño pequeño\r\nsi no está marcada s" +
    "e usará el tamaño que ajustes";
            this.mnuTamañoPequeño.Click += new System.EventHandler(this.MnuTamañoPequeño_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(278, 6);
            // 
            // mnuRecordarPosicion
            // 
            this.mnuRecordarPosicion.Checked = true;
            this.mnuRecordarPosicion.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuRecordarPosicion.Name = "mnuRecordarPosicion";
            this.mnuRecordarPosicion.Size = new System.Drawing.Size(281, 22);
            this.mnuRecordarPosicion.Text = "Recordar la posición y tamaño al iniciar";
            this.mnuRecordarPosicion.Click += new System.EventHandler(this.MnuRecordarPosicion_Click);
            // 
            // mnuTopMost
            // 
            this.mnuTopMost.Name = "mnuTopMost";
            this.mnuTopMost.Size = new System.Drawing.Size(281, 22);
            this.mnuTopMost.Text = "Siempre encima";
            this.mnuTopMost.Click += new System.EventHandler(this.MnuTopMost_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(278, 6);
            // 
            // mnuSalvapantalla
            // 
            this.mnuSalvapantalla.Name = "mnuSalvapantalla";
            this.mnuSalvapantalla.Size = new System.Drawing.Size(281, 22);
            this.mnuSalvapantalla.Text = "Usar como salvapantalla";
            this.mnuSalvapantalla.ToolTipText = "No sustituye a ningún salvapantalla, \r\nsimplemente desplaza la ventana por la pan" +
    "talla.\r\nPara deterlo haz doble-clic en la fecha o la hora.";
            this.mnuSalvapantalla.Click += new System.EventHandler(this.MnuSalvaPantalla_Click);
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
            // toolSeparatorCerrar
            // 
            this.toolSeparatorCerrar.Name = "toolSeparatorCerrar";
            this.toolSeparatorCerrar.Size = new System.Drawing.Size(278, 6);
            // 
            // mnuAcercaDe
            // 
            this.mnuAcercaDe.Image = ((System.Drawing.Image)(resources.GetObject("mnuAcercaDe.Image")));
            this.mnuAcercaDe.Name = "mnuAcercaDe";
            this.mnuAcercaDe.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.mnuAcercaDe.Size = new System.Drawing.Size(281, 22);
            this.mnuAcercaDe.Text = "Acerca de...";
            this.mnuAcercaDe.Click += new System.EventHandler(this.mnuAcercaDe_Click);
            // 
            // mnuCerrar
            // 
            this.mnuCerrar.Image = ((System.Drawing.Image)(resources.GetObject("mnuCerrar.Image")));
            this.mnuCerrar.Name = "mnuCerrar";
            this.mnuCerrar.Size = new System.Drawing.Size(281, 22);
            this.mnuCerrar.Text = "Cerrar el programa";
            this.mnuCerrar.Click += new System.EventHandler(this.MnuCerrar_Click);
            // 
            // timerActualizarFechaHora
            // 
            this.timerActualizarFechaHora.Tick += new System.EventHandler(this.TimerActualizarFechaHora_Tick);
            // 
            // timerSalvaPantalla
            // 
            this.timerSalvaPantalla.Tick += new System.EventHandler(this.TimerSalvaPantalla_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 450);
            this.Controls.Add(this.panelCentro);
            this.Controls.Add(this.statusStrip1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 180);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reloj Windows (C#)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Move += new System.EventHandler(this.Form1_Move);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panelCentro.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem mnuAcoplarIzq;
        private System.Windows.Forms.ToolStripMenuItem mnuAcoplarDer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuRecordarPosicion;
        private System.Windows.Forms.ToolStripMenuItem mnuTopMost;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripComboBox cboOpacidad;
        private System.Windows.Forms.ToolStripMenuItem mnuAcoplarMinimo;
        private System.Windows.Forms.ToolStripSeparator toolSeparatorCerrar;
        private System.Windows.Forms.ToolStripMenuItem mnuCerrar;
        private System.Windows.Forms.ToolStripMenuItem mnuSalvapantalla;
        private System.Windows.Forms.ToolStripMenuItem mnuOpacidad;
        private System.Windows.Forms.ToolStripMenuItem mnuAcoplarTransparente;
        private System.Windows.Forms.ToolStripMenuItem mnuTamañoPequeño;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem mnuAcercaDe;
    }
}

