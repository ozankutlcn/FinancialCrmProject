using FinancialCrm.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinancialCrm
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        FinancialCrmEntities db = new FinancialCrmEntities();
        private void button1_Click(object sender, EventArgs e)
        {
            db.Users.Add(new Users {  Username = txtUsername.Text, Password = txtPassword.Text, });
            MessageBox.Show("User added");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //if(txtUsername.Text == "admin" && txtPassword.Text == "admin")
            //{
            //    this.Hide();
            //    FrmAdminPanel adminPanel = new FrmAdminPanel();
            //    adminPanel.Show();
            //}
            //else
            //{
            //    var user = db.Users.FirstOrDefault(x => x.Username == txtUsername.Text && x.Password == txtPassword.Text);
            //    if (user != null)
            //    {
            //        this.Hide();
            //        FrmLogin userLogin = new FrmLogin();
            //        userLogin.Show();
            //    }
            //    else
            //    {
            //        MessageBox.Show("User not found");
            //    }
            //}
            this.Hide();
            FrmLogin login = new FrmLogin();
            login.Show();

        }

        #region FormBorderStyle None İle Sürükleme İşlemi

        // WinAPI fonksiyonlarını tanımla
        [DllImport("user32.dll")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private void Register_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion
    }
}
