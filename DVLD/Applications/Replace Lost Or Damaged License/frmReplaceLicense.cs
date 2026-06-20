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
    public partial class frmReplaceLicense : Form
    {
        private clsLicense _OldLicense;
        private int _NewLicenseID = -1;

        public frmReplaceLicense()
        {
            InitializeComponent();
        }

        private void _LoadDefaults()
        {
            lblReplacmentLicenseID.Text = "[???]";
            lblReplacedLicesnseID.Text = "[???]";
            lblAppDate.Text = DateTime.Now.ToShortDateString();
            lblCreatedBy.Text=clsGlobalSettings.CurrentUserInfo.UserName;
        }
        private void _SetLicenseInfo()
        {
            lblOldLicenseID.Text =_OldLicense.LicenseID.ToString();
        }
        private void _Issue()
        {
            clsLicense NewLicense;

            if (rbDamagedLicense.Checked)
            {
                NewLicense = _OldLicense.ReplaceForDamaged("",clsGlobalSettings.UserID);
            }
            else
                NewLicense = _OldLicense.ReplaceForLost("",clsGlobalSettings.UserID);

            btnIssue.Enabled = false;
            gbReplacmentSelect.Enabled = false;
            ctrlLicenseInfoWithFilter1.FilterEnabled(false);

            if(NewLicense==null)
            {
                MessageBox.Show("Error : Faild To Issue Replacment License...", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            _NewLicenseID= NewLicense.LicenseID;
            MessageBox.Show("License Replacement Issued Succussfully with ID : "+_NewLicenseID.ToString(), "Issued",MessageBoxButtons.OK,MessageBoxIcon.Information);
            linklblNewLicenseInfo.Enabled = true;
            lblReplacedLicesnseID.Text = _NewLicenseID.ToString();
            lblReplacmentLicenseID.Text=NewLicense.ApplicationID.ToString();

        }


        private void ctrlLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            btnIssue.Enabled = false;
            linklblLicenseHistory.Enabled = false;
            linklblNewLicenseInfo.Enabled=false;    


            if (obj == -1)
                return;

            _OldLicense = clsLicense.FindLicense(obj);
            
            if (_OldLicense == null)
                return;
            
            _SetLicenseInfo();
            linklblLicenseHistory.Enabled=true;

            if(_OldLicense.IsActive==false)
            {
                MessageBox.Show("This License is Not Active...Active It First", "Not Active",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }


            btnIssue.Enabled = true;

        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            if(rbDamagedLicense.Checked)
            {
                lblTitle.Text = "Replacment For Damaged License";
                this.Text=lblTitle.Text;
                lblAppFees.Text = clsApplicationType.Find((int)clsApplicationType.enApplicationType.ReplaceDamagedDrivingLicense).ApplicationTypeFee.ToString();
            }
            else
            {
                lblTitle.Text = "Replacment For Lost License";
                this.Text = lblTitle.Text;
                lblAppFees.Text = clsApplicationType.Find((int)clsApplicationType.enApplicationType.ReplaceLostDrivingLicecnse).ApplicationTypeFee.ToString();
            }
        }

        private void linklblLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm =new frmListLicenseHistory(_OldLicense.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void linklblNewLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm=new frmLicenseInfo(_NewLicenseID);
            frm.ShowDialog();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _Issue();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReplaceLicense_Load(object sender, EventArgs e)
        {
            ctrlLicenseInfoWithFilter1.FilterTextIDFoucs();
            btnIssue.Enabled = false;
            linklblLicenseHistory.Enabled = false;
            linklblNewLicenseInfo.Enabled = false;
            _LoadDefaults();



            rbDamagedLicense.Checked = true;
            lblTitle.Text = "Replacment For Damaged License";
            this.Text = lblTitle.Text;
            lblAppFees.Text = clsApplicationType.Find((int)clsApplicationType.enApplicationType.ReplaceDamagedDrivingLicense).ApplicationTypeFee.ToString();

        }
    }
}
