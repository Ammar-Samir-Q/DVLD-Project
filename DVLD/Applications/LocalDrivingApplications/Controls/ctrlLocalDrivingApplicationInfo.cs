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
    public partial class ctrlLocalDrivingApplicationInfo : UserControl
    {
        private int _LocalDrivingAppID = -1;
        private clsLocalDrivingApplication _LocalDrivingApplication;
        private clsTestAppointemnt _TestAppointemnt;
        private clsLicense _License;
        public ctrlLocalDrivingApplicationInfo()
        {
            InitializeComponent();
        }

        public int LocalDrivingApplicationID { get { return _LocalDrivingAppID;} }
        public clsLocalDrivingApplication LocalDrivingApplicationInfo { get { return _LocalDrivingApplication;} }

        public void LoadLocalDrivingApplicationByID(int LocalDrivingApplicationID)
        {
            _LocalDrivingAppID=LocalDrivingApplicationID;
            _LoadData();
        }

        public void LoadLocalDrivingLicenseApplicationByApplicationID(int ApplicationID)
        {
            _LocalDrivingApplication = clsLocalDrivingApplication.FindLocalDrivingApplicationByApplicationID(ApplicationID);
            _LocalDrivingAppID = _LocalDrivingApplication.LocalDrivingLicenseApplicationID;
            _LoadData();
        }


        private void _LoadData()
        {
            if (_LocalDrivingAppID > 0)
            {
                _LocalDrivingApplication = DVLD_BLL.clsLocalDrivingApplication.FindLocalDrivingApplication(_LocalDrivingAppID);
                _TestAppointemnt = DVLD_BLL.clsTestAppointemnt.FindByLocalDrivingLicenseApplicationID(_LocalDrivingAppID);
                if( _LocalDrivingApplication != null)
                {
                    ctrlApplicationInfo1.LoadApplication(_LocalDrivingApplication.ApplicationID);
                    lblLocalDrivingApplicationID.Text = _LocalDrivingApplication.LocalDrivingLicenseApplicationID.ToString();
                    lblClassName.Text = _LocalDrivingApplication.LicenseClassInfo.ClassName;
                    _License = DVLD_BLL.clsLicense.FindLicenseByLocalLicenseID(_LocalDrivingAppID);
                    if(_License!=null)
                    {
                        linklblLicenseInfo.Enabled=true;
                    }
                    lblPassedTests.Text = clsTest.GetPassedTestsCount(_LocalDrivingAppID).ToString()+ "/3";
                       
                }
                else
                {
                    MessageBox.Show("Cant Load The Application ...", "Error");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Error While Loading The Application ...", "Error");
            }


        }

        private void linklblLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmLicenseInfo(_License.LicenseID);
            frm.ShowDialog();
        }

    }
}
