using FinancialCrm.Models;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FinancialCrm
{
    public partial class FrmBilling : Form
    {
        public FrmBilling()
        {
            InitializeComponent();
            this.MouseDown += FrmBilling_MouseDown;
        }

        #region FormBorderStyle None İle Sürükleme İşlemi
        private void FrmBilling_MouseDown(object sender, MouseEventArgs e)
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


        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        #endregion

        FinancialCrmEntities db = new FinancialCrmEntities();
        private void FrmBilling_Load(object sender, EventArgs e)
        {

            dataGridView1.DataSource = db.Bills.ToList();
        }

        private void btnBillList_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Bills.ToList();
        }

        private void btnCreateBill_Click(object sender, EventArgs e)
        {
            string title = txtBillTitle.Text;
            decimal amount = decimal.Parse(txtBillAmount.Text);
            string date = txtBillPeriod.Text;

            db.Bills.Add(new Bills
            {
                BillTitle = title,
                BillAmount = amount,
                BillPeriod = date
            });
            db.SaveChanges();
            dataGridView1.DataSource = db.Bills.ToList();
            MessageBox.Show("Fatura Başarıyla Kaydedildi !","Fatura Kaydı",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }

        private void btnRemoveBill_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtBillId.Text);
            var removeValue = db.Bills.Find(id);
            db.Bills.Remove(removeValue);
            db.SaveChanges();
            MessageBox.Show("Fatura Silindi !", "Fatura Silme", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            var finallyValue = db.Bills.ToList();
            dataGridView1.DataSource = finallyValue;
        }

        private void btnUpdateBill_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtBillId.Text);
            string title = txtBillTitle.Text;
            decimal amount = decimal.Parse(txtBillAmount.Text);
            string date = txtBillPeriod.Text;

            var values = db.Bills.Find(id);
            values.BillTitle = title;
            values.BillAmount = amount;
            values.BillPeriod = date;

            db.SaveChanges();
            dataGridView1.DataSource = db.Bills.ToList();
            MessageBox.Show("Fatura Başarıyla Kaydedildi !", "Fatura Kaydı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnBanksForm_Click(object sender, EventArgs e)
        {
            FrmBanks frmBanks = new FrmBanks();
            frmBanks.Show();
            this.Hide();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmDashboard frmDashboard = new FrmDashboard();
            frmDashboard.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmBankProcess frmBankProcess = new FrmBankProcess();
            frmBankProcess.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSpendings frmSpendings = new FrmSpendings();
            frmSpendings.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmCategories frmCategories = new FrmCategories();
            frmCategories.Show();
        }
    }
}
