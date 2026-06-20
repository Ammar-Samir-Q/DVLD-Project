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
    public partial class frmDetainLicense : Form
    {
        clsLicense _License;
        private int _DetainID = -1;
        public frmDetainLicense()
        {
            InitializeComponent();
        }
        private void _Load()
        {
            lblCreatedBy.Text = clsGlobalSettings.CurrentUserInfo.UserName;

        }
        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            btnDetain.Enabled= false;
            linklblLicenseHistory.Enabled= false;
            linklblNewLicenseInfo.Enabled= false;


            if (obj == -1)
                return;

            _License=clsLicense.FindLicense(obj);
            if (_License == null)
                return;

            linklblLicenseHistory.Enabled = true;
            lblLinceseID.Text = _License.LicenseID.ToString();

            if(_License.IsDetained())
            {
                MessageBox.Show("This License Is Already Detained Select Another License", "Already Deatined",MessageBoxButtons.OK,MessageBoxIcon.Error);
                linklblNewLicenseInfo.Enabled = true;
                return;
            }


            if(!_License.IsActive)
            {
                MessageBox.Show("This License Is Not Active...Active It First","License Is Not Active",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            txtFineFees.Focus();
            btnDetain.Enabled = true;


        }
   
        private void _Detain()
        {
            ctrlLicenseInfoWithFilter1.FilterEnabled(false);
            btnDetain.Enabled=false;
            clsDetainedLicense DetainedLicense = _License.Detain(Convert.ToSingle(txtFineFees.Text),clsGlobalSettings.UserID);
            
            if(DetainedLicense == null)
            {
                MessageBox.Show("Error : Faild To Detain This License", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);    
                return;
            }
             _DetainID = DetainedLicense.DetainID;
            lblDetainId.Text = DetainedLicense.DetainID.ToString();
            lblDetainDate.Text = DetainedLicense.DetainDate.ToShortDateString();
            linklblNewLicenseInfo.Enabled=true;
            txtFineFees.Enabled = false;

            MessageBox.Show("License Detained Successfully With ID : " + _DetainID, "Detained", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
            ctrlLicenseInfoWithFilter1.SetIsDetained(true);

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

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if(this.ValidateChildren())
            _Detain();
        }

        private void frmDetainLicense_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void txtFineFees_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled=!char.IsDigit(e.KeyChar)&&!char.IsControl(e.KeyChar);
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if(txtFineFees.Text=="")
            {
                e.Cancel=true;
                ep1.SetError(txtFineFees, "Enter Fine Fees");
            }
        }
    }
}
