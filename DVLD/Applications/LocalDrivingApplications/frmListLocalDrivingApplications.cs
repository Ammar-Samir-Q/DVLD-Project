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
    public partial class frmListLocalDrivingApplications : Form
    {
        DataTable dtApplications;
        public frmListLocalDrivingApplications()
        {
            InitializeComponent();
        }

        private void _LoadDataGridView()
        {
             dtApplications = DVLD_BLL.clsLocalDrivingApplication.GetAllLocalApplicationsView();
             dgvApplications.DataSource= dtApplications;
            if(dgvApplications.Rows.Count > 0)
            {
                dgvApplications.Columns[0].HeaderText = "L.D.L AppID";
                dgvApplications.Columns[0].Width =100;

                dgvApplications.Columns[1].HeaderText = "Class Name";
                dgvApplications.Columns[1].Width =350;

                dgvApplications.Columns[2].HeaderText = "National No";
                dgvApplications.Columns[2].Width =100;

                dgvApplications.Columns[3].HeaderText = "Full Name";
                dgvApplications.Columns[3].Width =300;

                dgvApplications.Columns[4].HeaderText = "Application Date";
                dgvApplications.Columns[4].Width =200;

                dgvApplications.Columns[5].HeaderText = "Passed Test Count";
                dgvApplications.Columns[5].Width =100;

                dgvApplications.Columns[6].HeaderText = "Status";
                dgvApplications.Columns[6].Width =120;

            }


        }

        private void _RefreashDGV()
        {
            dgvApplications.DataSource = DVLD_BLL.clsLocalDrivingApplication.GetAllLocalApplicationsView();
            _CountRecords();
        }

        private void _CountRecords()
        {
            lblRecords.Text=dgvApplications.Rows.Count.ToString();
        }

        private void _LoadFilterCombobox()
        {
            cbFilter.Items.Clear();
            cbFilter.Items.Add("None");
            cbFilter.Items.Add("L.D.L AppID");
            cbFilter.Items.Add("National No");
            cbFilter.Items.Add("Full Name");
            cbFilter.Items.Add("Status");
            
            cbFilter.SelectedIndex = 0;
        }

        private void _ApplyFilters()
        {
            string Filter=cbFilter.Text.Replace(" ","");
            if(txtFilter.Text==""||Filter=="None")
            {
                dtApplications.DefaultView.RowFilter = "";
                _CountRecords();
                return;
            }

            if(cbFilter.SelectedIndex==1)
            {
                dtApplications.DefaultView.RowFilter = string.Format("{0}={1}", "LocalDrivingLicenseApplicationID", txtFilter.Text.Trim()); ;
            }
            else
            {
                dtApplications.DefaultView.RowFilter = string.Format("{0} Like '{1}%'", Filter, txtFilter.Text.Trim());

            }

        }

        private void frmManageApplications_Load(object sender, EventArgs e)
        {
            _LoadDataGridView();
            _LoadFilterCombobox();
            _CountRecords();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form frm=new frmAddLocalLicense();
            frm.ShowDialog();
            _RefreashDGV();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Clear();
            if(cbFilter.SelectedIndex == 0)
            {
                txtFilter.Visible=false;    
            }
            else
                txtFilter.Visible=true;
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilter.SelectedIndex==1)
            {
                e.Handled=!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar);
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            _ApplyFilters();
        }

        private void editApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddLocalLicense((int)dgvApplications.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
            _RefreashDGV();
        }

       
        private void cancelApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(DVLD_BLL.clsApplication.UpdateApplicationStatus(DVLD_BLL.clsLocalDrivingApplication.FindLocalDrivingApplication((int)dgvApplications.SelectedRows[0].Cells[0].Value).ApplicationID,2))
            {
                MessageBox.Show("Application Cancelled Successfully", "Cancelled",MessageBoxButtons.OK,MessageBoxIcon.Information);
                _RefreashDGV();
            }
            else
            {
                MessageBox.Show("Error : Faild To Cancel THis Application","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Delete Application This Application ?", "Delete", MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (DVLD_BLL.clsLocalDrivingApplication.DeleteLocalDrivingApplication((int)dgvApplications.SelectedRows[0].Cells[0].Value))
                {
                    MessageBox.Show("Deleted Successfully", "Deleted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    _RefreashDGV();
                    return;
                }
                else
                {
                    MessageBox.Show("Error : Faild To Delete The Application", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            
        }

        private void applicationInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmLocalDrivingApplicationInfo((int)dgvApplications.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
        }

       
        private void visionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmTestAppointemnt((int)dgvApplications.SelectedRows[0].Cells[0].Value, 1);
            frm.ShowDialog();
            _RefreashDGV();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmTestAppointemnt((int)dgvApplications.SelectedRows[0].Cells[0].Value, 2);
            frm.ShowDialog();
            _RefreashDGV();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmTestAppointemnt((int)dgvApplications.SelectedRows[0].Cells[0].Value, 3);
            frm.ShowDialog();
            _RefreashDGV();
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            if(dgvApplications.SelectedRows.Count < 0)
                return;
            //This Will Be Affected If DGV Changed
            int PassedTestsCount = (int)dgvApplications.SelectedRows[0].Cells[5].Value;
            string StatusText = (string)dgvApplications.SelectedRows[0].Cells[6].Value;

            foreach(ToolStripItem Tool in cmsApplications.Items)
            {
                Tool.Enabled = false;
            }
            showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
            switch (PassedTestsCount)
            {
                case 0:
                    schedualTestToolStripMenuItem.Enabled = true;
                    visionTestToolStripMenuItem.Enabled = true;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled = false;
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                    break;
                case 1:
                    schedualTestToolStripMenuItem.Enabled = true;
                    visionTestToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = true;
                    scheduleStreetTestToolStripMenuItem.Enabled = false;
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                    break;
                case 2:
                    schedualTestToolStripMenuItem.Enabled = true;
                    visionTestToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled = true;
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                    break;
                case 3:
                    schedualTestToolStripMenuItem.Enabled = false;
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
                    break;

            }

            switch (StatusText)
            {
                case "New":
                    editApplicationToolStripMenuItem.Enabled = true;
                    applicationInfoToolStripMenuItem.Enabled = true;
                    cancelApplicationToolStripMenuItem.Enabled = true;
                    deleteApplicationToolStripMenuItem.Enabled = true;
                    showLicenseToolStripMenuItem.Enabled = false;
                    break;
                case "Cancelled":
                    editApplicationToolStripMenuItem.Enabled = false;
                    applicationInfoToolStripMenuItem.Enabled = true;
                    cancelApplicationToolStripMenuItem.Enabled = false;
                    deleteApplicationToolStripMenuItem.Enabled = true;
                    schedualTestToolStripMenuItem.Enabled = false;
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                    showLicenseToolStripMenuItem.Enabled = false;
                    break;

                case "Completed":
                    editApplicationToolStripMenuItem.Enabled = false;
                    applicationInfoToolStripMenuItem.Enabled = true;
                    cancelApplicationToolStripMenuItem.Enabled = false;
                    deleteApplicationToolStripMenuItem.Enabled = false;
                    schedualTestToolStripMenuItem.Enabled = false;
                    issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                    showLicenseToolStripMenuItem.Enabled = true;
                    break;
            }
        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmIssueDriverLicense((int)dgvApplications.SelectedRows[0].Cells[0].Value);
            frm.ShowDialog();
            _RefreashDGV();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmLicenseInfo(DVLD_BLL.clsLicense.FindLicenseByLocalLicenseID((int)dgvApplications.SelectedRows[0].Cells[0].Value).LicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm =new frmListLicenseHistory(clsLocalDrivingApplication.FindLocalDrivingApplication((int)dgvApplications.SelectedRows[0].Cells[0].Value).ApplicantPersonID);
            frm.ShowDialog();
        }
    }
}
