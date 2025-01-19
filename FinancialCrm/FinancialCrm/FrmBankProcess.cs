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
    public partial class FrmBankProcess : Form
    {
        public FrmBankProcess()
        {
            InitializeComponent();
        }

        FinancialCrmEntities db = new FinancialCrmEntities();
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnBankProcess_Click(object sender, EventArgs e)
        {
            
        }

        #region CRUD İşlemleri
        private void btnList_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.BankProcesses.ToList();
            dataGridView1.Columns["BankId"].Visible = false;
            dataGridView1.Columns["Banks"].Visible = false;

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int deletedValueId = int.Parse(txtBankProcessId.Text);
            db.BankProcesses.Remove(db.BankProcesses.Find(deletedValueId));
            db.SaveChanges();
            MessageBox.Show("Banka Hareketi Başarıyla Silindi !");
            dataGridView1.DataSource = db.BankProcesses.ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string processDescription = txtDescription.Text;
            DateTime dateTime = DateTime.Parse(txtProcessDate.Text);
            string processType = txtProcessType.Text; 
            int bankAmount = int.Parse(txtAmount.Text);

            db.BankProcesses.Add(new BankProcesses
            {
                ProcessType = processType,
                ProcessDate = dateTime,
                Description = processDescription,
                Amount = bankAmount
            });

            db.SaveChanges();
            MessageBox.Show("Banka Hareketi Başarıyla Eklendi","Ekleme Başarılı !", MessageBoxButtons.OK,MessageBoxIcon.Information);
            dataGridView1.DataSource = db.BankProcesses.ToList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int updatedValueId = int.Parse(txtBankProcessId.Text);
            string processDescription = txtDescription.Text;
            DateTime dateTime = DateTime.Parse(txtProcessDate.Text);
            string processType = txtProcessType.Text;
            int bankAmount = int.Parse(txtAmount.Text);

            var value = db.BankProcesses.Find(updatedValueId);
            value.Description = processDescription;
            value.ProcessDate = dateTime;
            value.ProcessType = processType;
            value.Amount = bankAmount;
            db.SaveChanges();
            MessageBox.Show("Banka Hareketi Başarıyla Güncellendi !", "Güncelleme Başarılı !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            dataGridView1.DataSource = db.BankProcesses.ToList();
        }
        #endregion

        private void btnBills_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmBilling frm = new FrmBilling();
            frm.Show();
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmCategories frm = new FrmCategories();
            frm.Show();

        }

        private void btnBanks_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmBanks frm = new FrmBanks();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSpendings frm = new FrmSpendings();
            frm.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmDashboard frm = new FrmDashboard();
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmSettings frm = new FrmSettings();
            frm.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
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
