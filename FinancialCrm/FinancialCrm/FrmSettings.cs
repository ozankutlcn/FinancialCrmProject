using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinancialCrm
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
        }

        private bool isDarkMode = false;

        private void pbDarkMode_MouseClick(object sender, MouseEventArgs e)
        {
            //FrmSettings.ActiveForm.BackColor = Color.FromArgb(37, 37, 38);

            if (isDarkMode)
            {
                foreach (Form openForm in Application.OpenForms)
                {
                    openForm.BackColor = Color.White; // Light Mode arka plan rengi
                }
                lblOnOff.Text = "Dark Mode: Off";
                isDarkMode = false; // Durumu güncelle
            }
            else
            {
                // Eğer şu an Light Mode ise Dark Mode'a geç
                foreach (Form openForm in Application.OpenForms)
                {
                    openForm.BackColor = Color.FromArgb(37, 37, 38); // Dark Mode arka plan rengi
                }
                lblOnOff.Text = "Dark Mode: On";
                isDarkMode = true; // Durumu güncelle
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.Show();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {

        }

        private void FrmSettings_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        // WinAPI fonksiyonlarını tanımla
        [DllImport("user32.dll")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
    }
}
