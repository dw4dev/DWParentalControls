using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DWParentalControls.Helpers;

namespace DWParentalControls
{
    public partial class fLogin : Form
    {
        public string EncPWD { get; set; } = "";

        public fLogin()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (Modal)
            {
                EncPWD = SecurityHelper.Text.EncryptToString(txtPWD.Text);

                DialogResult = DialogResult.OK;
            }
            else
                this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (Modal)
                DialogResult = DialogResult.Cancel;
            else
                this.Hide();

        }
    }
}
