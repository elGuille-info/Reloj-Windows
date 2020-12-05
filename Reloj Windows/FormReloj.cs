//-----------------------------------------------------------------------------
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
    public partial class FormReloj : Form
    {
        private readonly System.Reflection.Assembly ensamblado;
        private readonly FileVersionInfo fvi;

        /// <summary>
        /// La información de este programa
        /// </summary>
        private readonly string InfoApp;

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

        /// <summary>
        /// Asignar true si se va a cambiar el tamaño después de estar acoplada.
        /// </summary>
        private bool estabaAcoplada;

        /// <summary>
        /// El tamaño y posición de la ventana
        /// </summary>
        private (int Left, int Top, int Width, int Height) tamVentana;

        /// <summary>
        /// Para la posición anterior a acoplar
        /// asignarlo solo si no está acoplado,
        /// por ejemplo, si estaba acoplado a la derecha y
        /// se cambia a la izquierda, no asignar la posición
        /// </summary>
        private (int Left, int Top, int Width, int Height) tamAnt;

        /// <summary>
        /// Los textos informativos según se muestre la ventana más ancha o más estrecha
        /// </summary>
        private (string Largo, string Corto) textoInfo;

        // Para el salvapantallas
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
        /// Si está en modo salvapantalla
        /// </summary>
        private bool salvaPantallaActivo;

        public FormReloj()
        {
            inicializando = true;

            InitializeComponent();

            ActivoDesde = DateTime.Now;

            ensamblado = System.Reflection.Assembly.GetExecutingAssembly();
            fvi = FileVersionInfo.GetVersionInfo(ensamblado.Location);
            InfoApp = $"{Application.ProductName} v{Application.ProductVersion} ({fvi.FileVersion})";

            // Leer los valores de la configuración

            if (MySettings.AcoplarDonde == -1)
                acoplarDonde = null;
            else if (MySettings.AcoplarDonde == 0)
                acoplarDonde = false;
            else if (MySettings.AcoplarDonde == 1)
                acoplarDonde = true;

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

            textoInfo.Corto = $"{InfoApp}";
            textoInfo.Largo = $"{textoInfo.Corto} - (c)Guillermo Som (elGuille), 2020{s}";

            LabelInfo.Text = textoInfo.Largo;

            // Actualizar el salvapantalla cada 3 segundos
            timerSalvaPantalla.Interval = 3000;
            timerSalvaPantalla.Enabled = false;

            // Actualizar la fecha y hora cada 0.8 segundos
            timerActualizarFechaHora.Interval = 800;
            timerActualizarFechaHora.Enabled = true;

            // Asignar el valor antes de inicializar
            // por si se ha acoplado
            tamAnt = (this.Left, this.Top, this.Width, this.Height);

            Inicializar();

            inicializando = false;
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

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // Ctrl+Shit+P inicia o cancela el salvapantalla
            if (!e.Alt && e.Shift && e.Control && e.KeyCode == Keys.P)
            {
                //CancelarSalvapantalla();
                //MnuSalvapantalla_Click(null, null);
                MostrarSalvapantalla();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Form1_Move(object sender, EventArgs e)
        {
            // Solo guardar la posición y tamaño si está en modo normal
            // no está como salvapantallas ni está acoplado
            if (this.WindowState == FormWindowState.Normal && !salvaPantallaActivo && 
                        acoplarDonde is null && !mnuTamañoPequeño.Checked)
            {
                tamVentana = (this.Left, this.Top, this.Width, this.Height);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (inicializando) return;

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
        /// Iniciar o detener el salvapantalla.
        /// Si al detenerlo se quiere cerrar la ventana, indicar true en cerrar.
        /// </summary>
        /// <param name="mostrar">True para iniciar el salvapantalla, false para detenerlo.</param>
        /// <param name="cerrar">Si al detenerlo se quiere cerrar la ventana, indicar true en cerrar. 
        /// No se tiene en cuenta si mostrar es true.
        /// </param>
        /// <remarks>Método público para usarlo desde otra aplicaciones.</remarks>
        public void Salvapantalla(bool mostrar, bool cerrar)
        {
            MySettings.UsarComoSalvaPantalla = mostrar;
            MySettings.OpacidadAcopleySalvaP = 0.75;
            MostrarSalvapantalla();

            if (!mostrar && cerrar)
                this.Close();
        }

        /// <summary>
        /// Iniciar o detener el salvapantalla
        /// </summary>
        private void MostrarSalvapantalla()
        {
            if (inicializando) return;

            mnuSalvapantalla.Checked = !mnuSalvapantalla.Checked;
            MySettings.UsarComoSalvaPantalla = mnuSalvapantalla.Checked;

            if (MySettings.UsarComoSalvaPantalla)
            {
                timerSalvaPantalla.Enabled = MySettings.UsarComoSalvaPantalla;

                // Guardar la posición y tamaño
                tamVentana = (this.Left, this.Top, this.Width, this.Height);

                // Quitar el borde
                this.FormBorderStyle = FormBorderStyle.None;

                topMostAnt = this.TopMost;
                this.TopMost = true;

                opacidadAnt = MySettings.Opacidad;
                if (MySettings.OpacidadAcopleySalvaP < 0.50)
                    MySettings.OpacidadAcopleySalvaP = 0.75;
                this.Opacity = MySettings.OpacidadAcopleySalvaP;

                salvaPantallaActivo = true;
            }
            else
                CancelarSalvapantalla();
        }

        /// <summary>
        /// Ajustar el tamaño de la fecha/hora y el contenido de la fecha
        /// </summary>
        private void AjustarTamañoFechaHora()
        {
            var d = DateTime.Now;

            // TODO
            // Ajustar el tamaño de la fuente del reloj si es ancho, pero menos alto

            // El tamaño mínimo de la ventana es: W=350, H = 180

            if (this.Height > 190)
            {
                //// Si ya estaba acoplada, no asignar el tamaño
                //if (!estabaAcoplada || mnuTamañoPequeño.Checked)
                //{
                //}
                labelFecha.Font = new Font(labelFecha.Font.FontFamily, 37, FontStyle.Bold);
                labelFecha.Height = 55;
                labelHora.Height = this.ClientSize.Height - labelFecha.Height - statusStrip1.Height - 60;
            }

            if (this.Width < 400)
            {
                //if (!(acoplarDonde is null))
                LabelInfo.Text = ""; // textoInfo.Corto;
                this.ControlBox = false;
            }
            else
            {
                LabelInfo.Text = textoInfo.Largo;
                this.ControlBox = true;
            }

            float fs = (float)(this.Width / 7); // 6.7
            labelHora.Font = new Font(labelHora.Font.FontFamily, fs, FontStyle.Bold);

            if (this.Width < 500)
                labelFecha.Text = d.ToString("dd.MMMyyyy");
            else if (this.Width < 650)
                labelFecha.Text = d.ToString("ddd, dd.MMMyyyy");
            else if (this.Width < 750)
                labelFecha.Text = d.ToString("dddd, dd.MMMyyyy");
            else
                labelFecha.Text = d.ToString("dddd, dd MMMM yyyy");
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
            inicializando = true;

            if (acoplarDonde is null)
                this.tamAnt = (this.Left, this.Top, this.Width, this.Height);

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
                estabaAcoplada = false;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                estabaAcoplada = true;

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

            acoplarDonde = donde;

            inicializando = false;
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
        /// Cancelar el salvapantalla.
        /// </summary>
        private void CancelarSalvapantalla()
        {
            timerSalvaPantalla.Enabled = false;

            mnuSalvapantalla.Checked = false;
            this.TopMost = topMostAnt;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Opacity = opacidadAnt;

            if (!mnuTamañoPequeño.Checked)
            {
                // Posicionarlo donde estaba si no está marcado el tamaño pequeño
                //this.Left = tamVentana.Left;
                //if (this.Left > Screen.PrimaryScreen.WorkingArea.Width - this.Width)
                //    this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
                //this.Top = tamVentana.Top;
                //this.Width = tamVentana.Width;
                //this.Height = tamVentana.Height;
                this.Left = tamAnt.Left;
                if (this.Left > Screen.PrimaryScreen.WorkingArea.Width - this.Width)
                    this.Left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
                this.Top = tamAnt.Top;
                this.Width = tamAnt.Width;
                this.Height = tamAnt.Height;

            }

            salvaPantallaActivo = false;

            mnuTamañoPequeño.Checked = false;
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
                tamAnt = (this.Left, this.Top, this.Width, this.Height);

            mnuAcoplarDer.Checked = MySettings.AcoplarDonde == 1;
            mnuAcoplarIzq.Checked = MySettings.AcoplarDonde == 0;
            mnuAcoplarMinimo.Checked = MySettings.AcoplarMinimo;
            mnuRecordarPosicion.Checked = MySettings.RecordarPos;
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
            if (e.Button == MouseButtons.Left)
            {
                ratonPulsado = true;
                pX = e.X;
                pY = e.Y;
            }
        }

        private void MoverForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (ratonPulsado)
            {
                this.Left += e.X - pX;
                this.Top += e.Y - pY;
            }
        }

        private void MoverForm_MouseUp(object sender, MouseEventArgs e)
        {
            ratonPulsado = false;
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
            // Poner a tamaño pequeño o restaurar
            if (mnuTamañoPequeño.Checked)
                mnuTamañoPequeño.Text = "Cambiar al tamaño normal";
            else
                mnuTamañoPequeño.Text = "Cambiar al tamaño a pequeño";
            
            mnuTamañoPequeño.Enabled = !estabaAcoplada;

            // El salvapantalla
            if (mnuSalvapantalla.Checked)
                mnuSalvapantalla.Text = "Detener salvapantalla";
            else
                mnuSalvapantalla.Text = "Iniciar salvapantalla";
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

        private void MnuSalvapantalla_Click(object sender, EventArgs e)
        {
            MostrarSalvapantalla();
        }

        /// <summary>
        /// Desactivar el salvapantalla al hacer doble clic en la fec o la hora
        /// </summary>
        private void LabelFechaHora_DoubleClick(object sender, EventArgs e)
        {
            CancelarSalvapantalla();
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

            mnuTamañoPequeño.Checked = !mnuTamañoPequeño.Checked;
            if (estabaAcoplada) return;

            if (mnuTamañoPequeño.Checked)
            {
                PonerTamañoAcoplar(true);
            }
            else
            {
                inicializando = true;
                CancelarSalvapantalla();
                AjustarTamañoFechaHora();
                inicializando = false;
            }
        }

        private void MnuAcercaDe_Click(object sender, EventArgs e)
        {
            var versionWeb = "xx";
            var msgVersion = $"{"\r\n"}{"\r\n"}Esta versión de '{Application.ProductName}' es la más reciente.";
            
            var cualVersion = VersionUtilidades.CompararVersionWeb(ensamblado, ref versionWeb);
            
            if (cualVersion == 1)
                msgVersion = $"{"\r\n"}{"\r\n"}En la web hay una versión más reciente: {versionWeb}";
            else if (cualVersion == -1)
                msgVersion = $"{"\r\n"}{"\r\n"}Esta versión de '{Application.ProductName}' es más reciente que la de la web: {versionWeb}";

            var titulo = $"Acerca de {InfoApp }";

            var msg = @$"{titulo}

Esta aplicación es un 'simple' reloj que te muestra la fecha y la hora en una ventana.

Te permite controlar dónde verla, si a tamaño normal, a tamaño pequeño, incluso puedes cambiar el tamaño a tu gusto.

Puedes hacer que esté encima del resto de ventanas.

Además puedes hacerla menos opaca (más transparente) para que pueda verse lo que hay debajo de la ventana.
El nivel de transparencia lo puedes controlar a tu gusto, desde casi totalmente transparente a totalmente opaca.

Puedes usarla también como una especie de salvapantallas (se va desplazando por la pantalla), pero con transparencia con idea de que veas lo que hay debajo sin tener que detener la aplicación.
Si quieres dejar de ver cómo se mueve por la pantalla, simplemente haz doble-clic en la fecha o en la hora.
El salvapantallas lo puedes usar a tamaño normal o en tamaño pequeño.
Cuando funciona como salvapantalla siempre está encima del resto de las ventanas y no muestra la barra de título.

La ventana la puedes mover desde la barra de título o bien manteniendo el ratón pulsado en la fecha o en la hora.

También puedes acoplarla en la parte superior izquierda o derecha, en esos casos, se cambia a tamaño mínimo y se hace transparente.

Y esto es casi todo... creo... en el menú de opciones (en el botón que hay abajo a la derecha) están todas las opciones de uso disponibles.

¡Disfrútala! :-)" + msgVersion;
            MessageBox.Show(msg, titulo, 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
