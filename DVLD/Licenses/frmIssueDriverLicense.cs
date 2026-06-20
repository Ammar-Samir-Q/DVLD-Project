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
    public partial class frmIssueDriverLicense : Form
    {
       private int _LocalDrivingApplicationID = -1;
        private DVLD_BLL.clsLocalDrivingApplication _LocalDrivingApplication;
        public frmIssueDriverLicense(int LocalDrivingApplicationID)
        {
            InitializeComponent();
             _LocalDrivingApplicationID= LocalDrivingApplicationID;
        }
    
        private void _Load()
        {
            _LocalDrivingApplication = clsLocalDrivingApplication.FindLocalDrivingApplication(_LocalDrivingApplicationID);
            if(!_LocalDrivingApplication.PassedAllTests())
            {
                MessageBox.Show("Person Should Finish All Tests First", "Not Allowed",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if(_LocalDrivingApplication.GetLastestActiveLicense()>0)
            {
                MessageBox.Show("Person Already Have License...", "Already Have A Lincese", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                this.Close();
                return;
            }

            if(_LocalDrivingApplicationID>0)
            {
                _LocalDrivingApplication = DVLD_BLL.clsLocalDrivingApplication.FindLocalDrivingApplication(_LocalDrivingApplicationID);
                ctrlLocalDrivingApplicationInfo1.LoadLocalDrivingApplicationByID(_LocalDrivingApplicationID);
            }
            else
            {
                MessageBox.Show("Error : Faild To Load This Local Driving Application","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;

            }

        }

        private void _Issue()
        {
            int LicenseID = _LocalDrivingApplication.IssueLicnseForFirstTime(txtNotes.Text, clsGlobalSettings.UserID); 
            if(LicenseID>0)
            {
                MessageBox.Show("License Issued Succssfully With ID = "+ LicenseID.ToString(), "Issued Succussfully",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Close();
                return;
            }
            else
            {
                MessageBox.Show("Error : Faild To Issue This License", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }

        }

        private void frmIssueDriverLicense_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _Issue();
        }
    }
}
