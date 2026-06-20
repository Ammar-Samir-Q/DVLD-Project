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
    public partial class frmMainMenu : Form
    {

        private frmLogin _frmLogin;
        public frmMainMenu(frmLogin form)
        {
            InitializeComponent();
            _frmLogin = form;
        }

        
        private void peopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmManagePeople();
            frm.ShowDialog();
        }

        private void usersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm =new frmListUsers();
            frm.ShowDialog();
        }

        private void frmMainMenu_Load(object sender, EventArgs e)
        {
            if (clsGlobalSettings.CurrentUserInfo != null)
                lblUserName.Text = clsGlobalSettings.CurrentUserInfo.UserName!="Admin"?
                    clsGlobalSettings.CurrentUserInfo.UserName: clsGlobalSettings.CurrentUserInfo.UserName.ToString() + " (" + clsGlobalSettings.CurrentUserInfo.PersonInfo.FirstName + ")";
            else
                lblUserName.Text = "???";
        }

        private void currentUserInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm=new frmUserInfo(DVLD_BLL.clsGlobalSettings.CurrentUserInfo.UserID);
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new DVLD.Users.frmChangePassword(DVLD_BLL.clsGlobalSettings.CurrentUserInfo.UserID);
            frm.ShowDialog();
        }

        private void signOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DVLD_BLL.clsGlobalSettings.CurrentUserInfo = null;
            this.Hide();
            _frmLogin.Show();

        }

        private void manageApplicationTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new DVLD.ApplicationTypes.frmListApplicationTypes();
            frm.ShowDialog();
        }

        private void manageTestTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm =new DVLD.TestTypes.frmListTestTypes();
            frm.ShowDialog();
        }

        private void frmMainMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void localLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddLocalLicense();
            frm.ShowDialog();
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListLocalDrivingApplications();
            frm.ShowDialog();
        }

        private void driversToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm =new frmListDrivers();
            frm.ShowDialog();
        }

        private void internationalLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm =new frmAddInternationalLicense();
            frm.ShowDialog();
        }

        private void internationalLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmListInternationalLicesneApplications();
            frm.ShowDialog();
        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm=new frmRenewLicense();
            frm.ShowDialog();
        }

      
        private void replacmentForLostOrDamagedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReplaceLicense();
            frm.ShowDialog();
        }

        private void detainLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm=new frmDetainLicense();
            frm.ShowDialog();
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm=new frmReleaseLicense();
            frm.ShowDialog();
        }

        private void releseDetaindDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new frmReleaseLicense();
            frm.ShowDialog();
        }

        private void manageDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm=new frmListDetainedLicenses();
            frm.ShowDialog();
        }

        private void retakeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm=new frmListLocalDrivingApplications();
            frm.ShowDialog();
        }

        private void applicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
