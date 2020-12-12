//-----------------------------------------------------------------------------
// Clase para extensiones                                           (12/Dic/20)
//
// (c) Guillermo (elGuille) Som, 2020
//-----------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reloj_Windows
{
    public static class Extensiones
    {

        //public static ToolStripMenuItem Clonar(this ToolStripItem mnu, EventHandler eClick, EventHandler eSelect = null)
        //{
        //    ToolStripMenuItem mnuC = new ToolStripMenuItem();
        //    mnuC.Click += eClick;
        //    if (eSelect != null)
        //        mnuC.DropDownOpening += eSelect;
        //    //mnuC.Checked = mnu.Checked;
        //    mnuC.Enabled = mnu.Enabled;
        //    mnuC.Font = mnu.Font;
        //    mnuC.Image = mnu.Image;
        //    mnuC.Name = mnu.Name;
        //    //mnuC.ShortcutKeys = mnu.ShortcutKeys;
        //    //mnuC.ShowShortcutKeys = mnu.ShowShortcutKeys;
        //    mnuC.Tag = mnu.Tag;
        //    mnuC.Text = mnu.Text;
        //    mnuC.ToolTipText = mnu.ToolTipText;

        //    return mnuC;
        //}

        /// <summary>
        /// Clonar un menú item del tipo <see cref="ToolStripMenuItem"/>.
        /// </summary>
        /// <param name="mnu">El <see cref="ToolStripMenuItem"/> al que se va a asignar el nuevo menú.</param>
        /// <param name="eClick">Manejador del evento Click.</param>
        /// <param name="eSelect">Manejador del evento DropDownOpening.</param>
        /// <returns>Una nueva copia del tipo <see cref="ToolStripMenuItem"/>.</returns>
        public static ToolStripMenuItem Clonar(this ToolStripMenuItem mnu, EventHandler eClick, EventHandler eSelect = null)
        {
            ToolStripMenuItem mnuC = new ToolStripMenuItem();
            mnuC.Click += eClick;
            if (eSelect != null)
                mnuC.DropDownOpening += eSelect;
            mnuC.Checked = mnu.Checked;
            mnuC.Enabled = mnu.Enabled;
            mnuC.Font = mnu.Font;
            mnuC.Image = mnu.Image;
            mnuC.Name = mnu.Name;
            mnuC.ShortcutKeys = mnu.ShortcutKeys;
            mnuC.ShowShortcutKeys = mnu.ShowShortcutKeys;
            mnuC.Tag = mnu.Tag;
            mnuC.Text = mnu.Text;
            mnuC.ToolTipText = mnu.ToolTipText;

            return mnuC;
        }
    }
}
