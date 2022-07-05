using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DWParentalControls
{
    public partial class fMain : Form
    {
        string appDataPath = "";
        KeyboardHook hook = new KeyboardHook();
        const string codeFileName = "ManagerCode.sec";
        FileStream mngCodeLocker;
        string mngCode = "";

        public fMain()
        {
            InitializeComponent();
            // register the event that is fired after the key press.
            hook.KeyPressed +=
                new EventHandler<KeyPressedEventArgs>(hook_KeyPressed);
            // register the CONTROL + SHIFT + F12 combination as hot key.
            hook.RegisterHotKey(DWParentalControls.ModifierKeys.Control | DWParentalControls.ModifierKeys.Shift,
                Keys.F12);

            appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ParentalControls");
            if (!Directory.Exists(appDataPath))
                Directory.CreateDirectory(appDataPath);

            this.WindowState = FormWindowState.Minimized;
            this.StartPosition = FormStartPosition.CenterScreen;
        }
        void hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (mngCode.Length > 0)
            {
                int maxTry = 3;
                int errTry = 0;
                while (true)
                {
                    using (var f = new fLogin())
                    {
                        if (f.ShowDialog(this) == DialogResult.OK)
                        {
                            if (f.EncPWD != mngCode)
                            {
                                MessageBox.Show("密碼錯誤");
                                errTry++;
                            }
                        }
                        else
                            return;
                    }

                    if (errTry >= maxTry) return;
                }
            }

            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
            Application.DoEvents();
        }

        private void fMain_Load(object sender, EventArgs e)
        {
            this.Hide();
            Application.DoEvents();

            mngCodeLocker = new FileStream(FilePath(codeFileName), FileMode.OpenOrCreate, FileAccess.ReadWrite);
            using (var sr = new StreamReader(mngCodeLocker))
                mngCode = sr.ReadToEnd();

        }

        string ReadFile(string fn)
        {
            return File.ReadAllText(Path.Combine(appDataPath, fn));
        }
        bool FileExist(string fn)
        {
            return File.Exists(FilePath(fn));
        }

        string FilePath(string fn)
        {
            return Path.Combine(appDataPath, fn);
        }

        private void fMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            UpdateMngCode(false);
        }

        void UpdateMngCode(bool reopen = true)
        {
            mngCodeLocker?.Close();
            File.WriteAllText(FilePath(codeFileName), mngCode);
            if (reopen)
                mngCodeLocker = new FileStream(FilePath(codeFileName), FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        private void fMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Hide();
                e.Cancel = true;
            }
        }

        private void tsmiSetMngCode_Click(object sender, EventArgs e)
        {
            using (var f = new fSetMngCode())
            {
                f.OldPwd = mngCode;
                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    mngCode = f.NewPwd;
                    UpdateMngCode();
                    MessageBox.Show("密碼變更完成。");
                }
            }
        }
    }
}
