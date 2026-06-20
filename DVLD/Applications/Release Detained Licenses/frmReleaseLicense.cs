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
    public partial class frmReleaseLicense : Form
    {
        private clsLicense _License;

        public frmReleaseLicense()
        {
            InitializeComponent();
        }

        public frmReleaseLicense(int LicenseID)
        {
            InitializeComponent();
            
                _LoadDefaults();
                ctrlLicenseInfoWithFilter1.LoadLicense(LicenseID);
                ctrlLicenseInfoWithFilter1.FilterEnabled(false);
              
            
        }

        private void _LoadDefaults()
        {
            lblCreatedBy.Text =clsGlobalSettings.CurrentUserInfo.UserName;
            lblAppFees.Text = clsApplicationType.Find((int)clsApplicationType.enApplicationType.ReleaseDetainedDrivingLicense).ApplicationTypeFee.ToString();
            

        }
       
        private void _ReleaseLicnese()
        {
            btnRelease.Enabled = false;
            ctrlLicenseInfoWithFilter1.FilterEnabled(false);
            int ApplicationID = -1;


            if(!_License.Release(clsGlobalSettings.UserID,ref ApplicationID))
            {
                MessageBox.Show("Error : Faild To Release This Detian License", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            _License.ActiveCurrentLicense();
            linklblNewLicenseInfo.Enabled = true;
            lblReleaseAppID.Text = ApplicationID.ToString();
            ctrlLicenseInfoWithFilter1.SetIsDetained(false);
            MessageBox.Show("Detained License Released Succussfully With Application ID : " + ApplicationID.ToString(), "License Released",MessageBoxButtons.OK,MessageBoxIcon.Information);


        }
        private void _SetLicenseInfo()
        {
            lblLinceseID.Text =_License.LicenseID.ToString();
            lblDetainId.Text=_License.DetainedInfo.DetainID.ToString();
            lblDetainDate.Text = _License.DetainedInfo.DetainDate.ToString();
            lblFineFees.Text=_License.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblFineFees.Text) + Convert.ToSingle(lblAppFees.Text)).ToString();

        }
        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            btnRelease.Enabled=false;
            linklblLicenseHistory.Enabled=false;
            linklblNewLicenseInfo.Enabled=false;


            if (obj == -1)
                return;

            _License=clsLicense.FindLicense(obj);
            if (_License.DetainedInfo == null)
            {
                MessageBox.Show("This License Is Not Detaied Select Detained License", "Not Detained",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }

            _SetLicenseInfo();
            linklblLicenseHistory.Enabled = (_License.LicenseID!=-1);

            if(_License.DetainedInfo.IsReleased)
            {
                MessageBox.Show("This License Is Released In Date "+_License.DetainedInfo.ReleaseDate.ToShortDateString()+" Select Detained License", "Already Released",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            btnRelease.Enabled=true;

        }

        private void frmReleaseLicense_Load(object sender, EventArgs e)
        {
            ctrlLicenseInfoWithFilter1.FilterTextIDFoucs();
            _LoadDefaults();
        }

        private void linklblLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmListLicenseHistory(_License.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void linklblNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmLicenseInfo(_License.LicenseID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            _ReleaseLicnese();
        }
    }
}
