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
    public partial class frmAddInternationalLicense : Form
    {
        public frmAddInternationalLicense()
        {
            InitializeComponent();
        }
        private clsInternationaLicense _InternationaLicense;
        public int SelectedLicenseID = -1;

        private void _Issue()
        {
            clsInternationaLicense InternationalLicense = new clsInternationaLicense();
            InternationalLicense.ApplicantPersonID = ctrlLicenseInfoWithFilter1.LicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate=DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enStatus.New;
            InternationalLicense.LastStatusDate=DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationType.Find((int)clsApplicationType.enApplicationType.NewInternationalLicense).ApplicationTypeFee;
            InternationalLicense.CreatedByUserID = clsGlobalSettings.UserID;


            InternationalLicense.DriverID = ctrlLicenseInfoWithFilter1.LicenseInfo.DriverID;
            InternationalLicense.IssueUsingLicenseID = ctrlLicenseInfoWithFilter1.LicenseInfo.LicenseID;
            InternationalLicense.IssueDate=DateTime.Now;
            //Handle This In DataBase (Create New Table Name It Settings And Insert Into InternationalValidatyLength)
            InternationalLicense.ExpirationDate=DateTime.Now.AddYears(1);
            InternationalLicense.CreatedByUserID = clsGlobalSettings.UserID;
            InternationalLicense.IsActive = true;
            btnIssue.Enabled = false;

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Error : Faild To Issue International License", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("International License Issued Successfully", "Issued",MessageBoxButtons.OK,MessageBoxIcon.Information);
            _InternationaLicense = InternationalLicense;
            lblInternationalApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            lblApplicationDate.Text=InternationalLicense.ApplicationDate.ToShortDateString();
            lblIssueDate.Text = InternationalLicense.IssueDate.ToShortDateString();
            lblExpirationDate.Text=InternationalLicense.ExpirationDate.ToShortDateString();
            lblFees.Text = InternationalLicense.PaidFees.ToString();
            lblCreatedBy.Text = clsGlobalSettings.CurrentUserInfo.UserName;
            linkShowLicenses.Enabled = true;
            ctrlLicenseInfoWithFilter1.FilterEnabled(false);
            }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _Issue();
        }

        private void linkShowLicenses_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm =new frmInternationalLicenseInfo(_InternationaLicense.InternationalLicenseID);
            frm.ShowDialog();
        }

        private void linkShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmListLicenseHistory(ctrlLicenseInfoWithFilter1.LicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            linkShowLicenses.Enabled = false;
            SelectedLicenseID = obj;


            if (SelectedLicenseID == -1)
            {
                btnIssue.Enabled=false;
                return;
            }
            lblLocalLicenseID.Text = SelectedLicenseID.ToString();
            linkShowLicenseHistory.Enabled = true;

            if (ctrlLicenseInfoWithFilter1.LicenseInfo.LicenseClass!=3)
            {
                MessageBox.Show("Selected License Should Be Class 3... Select Another One","Not Allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            int ActiveInternationalLicenseID = clsInternationaLicense.GetActiveInternationalLicenseID(ctrlLicenseInfoWithFilter1.LicenseInfo.DriverID);
            if (ActiveInternationalLicenseID>-1)
            {
                MessageBox.Show("Driver Already Have Active International License With InternationalID : "+ActiveInternationalLicenseID.ToString(), "Already Exists",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                linkShowLicenses.Enabled = true;
                _InternationaLicense=clsInternationaLicense.FindInternationalLicense(ActiveInternationalLicenseID);
                btnIssue.Enabled = false;
                return;
            }

            if(!ctrlLicenseInfoWithFilter1.LicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License Is Not Active ...Active it First", "Not Active",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            btnIssue.Enabled = true;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
    }
}
