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
    public partial class fSetMngCode : Form
    {
        int maxTry = 3;
        int errTry = 0;

        public string OldPwd { get; set; }
        public string NewPwd { get; private set; }


        public fSetMngCode()
        {
            InitializeComponent();
        }



        private void fSetMngCode_Load(object sender, EventArgs e)
        {
            txtOldPwd.Enabled = (OldPwd != "");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (Modal)
                DialogResult = DialogResult.Cancel;
            else
                this.Hide();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(OldPwd != "")
            {
                if(SecurityHelp.Text.EncryptToString(txtOldPwd.Text) != OldPwd)
                {
                    errTry++;
                    if(errTry >= maxTry)
                    {
                        MessageBox.Show("已達錯誤次數！\n請稍後重新設定。");
                        DialogResult = DialogResult.Cancel;
                    }
                    else
                        MessageBox.Show("舊密碼不正確！\n請確認後再重新輸入。");
                    return;
                }
            }
            if(txtNewPWd.Text == txtNewPwd2.Text)
            {
                NewPwd = SecurityHelp.Text.EncryptToString(txtNewPWd.Text);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("新密碼與確認新密碼不相符！\n請修正。");
            }
        }
    }
}
