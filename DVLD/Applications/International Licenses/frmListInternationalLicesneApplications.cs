using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BLL;
namespace DVLD
{
    public partial class frmListInternationalLicesneApplications : Form
    {
        public frmListInternationalLicesneApplications()
        {
            InitializeComponent();
        }

        private DataTable _dtInternationalApplications= DVLD_BLL.clsInternationaLicense.GetAllInternationalLicecnse();

        void _CountRecords()
        {
            lblRecords.Text=dgvInternationalLicenseApplications.Rows.Count.ToString();
        }
        void _LoadDataGridView()
        {
            
            dgvInternationalLicenseApplications.DataSource= _dtInternationalApplications;
            if(dgvInternationalLicenseApplications.Rows.Count > 0)
            {
                dgvInternationalLicenseApplications.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicenseApplications.Columns[0].Width = 150;

                dgvInternationalLicenseApplications.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicenseApplications.Columns[1].Width = 150;

                dgvInternationalLicenseApplications.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenseApplications.Columns[2].Width = 120;

                dgvInternationalLicenseApplications.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenseApplications.Columns[3].Width = 100;

                dgvInternationalLicenseApplications.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenseApplications.Columns[4].Width = 250;

                dgvInternationalLicenseApplications.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicenseApplications.Columns[5].Width = 250;

                dgvInternationalLicenseApplications.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenseApplications.Columns[6].Width = 100;
            }


        }

        void _LoadComboBox()
        {
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("International License ID");
            cbFilter.Items.Add("Application ID");
            cbFilter.Items.Add("Driver ID");
            cbFilter.Items.Add("License ID");
            cbFilter.Items.Add("Is Active");
            cbFilter.SelectedIndex = 0;

            cbIsActive.Items.Add("Active");
            cbIsActive.Items.Add("UnActive");
            cbIsActive.SelectedIndex = 0;

        }

        void _ApplyFilters()
        {
            string Filter = cbFilter.Text.Replace(" ", "");
            if (cbFilter.SelectedIndex == 5)
            {
                bool IsActiveText = cbIsActive.SelectedIndex == 0 ? true : false;
                _dtInternationalApplications.DefaultView.RowFilter = string.Format("{0} = {1}", Filter, IsActiveText.ToString());
                return;
            }
            if (txtFilter.Text==""||cbFilter.SelectedIndex==0)
            {
                _dtInternationalApplications.DefaultView.RowFilter = "";
                return;
            }
           
            if(cbFilter.SelectedIndex==4)
            {
                _dtInternationalApplications.DefaultView.RowFilter = string.Format("{0} = {1}", "IssuedUsingLocalLicenseID", txtFilter.Text);

            }
            else
            {
                _dtInternationalApplications.DefaultView.RowFilter=string.Format("{0} = {1}",Filter,txtFilter.Text);
            }


        }

        private void frmListInternationalLicesneApplications_Load(object sender, EventArgs e)
        {
            _LoadComboBox();
            _LoadDataGridView();
            _CountRecords();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddInternationalLicense();
            frm.ShowDialog();
            _LoadDataGridView();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmPersondetails(clsInternationaLicense.FindInternationalLicense((int)dgvInternationalLicenseApplications.SelectedRows[0].Cells[0].Value).ApplicantPersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmInternationalLicenseInfo((int)dgvInternationalLicenseApplications.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListLicenseHistory(DVLD_BLL.clsLocalDrivingApplication.FindLocalDrivingApplicationByLicenseID((DVLD_BLL.clsInternationaLicense.FindInternationalLicense((int)dgvInternationalLicenseApplications.SelectedRows[0].Cells[0].Value).IssueUsingLicenseID)).ApplicantPersonID);
            frm.ShowDialog();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Clear();
            if (cbFilter.SelectedIndex == 0)
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = false;
                return;
            }
            if(cbFilter.SelectedIndex == 5)
            {
                txtFilter.Visible = false;
                cbIsActive.Visible = true;
            }
            else
            {
                txtFilter.Visible = true;
                cbIsActive.Visible = false;
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ApplyFilters();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            _ApplyFilters();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled=!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar);
        }
    }
}
