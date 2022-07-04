using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DWParentalControls
{
    public partial class fMain : Form
    {
        KeyboardHook hook = new KeyboardHook();

        public fMain()
        {
            InitializeComponent();
            // register the event that is fired after the key press.
            hook.KeyPressed +=
                new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            // register the control + alt + F12 combination as hot key.
            hook.RegisterHotKey(DWParentalControls.ModifierKeys.Control | DWParentalControls.ModifierKeys.Alt,
                Keys.F12);
        }
        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
