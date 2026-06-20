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
    public partial class frmRenewLicense : Form
    {
        private clsLicense _OldLicnese;
        private int _NewLicenseID = -1;

        public frmRenewLicense()
        {
            InitializeComponent();
        }

     

        private void _SetApplicationInfo()
        {
            lblAppDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblOldLicenseID.Text = ctrlLicenseInfoWithFilter1.LicenseID.ToString();
            lblExpDate.Text = DateTime.Now.AddYears(clsLicenseClasse.Find(ctrlLicenseInfoWithFilter1.LicenseInfo.LicenseClass).DefaultValidityLength).ToShortDateString();
            lblAppFees.Text = clsApplicationType.Find((int)clsApplicationType.enApplicationType.RenewDrivingLicense).ApplicationTypeFee.ToString(); ;
            lblLicenseFees.Text = ctrlLicenseInfoWithFilter1.LicenseInfo.LicesneClassInfo.ClassFees.ToString();
            lblCreatedBy.Text = clsGlobalSettings.CurrentUserInfo.UserName;
            lblTotalFees.Text = (Convert.ToSingle(lblAppFees.Text) + Convert.ToSingle(lblLicenseFees.Text)).ToString();
            txtNotes.Text = ctrlLicenseInfoWithFilter1.LicenseInfo.Notes;
        }

        private void _LoadDefaults()
        {
            
            lblAppDate.Text = DateTime.Now.ToShortDateString();
            lblAppFees.Text = clsApplicationType.Find((int)clsApplicationType.enApplicationType.RenewDrivingLicense).ApplicationTypeFee.ToString(); ;
            lblCreatedBy.Text = clsGlobalSettings.CurrentUserInfo.UserName;
            lblTotalFees.Text = "[$$$]";
            txtNotes.Text = "";
        }


        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            btnIssue.Enabled = false;
            linklblLicenseHistory.Enabled = false;
            linklblLicenseHistory.Enabled = false;

            if (obj == -1)
                return;

            _OldLicnese = ctrlLicenseInfoWithFilter1.LicenseInfo;

            if (_OldLicnese == null)
                return;

            linklblLicenseHistory.Enabled = (obj!=-1);
            _SetApplicationInfo();


            if(_OldLicnese.IsActive==false)
            {
                MessageBox.Show("This License In Not Active...Active It First","Not Active",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;

            }

            if(DateTime.Now<_OldLicnese.ExpirationDate)
            {
                MessageBox.Show("This License Is Not Expired Yet The ExpirationDate : " + _OldLicnese.ExpirationDate.ToShortDateString(), "Not Expired",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            btnIssue.Enabled = true;

        }

        private void _Issue()
        {
            clsLicense NewLicense = _OldLicnese.Renew(txtNotes.Text.Trim(),clsGlobalSettings.UserID);
            if(NewLicense==null)
            {
                MessageBox.Show("Error : Faild To Renew This License", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                btnIssue.Enabled=false;
                return;
            }    

            lblRenewAppID.Text=NewLicense.ApplicationID.ToString();
            lblRenewdLicesnseID.Text = NewLicense.LicenseID.ToString();
            linklblNewLicenseInfo.Enabled = true;

            MessageBox.Show("License Renewed Succussfully With ID : "+NewLicense.LicenseID.ToString(), "License Renewed",MessageBoxButtons.OK,MessageBoxIcon.Information);


            _NewLicenseID = NewLicense.LicenseID;
            ctrlLicenseInfoWithFilter1.FilterEnabled(false);
            btnIssue.Enabled = false;

        }

        private void frmRenewLicense_Load(object sender, EventArgs e)
        {
            ctrlLicenseInfoWithFilter1.FilterTextIDFoucs();
            _LoadDefaults();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _Issue();
        }

        private void linklblLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm=new frmListLicenseHistory(_OldLicnese.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void linklblNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }
    }
}
