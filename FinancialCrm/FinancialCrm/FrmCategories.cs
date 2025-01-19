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
    public partial class FrmCategories : Form
    {
        public FrmCategories()
        {
            InitializeComponent();
        }
        FinancialCrmEntities db = new FinancialCrmEntities();
        private void FrmCategories_Load(object sender, EventArgs e)
        {
            
            dataGridView1.DataSource = db.Categories.ToList();
        }

        #region Crud İşlemleri
        private void btnCreateCategory_Click(object sender, EventArgs e)
        {
            string addedValue =  txtCategoryName.Text;
            db.Categories.Add(new Categories { CategoryName = addedValue });
            db.SaveChanges();
            dataGridView1.DataSource = db.Categories.ToList();
            MessageBox.Show("Category Added","Başarılı !",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int deletedValue = Convert.ToInt32(txtCategoryId.Text);
            var category = db.Categories.Find(deletedValue);
            db.Categories.Remove(category);
            db.SaveChanges();
            dataGridView1.DataSource = db.Categories.ToList();
            MessageBox.Show("Kategori Silindi !", "Başarılı !", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            var updatedValue = txtCategoryName.Text;
            db.Categories.Find(Convert.ToInt32(txtCategoryId.Text)).CategoryName = updatedValue;
            db.SaveChanges();
            dataGridView1.DataSource = db.Categories.ToList();
            MessageBox.Show("Kategori Güncellendi !", "Başarılı !", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnBanksForm_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmBanks frmBanks = new FrmBanks(); 
            frmBanks.Show();

        }

        private void btnBillsFrom_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmBilling frmBills = new FrmBilling();
            frmBills.Show();
        }

        private void btnSpendingsForm_Click(object sender, EventArgs e)
        {
            FrmSpendings frmSpendings = new FrmSpendings();
            frmSpendings.Show();
            this.Hide();

        }

        private void btnBankProccesFrom_Click(object sender, EventArgs e)
        {
            FrmBankProcess frmBankProcesses = new FrmBankProcess();
            frmBankProcesses.Show();
            this.Hide();

        }

        private void btnDashboardForm_Click(object sender, EventArgs e)
        {
            FrmDashboard frmDashboard = new FrmDashboard();
            frmDashboard.Show();
            this.Hide();

        }

        private void btnSettingsForm_Click(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
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
