﻿//-----------------------------------------------------------------------------
// Reloj para Windows                                               (02/Dic/20)
//
// (c) Guillermo (elGuille) Som, 2020
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
//using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reloj_Windows
{
    public partial class Form1 : Form
    {
        // 
        /// <summary>
        /// Para acceder a la propiedades del programa 
        /// </summary>
        private readonly Properties.Settings MySettings = Properties.Settings.Default;

        /// <summary>
        /// Fecha desde que está activo el programa 
        /// </summary>
        private readonly DateTime ActivoDesde;

        /// <summary>
        /// True para no ejecutar el código de los eventos
        /// </summary>
        private bool inicializando;

        /// <summary>
        /// Los valores para la configuración que necesitan variables aparte
        /// Si es nulo no acoplar, 
        /// si es true acoplar a la derecha, si es false acoplar a la izquierda
        /// </summary>
        private bool? acoplarDonde;

        // El tamaño y posición de la ventana
        private (int Left, int Top, int Width, int Height) tamVentana;

        // Para la posición anterior a acoplar
        // asignarlo solo si no está acoplado,
        // por ejemplo, si estaba acoplado a la derecha y
        // se cambia a la izquierda, no asignar la posición
        private (int Left, int Top, int Width, int Height) tamAnt;

        // Los textos informativos según se muestre la ventana más ancha o más estrecha
        private (string Largo, string Corto) textoInfo;

        // Para el salva pantallas
        private int posLeft = -1;
        private int posTop = -1;
        private readonly Random aleatorio = new Random();

        /// <summary>
        /// para cuando está el salvapantallas activo dejar siempre encima
        /// </summary>
        private bool topMostAnt;

        /// <summary>
        /// Cuando está el savapantallas, ponerlo más transparente 
        /// esta variable recuerda el valor que tenía antes de iniciar el salvapantallas
        /// </summary>
        private double opacidadAnt;

        /// <summary>
        /// Si está en modo salva pantalla
        /// </summary>
        private bool salvaPantallaActivo;

        public Form1()
        {
            inicializando = true;

            InitializeComponent();

            this.ActivoDesde = DateTime.Now;

            // Leer los valores de la configuración

            if (MySettings.AcoplarDonde == -1)
                this.acoplarDonde = null;
            else if (MySettings.AcoplarDonde == 0)
                this.acoplarDonde = false;
            else if (MySettings.AcoplarDonde == 1)
                this.acoplarDonde = true;

            tamVentana = (MySettings.vLeft, MySettings.vTop, MySettings.vWidth, MySettings.vHeight);

            // Por si modifica manualmente el valor de la opacidad al acoplar y en salvapantallas)
            if (MySettings.OpacidadAcopleySalvaP < 0.5)
                MySettings.OpacidadAcopleySalvaP = 0.75;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelHora.Text = ActivoDesde.ToString("HH:mm:ss");
            labelFecha.Text = ActivoDesde.ToString("dddd, dd MMMM yyyy");

            var s = "";
            if (ActivoDesde.Year > 2020)
                s = $"-{ActivoDesde.Year}";

            textoInfo.Corto = $"{Application.ProductName} v{Application.ProductVersion}";
            textoInfo.Largo = $"{textoInfo.Corto} - (c)Guillermo Som (elGuille), 2000{s}";

            LabelInfo.Text = textoInfo.Largo;

            // Actualizar el salvapantalla cada 3 segundos
            timerSalvaPantalla.Interval = 3000;
            timerSalvaPantalla.Enabled = false;

            // Actualizar la fecha y hora cada 0.8 segundos
            timerActualizarFechaHora.Interval = 800;
            timerActualizarFechaHora.Enabled = true;

            // Asignar el valor antes de inicializar
            // por si se ha acoplado
            this.tamAnt = (this.Left, this.Top, this.Width, this.Height);

            Inicializar();

            this.inicializando = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Guardar los valores de la configuración

            // mySettings.AcoplarDonde es un valor int -1 es null, 0 es false y 1 es true
            MySettings.AcoplarDonde = acoplarDonde is null ? -1 : acoplarDonde.Value ? 1 : 0;
            MySettings.vLeft = tamVentana.Left;
            MySettings.vTop = tamVentana.Top;
            MySettings.vWidth = tamVentana.Width;
            MySettings.vHeight = tamVentana.Height;

            MySettings.Save();
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            // Solo guardar la posición y tamaño si está en modo normal
            // no está como salvapantallas ni está acoplado
            if (this.WindowState == FormWindowState.Normal && !salvaPantallaActivo && acoplarDonde is null)
            {
                tamVentana = (this.Left, this.Top, this.Width, this.Height);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (inicializando) return;

            //var d = DateTime.Now;

            // Solo guardar la posición y tamaño si está en modo normal
            // no está como salvapantallas ni está acoplado
            if (this.WindowState == FormWindowState.Normal && !salvaPantallaActivo &&
                        acoplarDonde is null && !mnuTamañoPequeño.Checked)
            {
                tamVentana = (this.Left, this.Top, this.Width, this.Height);
            }

            AjustarTamañoFechaHora();

            btnSplitDrop.ToolTipText = $"{this.Location} {this.Size}".Replace("X", "L").Replace("Y", "T");
        }


        //
        // Los métodos normales
        //

        /// <summary>
        /// Ajustar el tamaño de la fecha/hora y el contenido de la fecha
        /// </summary>
        /// <param name="d"></param>
        private void AjustarTamañoFechaHora()
        {
            var d = DateTime.Now;

            // TODO
            // Ajustar el tamaño de la fuente del reloj si es ancho, pero menos alto

            // El tamaño mínimo de la ventana es: W=350, H = 180

            if (Height > 190)
            {
                labelFecha.Font = new Font(labelFecha.Font.FontFamily, 37, FontStyle.Bold);
                labelFecha.Height = 55;
                labelHora.Height = this.ClientSize.Height - labelFecha.Height - statusStrip1.Height - 60;
            }

            if (Width < 400)
            {
                LabelInfo.Text = textoInfo.Corto;
                this.ControlBox = false;
            }
            else
            {
                LabelInfo.Text = textoInfo.Largo;
                this.ControlBox = true;
            }

            float fs = (float)(Width / 7); // 6.7
            labelHora.Font = new Font(labelHora.Font.FontFamily, fs, FontStyle.Bold);

            if (Width < 500)
            {
                labelFecha.Text = d.ToString("dd.MMMyyyy");
            }
            else if (Width < 650)
            {
                labelFecha.Text = d.ToString("ddd, dd.MMMyyyy");
            }
            else if (Width < 750)
            {
                labelFecha.Text = d.ToString("dddd, dd.MMMyyyy");
            }
            else
            {
                labelFecha.Text = d.ToString("dddd, dd MMMM yyyy");
            }
        }

        /// <summary>
        /// Acoplar la ventana a la derecha o izquierda o centrarla.
        /// </summary>
        /// <param name="donde">Un valor de tipo Boolean? 
        /// true acopla a la derecha, false acopla a la izquierda, 
        /// nulo la pone donde estaba antes de acoplar.
        /// </param>
        private void AcoplarVentana(bool? donde)
        {
            this.inicializando = true;

            if (acoplarDonde is null)
                this.tamAnt = (Left, Top, Width, Height);

            if (donde is null)
            {
                // Dejarla en la posición normal
                // no acoplar
                mnuAcoplarDer.Checked = false;
                mnuAcoplarIzq.Checked = false;

                // Restaurar la posición y tamaño guardados
                if (!(tamAnt.Left < 0 && tamAnt.Top < 0 &&
                            tamAnt.Width < 0 && tamAnt.Height < 0))
                {
                    // Debe entrar en el evento Form1_Resize
                    // para que se cambie el tamaño de la fecha/hora
                    inicializando = false;
                    this.Left = tamAnt.Left;
                    this.Top = tamAnt.Top;
                    this.Width = tamAnt.Width;
                    this.Height = tamAnt.Height;
                    inicializando = true;
                }
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.Opacity = MySettings.Opacidad;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;

                if (MySettings.AcoplarTransparente)
                    this.Opacity = MySettings.OpacidadAcopleySalvaP;

                if (donde.Value)
                {
                    PonerTamañoAcoplar(MySettings.AcoplarMinimo);
                    // Posicionar la ventana arriba a la derecha
                    this.Left = Screen.PrimaryScreen.WorkingArea.Width - Width;
                    this.Top = 0;
                    mnuAcoplarDer.Checked = true;
                    mnuAcoplarIzq.Checked = false;
                }
                else if (!donde.Value)
                {
                    PonerTamañoAcoplar(MySettings.AcoplarMinimo);
                    // Posicionar la ventana arriba a la izquierda
                    this.Left = 0;
                    this.Top = 0;
                    mnuAcoplarIzq.Checked = true;
                    mnuAcoplarDer.Checked = false;
                }
            }

            this.acoplarDonde = donde;

            this.inicializando = false;
        }

        /// <summary>
        /// Asignar el tamaño de acoplar a la ventana
        /// </summary>
        private void PonerTamañoAcoplar(bool acoplarMinimo)
        {
            if (acoplarMinimo)
            {
                var tmp = inicializando;
                var tamTmp = tamAnt;

                // interesa que entre en Form1_Resize
                inicializando = false;
                this.Width = this.MinimumSize.Width;
                this.Height = this.MinimumSize.Height;

                tamAnt = tamTmp;
                inicializando = tmp;

                labelFecha.Font = new Font(labelFecha.Font.FontFamily, 24, FontStyle.Bold);
                labelFecha.Height = 35;
                labelFecha.Text = DateTime.Now.ToString("ddd, dd.MMMyyyy");
                labelHora.Height = this.ClientSize.Height - labelFecha.Height - statusStrip1.Height - 20;
            }
        }

        /// <summary>
        /// Inicializar con los valores de la configuración
        /// </summary>
        private void Inicializar()
        {
            // Valores predeterminados por si los cambio en diseño
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Opacity = 1;
            opacidadAnt = 1;

            salvaPantallaActivo = false;

            // Posicionar la ventana solo si los valores no son menores de cero
            // esto habrá que arreglarlo si se usa en un monitor externo
            if (!(tamVentana.Left < 0 && tamVentana.Top < 0 &&
                        tamVentana.Width < 0 && tamVentana.Height < 0))
            {
                this.Left = tamVentana.Left;
                this.Top = tamVentana.Top;
                this.Width = tamVentana.Width;
                this.Height = tamVentana.Height;
            }

            this.TopMost = MySettings.SiempreEncima;

            // No poner una opacidad menor de 20
            if (MySettings.Opacidad < 20)
                MySettings.Opacidad = 20;

            this.Opacity = MySettings.Opacidad / 100;
            cboOpacidad.Text = MySettings.Opacidad.ToString();


            // Solo asignarlo si no está acoplada
            if (acoplarDonde is null)
                this.tamAnt = (Left, Top, Width, Height);

            mnuAcoplarDer.Checked = MySettings.AcoplarDonde == 1;
            mnuAcoplarIzq.Checked = MySettings.AcoplarDonde == 0;
            mnuAcoplarMinimo.Checked = MySettings.AcoplarMinimo;
            mnuRecordarPosicion.Checked = MySettings.RecordarPos;
            //mnuSalvapantalla.Checked =
            mnuTopMost.Checked = MySettings.SiempreEncima;
            mnuAcoplarTransparente.Checked = MySettings.AcoplarTransparente;

            AcoplarVentana(acoplarDonde);

            LabelInfo.ToolTipText = $"Activo desde {ActivoDesde:HH:mm:ss dd.MMMyyyy}";
            btnSplitDrop.ToolTipText = $"{this.Location} {this.Size}".Replace("X", "L").Replace("Y", "T");
        }

        //
        // Los temporizadores
        //

        private void TimerActualizarFechaHora_Tick(object sender, EventArgs e)
        {
            labelHora.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// Este temporizador actúa como un salvapantallas
        /// </summary>
        private void TimerSalvaPantalla_Tick(object sender, EventArgs e)
        {
            if (aleatorio.Next(0, 2) == 0)
                posLeft = -1;
            else
                posTop = 1;

            if (aleatorio.Next(0, 2) == 0)
                posTop = -1;
            else
                posTop = 1;

            this.Left += 90 * posLeft;
            this.Top += 90 * posTop;

            if (this.Left > Screen.PrimaryScreen.WorkingArea.Width - this.Width - 120)
            {
                this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width - 120;
                posLeft = -1;
            }
            if (this.Left < 0)
            {
                this.Left = 0;
                posLeft = 1;
            }

            if (this.Top > Screen.PrimaryScreen.WorkingArea.Height - this.Height - 30)
            {
                this.Top = Screen.PrimaryScreen.WorkingArea.Height - this.Height - 30;
                posTop = -1;
            }
            if (this.Top < 0)
            {
                this.Top = 0;
                posTop = 1;
            }
        }

        //
        // Para mover el formulario pulsando en los controles que están encima
        // el código que suelo usar en mis aplicaciones (normalmente en las ventanas AcercaDe)
        // adaptado de VB a C#
        //

        // Si se muestra la barra de títulos, no moverlo desde los controles

        private int pX;
        private int pY;
        private bool ratonPulsado;

        private void MoverForm_MouseDown(object sender, MouseEventArgs e)
        {
            //if (this.ControlBox) return;

            if (e.Button == MouseButtons.Left)
            {
                this.ratonPulsado = true;
                this.pX = e.X;
                this.pY = e.Y;
            }
        }

        private void MoverForm_MouseMove(object sender, MouseEventArgs e)
        {
            //if (this.ControlBox) return;

            if (ratonPulsado)
            {
                this.Left += e.X - pX;
                this.Top += e.Y - pY;
            }
        }

        private void MoverForm_MouseUp(object sender, MouseEventArgs e)
        {
            this.ratonPulsado = false;
        }

        //
        // Otros métodos de evento
        //

        /// <summary>
        /// Mientras se deja sin mover el cursor del ratón encima del control
        /// </summary>
        private void LabelInfo_MouseHover(object sender, EventArgs e)
        {
            LabelInfo.ToolTipText = $"Activo desde {ActivoDesde:HH:mm:ss dd.MMMyyyy}";
        }

        private void MnuAcoplarDer_Click(object sender, EventArgs e)
        {
            if (inicializando) return;

            mnuAcoplarDer.Checked = !mnuAcoplarDer.Checked;
            // Acoplar a la derecha
            if (mnuAcoplarDer.Checked)
                AcoplarVentana(true);
            else
                AcoplarVentana(null);
        }

        private void MnuAcoplarIzq_Click(object sender, EventArgs e)
        {
            if (inicializando) return;

            mnuAcoplarIzq.Checked = !mnuAcoplarIzq.Checked;
            // Acoplar arriba a la izquierda
            if (mnuAcoplarIzq.Checked)
                AcoplarVentana(false);
            else
                AcoplarVentana(null);
        }

        private void CboOpacidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ = double.TryParse(cboOpacidad.SelectedItem.ToString(), out double transp);

            MySettings.Opacidad = transp;

            if (MySettings.Opacidad > 20)
                this.Opacity = MySettings.Opacidad / 100;
            else
            {
                MySettings.Opacidad = this.Opacity;
                inicializando = true;
                cboOpacidad.Text = MySettings.Opacidad.ToString();
                inicializando = false;
            }
        }

        private void BtnSplitDrop_DropDownOpening(object sender, EventArgs e)
        {
            //// Si está el control box del formulario, ocultar el menú cerrar
            //// y viceversa
            //toolSeparatorCerrar.Visible = !this.ControlBox;
            //mnuCerrar.Visible = !this.ControlBox;
            if(mnuTamañoPequeño.Checked)
                mnuTamañoPequeño.Text = "Cambiar al tamaño normal";
            else
                mnuTamañoPequeño.Text = "Cambiar al tamaño a pequeño";
        }

        private void MnuRecordarPosicion_Click(object sender, EventArgs e)
        {
            if (inicializando) return;

            mnuRecordarPosicion.Checked = !mnuRecordarPosicion.Checked;
            MySettings.RecordarPos = mnuRecordarPosicion.Checked;
        }

        private void MnuTopMost_Click(object sender, EventArgs e)
        {
            if (inicializando) return;

            mnuTopMost.Checked = !mnuTopMost.Checked;

            this.TopMost = mnuTopMost.Checked;
            MySettings.SiempreEncima = this.TopMost;
        }

        private void MnuAcoplarMinimo_Click(object sender, EventArgs e)
        {
            if (inicializando) return;

            mnuAcoplarMinimo.Checked = !mnuAcoplarMinimo.Checked;
            MySettings.AcoplarMinimo = mnuAcoplarMinimo.Checked;
            AcoplarVentana(acoplarDonde);
        }

        private void MnuCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MnuSalvaPantalla_Click(object sender, EventArgs e)
        {
            if (inicializando) return;

            // Guardar la posición y tamaño
            tamVentana = (this.Left, this.Top, this.Width, this.Height);

            mnuSalvapantalla.Checked = !mnuSalvapantalla.Checked;
            MySettings.UsarComoSalvaPantalla = mnuSalvapantalla.Checked;

            timerSalvaPantalla.Enabled = MySettings.UsarComoSalvaPantalla;

            if (MySettings.UsarComoSalvaPantalla)
            {
                // Quitar el borde
                this.FormBorderStyle = FormBorderStyle.None;

                topMostAnt = this.TopMost;
                this.TopMost = true;

                opacidadAnt = MySettings.Opacidad;
                this.Opacity = MySettings.OpacidadAcopleySalvaP;

                salvaPantallaActivo = true;
            }
            else
            { 
            }
        }

        /// <summary>
        /// Desactivar el salva pantalla al hacer doble clic en la fec o la hora
        /// </summary>
        private void LabelFechaHora_DoubleClick(object sender, EventArgs e)
        {
            timerSalvaPantalla.Enabled = false;

            mnuSalvapantalla.Checked = false;
            this.TopMost = topMostAnt;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Opacity = opacidadAnt;

            // Posicionarlo donde estaba
            this.Left = tamVentana.Left;
            this.Top = tamVentana.Top;
            this.Width = tamVentana.Width;
            this.Height = tamVentana.Height;

            salvaPantallaActivo = false;

            mnuTamañoPequeño.Checked = false;
        }

        private void MnuAcoplarTransparente_Click(object sender, EventArgs e)
        {
            if (inicializando) return;

            mnuAcoplarTransparente.Checked = !mnuAcoplarTransparente.Checked;
            MySettings.AcoplarTransparente = mnuAcoplarTransparente.Checked;
            AcoplarVentana(acoplarDonde);
        }

        private void MnuTamañoPequeño_Click(object sender, EventArgs e)
        {
            if (inicializando) return;

            //tamAnt = (this.Left, this.Top, this.Width, this.Height);
            mnuTamañoPequeño.Checked = !mnuTamañoPequeño.Checked;
            if (mnuTamañoPequeño.Checked)
            {
                PonerTamañoAcoplar(true);
            }
            else
            {
                inicializando = true;
                LabelFechaHora_DoubleClick(null, null);
                AjustarTamañoFechaHora();
                inicializando = false;
                //// Posicionarlo donde estaba
                //this.Left = tamVentana.Left;
                //this.Top = tamVentana.Top;
                //this.Width = tamVentana.Width;
                //this.Height = tamVentana.Height;
            }
        }

        private void mnuAcercaDe_Click(object sender, EventArgs e)
        {
            var msg = @$"Acerca de {Application.ProductName} v{Application.ProductVersion}

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

¡Disfrútala! :-)";
            MessageBox.Show(msg, $"Acerca de {Application.ProductName} v{Application.ProductVersion}", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
