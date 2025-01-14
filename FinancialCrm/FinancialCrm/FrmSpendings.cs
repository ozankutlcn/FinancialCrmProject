using FinancialCrm.Models;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace FinancialCrm
{
    public partial class FrmSpendings : Form
    {
        public FrmSpendings()
        {
            InitializeComponent();
            
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
        #endregion

        FinancialCrmEntities db = new FinancialCrmEntities();

        private void FrmSpendings_Load(object sender, EventArgs e)
        {
            var spendingsDatas = db.Spendings.ToList();
            dataGridView1.DataSource = spendingsDatas;
            dataGridView1.Columns["CategoryId"].Visible = false;
            dataGridView1.Columns["Categories"].Visible = false;
        }


        int count = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            count++;
            if (count % 4 == 1)
            {
                var tansasAlısverisHarcama = db.Spendings.Where(x => x.SpendingTitle == "Tansaş Alışveriş")
                    .Select(y => y.SpendingAmount).FirstOrDefault();
                lblTotalSpending.Text = tansasAlısverisHarcama.ToString() + "₺";

                var tansasAlısverisTitle = db.Spendings.Where(y => y.SpendingTitle == "Tansaş Alışveriş").Select(y => y.SpendingTitle).FirstOrDefault();
                lblSpendingTitle.Text = tansasAlısverisTitle;

                var tansasHarcamaTarihi = db.Spendings.Where(z => z.SpendingTitle == "Tansaş Alışveriş").Select(x => x.SpendingDate).FirstOrDefault();
                lblLastSpendingDate.Text = tansasHarcamaTarihi.HasValue ? tansasHarcamaTarihi.Value.ToString("yyyy-MM-dd") : "Tarih bulunamadı";
            }
            if (count % 4 == 2)
            {
                var damacanaSuHarcama = db.Spendings.Where(x => x.SpendingTitle == "Damacana Su")
                    .Select(y => y.SpendingAmount).FirstOrDefault();
                lblTotalSpending.Text = damacanaSuHarcama.ToString() + "₺";

                var damacanaSuHarcamaTitle = db.Spendings.Where(y => y.SpendingTitle == "Damacana Su").Select(x => x.SpendingTitle).FirstOrDefault();
                lblSpendingTitle.Text = damacanaSuHarcamaTitle;

                var damacanaHarcamaTarihi = db.Spendings.Where(z => z.SpendingTitle == "Damacana Su").Select(x => x.SpendingDate).FirstOrDefault();
                lblLastSpendingDate.Text = damacanaHarcamaTarihi.HasValue ? damacanaHarcamaTarihi.Value.ToString("yyyy-MM-dd") : "Tarih bulunamadı";
            }
            if (count % 4 == 3)
            {
                var aylikAkbilHarcama = db.Spendings.Where(x => x.SpendingTitle == "Aylık Akbil")
                    .Select(y => y.SpendingAmount).FirstOrDefault();
                lblTotalSpending.Text = aylikAkbilHarcama.ToString() + "₺";

                var aylikAkbilHaracamaTitle = db.Spendings.Where(y => y.SpendingTitle == "Aylık Akbil").Select(x => x.SpendingTitle).FirstOrDefault();
                lblSpendingTitle.Text = aylikAkbilHaracamaTitle;

                var aylikAkbilHaracamaTarihi = db.Spendings.Where(z => z.SpendingTitle == "Aylık Akbil").Select(x => x.SpendingDate).FirstOrDefault();
                lblLastSpendingDate.Text = aylikAkbilHaracamaTarihi.HasValue ? aylikAkbilHaracamaTarihi.Value.ToString("yyyy-MM-dd") : "Tarih bulunamadı";
            }

        }

        private void btnBanks_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmBanks frmBanks = new FrmBanks();
            frmBanks.Show();
        }

        private void btnBills_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmBilling frmBilling = new FrmBilling();
            frmBilling.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmDashboard frmDashboard = new FrmDashboard();
            frmDashboard.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
