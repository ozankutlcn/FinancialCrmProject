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
            txtPassword.PasswordChar = '*';
        }
        private bool isPasswordVisible = false;

        FinancialCrmEntities db = new FinancialCrmEntities();
       

        private void button2_Click(object sender, EventArgs e)
        {
            
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





        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == txtUsername.Text);
            var userNameText = txtUsername.Text;
            var userPasswordText = txtPassword.Text;
            if (user != null)
            {
                MessageBox.Show("Böyle Bir Kullanıcı Zaten Var!","Hatalı Kullanıcı !",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Lütfen Boş Alan Bırakmayınız!","Uyarı !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                db.Users.Add(new Users
                {
                    Username = txtUsername.Text,
                    Password = txtPassword.Text // Şifreyi hashleyerek saklamayı unutmayın!
                });
                db.SaveChanges();
                MessageBox.Show("Kullanıcınız Başarıyla Oluşturulmuştur!","Başarılı !",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isPasswordVisible)
            {
                // Nokta şeklinde görünmesi için
                txtPassword.PasswordChar = '*';
            }
            else
            {
                // Düz metin olarak göster
                txtPassword.PasswordChar = '\0'; // Şifre gizlemeyi kaldır
            }

            // Görünürlük durumunu değiştir
            isPasswordVisible = !isPasswordVisible;
        }
    }
    
}
