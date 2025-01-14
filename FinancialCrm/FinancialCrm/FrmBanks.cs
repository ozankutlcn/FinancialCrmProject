using FinancialCrm.Models;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace FinancialCrm
{
    public partial class FrmBanks : Form
    {
        public FrmBanks()
        {
            InitializeComponent();
            // MouseDown olayını forma bağla
            this.MouseDown += FrmBanks_MouseDown;
        }
        FinancialCrmEntities db = new FinancialCrmEntities();

        #region FormBorderStyle None İle Sürükleme İşlemi
        // WinAPI fonksiyonlarını tanımla
        [DllImport("user32.dll")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private void FrmBanks_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture(); // Fare yakalama işlemini serbest bırak
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0); // Sürükleme komutu gönder
            }
        }

        

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        private void FrmBanks_Load(object sender, EventArgs e)
        {

            lblZiraatBankBalance.Text = db.Banks.Where(x => x.BankTitle == "Ziraat Bankası")
                .Select(x => x.BankBalance + "₺").FirstOrDefault().ToString();

            lblisBankasiBalance.Text = db.Banks.Where(x => x.BankTitle == "İş Bankası")
                .Select(x => x.BankBalance + "₺").FirstOrDefault().ToString();

            lblVakifbankBalance.Text = db.Banks.Where(x => x.BankTitle == "Vakıfbank")
                .Select(x => x.BankBalance + "₺").FirstOrDefault().ToString();


            var bankProcess1 = db.BankProcesses.OrderByDescending(x => x.BankProcessId) //BankProcesses Tablosunda ProcessId'yi tersten sıralar. Yani en son eklenen procces ilk sıraya gelir.
                .Take(1) // İlk sıradaki veriyi alır.
                .FirstOrDefault(); //Description alanını select eder.
            lblBankProcess1.Text = bankProcess1.Description + " | " + bankProcess1.Amount + " | " + bankProcess1.ProcessDate;

            var bankProcess2 = db.BankProcesses.OrderByDescending(x => x.BankProcessId)
                .Skip(1) //Tersten sıralamış olduğumuz verinin ilk sırasını atlar.
                .FirstOrDefault();
            lblBankProcess2.Text = bankProcess2.Description + " | " + bankProcess2.Amount + " | " + bankProcess2.ProcessDate;

            var bankProcess3 = db.BankProcesses.OrderByDescending(x => x.BankProcessId)
                .Skip(2) //Tersten sıralamış olduğumuz verinin ilk iki sırasını atlar.
                .FirstOrDefault();
            lblBankProcess3.Text = bankProcess3.Description + " | " + bankProcess3.Amount + " | " + bankProcess3.ProcessDate; 

            var bankProcess4 = db.BankProcesses.OrderByDescending(x => x.BankProcessId)
               .Skip(3) //Tersten sıralamış olduğumuz verinin ilk üç sırasını atlar.
               .FirstOrDefault();
            lblBankProcess4.Text = bankProcess4.Description + " | " + bankProcess4.Amount + " | " + bankProcess4.ProcessDate;

            var bankProcess5 = db.BankProcesses.OrderByDescending(x => x.BankProcessId)
               .Skip(4) //Tersten sıralamış olduğumuz verinin ilk dört sırasını atlar.
               .FirstOrDefault();
            lblBankProcess5.Text = bankProcess5.Description + " | " + bankProcess5.Amount + " | " + bankProcess5.ProcessDate;

        }

       
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            // Kalemi oluştur
            Pen blackPen = new Pen(ColorTranslator.FromHtml("#c8d6e5"),3);

            // Çizgi koordinatlarını belirle
            int x1 = 1;  // Başlangıç X noktası
            int x2 = 789; // Bitiş X noktası
            int y1 = 77;  // Başlangıç Y noktası
            int y2 = 77;  // Bitiş Y noktası

            // Çizgiyi ekrana çiz
            e.Graphics.DrawLine(blackPen, x1, y1, x2, y2); // Line 1
            e.Graphics.DrawLine(blackPen, 1, 135, 789, 135); // Line 2
            e.Graphics.DrawLine(blackPen, 1, 195, 789, 195); // Line 3
            e.Graphics.DrawLine(blackPen, 1, 255, 789, 255); // Line 4
            
           
        }


        private void FrmBanks_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmBilling frmBilling = new FrmBilling();
            frmBilling.Show();
            this.Hide();
        }

        private void FrmBanks_MouseDown_1(object sender, MouseEventArgs e)
        {
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSpendings frmSpendings = new FrmSpendings();
            frmSpendings.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmDashboard frmDashboard = new FrmDashboard();
            frmDashboard.Show();
        }

        private void btnBanks_Click(object sender, EventArgs e)
        {

        }
    }
}
