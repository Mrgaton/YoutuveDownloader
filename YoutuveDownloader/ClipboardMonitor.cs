using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ClipboardAssist
{
    // https://stackoverflow.com/a/1394225/14735918

    [DefaultEvent("ClipboardChanged")]
    public partial class ClipboardMonitor : Control
    {
        public ClipboardMonitor()
        {
            this.BackColor = Color.Red;
            this.Visible = false;
        }

        /// <summary>
        /// Clipboard contents changed.
        /// </summary>
    }
}