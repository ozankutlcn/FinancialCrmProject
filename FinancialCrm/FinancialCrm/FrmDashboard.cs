using FinancialCrm.Models;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FinancialCrm
{
    public partial class FrmDashboard : Form
    {
        public FrmDashboard()
        {
            InitializeComponent();
            this.MouseDown += FrmDashboard_MouseDown;
        }

        #region FormBorderStyle None İle Sürükleme İşlemi
        // WinAPI fonksiyonlarını tanımla
        [DllImport("user32.dll")]
        public static extern void ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern void SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void FrmDashboard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture(); // Fare yakalama işlemini serbest bırak
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0); // Sürükleme komutu gönder
            }
        }

        #endregion

        
        FinancialCrmEntities db = new FinancialCrmEntities();
        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            lblTotalBalance.Text = db.Banks.Sum(x => x.BankBalance).ToString() + "₺";

            var lastBankProcessAmount = db.BankProcesses.OrderByDescending(x => x.BankProcessId)
                .Take(1).Select(y => y.Amount).FirstOrDefault();
            lblLastBankProcessAmount.Text = lastBankProcessAmount.ToString() + "₺";

            #region Chart 1 Kodları

            var bankData = db.Banks.Select(x => new //Newlememizin sebebi Banks tablosunda bulunan tüm verileri çekmek yerine sadece BankTitle ve BankBalance'ı çekmek. Direkt olarak Select içinde birden fazla alan döndüremiyoruz, çünkü Select her bir kaydı bir nesne olarak döndürmek zorunda.
            {
                x.BankTitle,
                x.BankBalance
            }).ToList();

            chart1.Series.Clear();
            var series = chart1.Series.Add("Series1");

            foreach (var item in bankData)
            {
                series.Points.AddXY(item.BankTitle, item.BankBalance); //X ekseni BankTitle'a eşit, Y ekseni BankBalance'a eşit
            }

            #endregion

            #region Chart 2 Kodları

            var billData = db.Bills.Select(x => new
            {
                x.BillTitle,
                x.BillAmount
            }).ToList();

            chart2.Series.Clear();
            var series2 = chart2.Series.Add("Faturalar");
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Renko;
            foreach (var item in billData) 
            {
                series2.Points.AddXY(item.BillTitle, item.BillAmount);
            }

            #endregion

        }


        int count = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            count++;
            if (count%4==1)
            {
                var elektrikFaturası = db.Bills.Where(x => x.BillTitle == "Elektrik Faturası")
                    .Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Elektrik Faturası";
                lblBillAmount.Text = elektrikFaturası.ToString() + "₺";
            }
            if (count % 4 == 2)
            {
                var dogalgazFaturası = db.Bills.Where(x => x.BillTitle == "Doğalgaz Faturası")
                    .Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Doğalgaz Faturası";
                lblBillAmount.Text = dogalgazFaturası.ToString() + "₺";
            }
            if (count % 4 == 3)
            {
                var suFaturası = db.Bills.Where(x => x.BillTitle == "Su Faturası")
                    .Select(y => y.BillAmount).FirstOrDefault();
                lblBillTitle.Text = "Su Faturası";
                lblBillAmount.Text = suFaturası.ToString() + "₺";
            }

        }

        private void btnBills_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmBilling frmBilling = new FrmBilling();
            frmBilling.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmBanks frmBanks = new FrmBanks();
            frmBanks.Show();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmCategories frmCategories = new FrmCategories();
            frmCategories.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSpendings frmSpendings = new FrmSpendings();
            frmSpendings.Show();
        }

        private void btnBankProcess_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmBankProcess frmBankProcess = new FrmBankProcess();
            frmBankProcess.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.Show();
        }
    }
}
