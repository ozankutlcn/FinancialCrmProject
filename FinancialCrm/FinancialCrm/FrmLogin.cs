using FinancialCrm.Models;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FinancialCrm
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*'; // TextBox şifre modunda başlasın
        }
        private bool isPasswordVisible = false;


        FinancialCrmEntities db = new FinancialCrmEntities();
        

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register register = new Register();
            register.Show();
            
        }


        #region FormBorderStyle None İle Sürükleme İşlemi


        // WinAPI fonksiyonlarını tanımla
        [DllImport("user32.dll")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private void FrmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var user = db.Users.FirstOrDefault(u => u.Username == txtUsername.Text && u.Password == txtPassword.Text);
            if (user != null)
            {
                MessageBox.Show("Giriş Başarılı","Başarılı !",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Hide();
                FrmBilling billing = new FrmBilling();
                billing.Show();
            }
            else
            {
                MessageBox.Show("Yanlış Kullanıcı Adı Veya Şifre","Hata !",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

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
