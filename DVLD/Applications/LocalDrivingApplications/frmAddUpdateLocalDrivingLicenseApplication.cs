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
    public partial class frmAddLocalLicense : Form
    {
        enum enMode { eAdd=1,eUpdate=2}
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingApplication _LocalDrivingApplication;
        private enMode _Mode;
        private clsApplicationType _ApplicationType = DVLD_BLL.clsApplicationType.Find((int)clsLocalDrivingApplication.enApplicationType.NewLocalDrivingLicense);
        public frmAddLocalLicense()
        {
            InitializeComponent();
            _Mode = enMode.eAdd;
        }

        public frmAddLocalLicense(int LocalDrivingApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID=LocalDrivingApplicationID;
            _Mode = enMode.eUpdate;
        }

        private void _LoadLicenseClassesComboBox()
        {
           cbLicenseClasses.DataSource = DVLD_BLL.clsLicenseClasse.GetAllLicenseClasses();
            if(cbLicenseClasses.Items.Count <= 0)
            {
                return;
            }
            cbLicenseClasses.DisplayMember = "ClassName";
            cbLicenseClasses.ValueMember = "LicenseClassID";
        }

        private void _SetDefault()
        {
            
            _LocalDrivingApplication.ApplicationDate=DateTime.Now;
            _LocalDrivingApplication.CreatedByUserID = DVLD_BLL.clsGlobalSettings.UserID;
            _LocalDrivingApplication.UserInfo = DVLD_BLL.clsGlobalSettings.CurrentUserInfo;
            btnSave.Enabled = false;
            tc1.SelectedIndex = 2;
            lblApplicationDate.Text = _LocalDrivingApplication.ApplicationDate.ToShortDateString();
            cbLicenseClasses.SelectedIndex = 0;
            lblApplicationFees.Text = _ApplicationType.ApplicationTypeFee.ToString();
            lblCreatedBy.Text =clsGlobalSettings.CurrentUserInfo.UserName;
        }

        private void _Load()
        {
            _LoadLicenseClassesComboBox();
            if (_Mode==enMode.eAdd)
            {
                lblTitle.Text = "New Local Driving License Application";
                this.Text=lblTitle.Text;
                btnSave.Enabled = false;
                _LocalDrivingApplication = new DVLD_BLL.clsLocalDrivingApplication();
                _SetDefault();
                return;
            }
            _LocalDrivingApplication = DVLD_BLL.clsLocalDrivingApplication.FindLocalDrivingApplication(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingApplication == null)
            {
                MessageBox.Show("Error : Faild To Load Local Driving Application","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }
            gbLicenseInfo.Enabled = true;
            ctrlPersonCardWithFilter1.EnableFilter = false;
            btnNext.Visible = false;
            btnSave.Enabled=true;
            lblTitle.Text = "Update Local Driving License Application";
            this.Text = lblTitle.Text;
            ctrlPersonCardWithFilter1.LoadPersonInfo(_LocalDrivingApplication.ApplicantPersonID);
            lblDLApplicationID.Text = _LocalDrivingApplication.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = _LocalDrivingApplication.ApplicationDate.ToShortDateString();
            cbLicenseClasses.SelectedValue = _LocalDrivingApplication.LicenseClassID;
            lblApplicationFees.Text = _LocalDrivingApplication.PaidFees.ToString();
            lblCreatedBy.Text=this._LocalDrivingApplication.UserInfo.UserName;
        }
        private void _Next()
        {
            
            
                if (ctrlPersonCardWithFilter1.PersonInfo != null)
                {
                    ctrlPersonCardWithFilter1.EnableFilter = false;
                    tc1.SelectedTab = tpLicense;
                    btnNext.Visible = false;
                    btnSave.Enabled = true;
                    gbLicenseInfo.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Please Select Perosn First", "Select Perosn",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
            
        }

        private void _Save()
        {
            _LocalDrivingApplication.ApplicantPersonID = ctrlPersonCardWithFilter1.PersonID;
            if(_Mode==enMode.eAdd)
            {
                _LocalDrivingApplication.PaidFees = _ApplicationType.ApplicationTypeFee;
                _LocalDrivingApplication.ApplicationTypeID = DVLD_BLL.clsApplication.enApplicationType.NewLocalDrivingLicense;
                _LocalDrivingApplication.ApplicationStatus = (DVLD_BLL.clsApplication.enStatus.New);
                _LocalDrivingApplication.LastStatusDate = DateTime.Now;
            }
            _LocalDrivingApplication.LicenseClassID = (int)cbLicenseClasses.SelectedValue;


            //Check If Person Already Have License
            if (clsLocalDrivingApplication.IsPersonHaveLocalLicense(_LocalDrivingApplication.ApplicantPersonID, _LocalDrivingApplication.LicenseClassID))
            {
                MessageBox.Show("Person With PerosnID : " + _LocalDrivingApplication.ApplicantPersonID + " Is Already have Active License With The Same License Class", "Already Have License", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                return;
            }

            //Check If Person Have Active Application
            if (clsLocalDrivingApplication.IsPersonHaveActiveLocalDrivingApplicationClass(_LocalDrivingApplication.ApplicantPersonID, _LocalDrivingApplication.LicenseClassID))
            {
                MessageBox.Show("Person With PerosnID = " + _LocalDrivingApplication.ApplicantPersonID + " Is Already have Active Application With The Same License Class", "Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                return;
            }
           
            if(_LocalDrivingApplication.Save())
            {
                lblDLApplicationID.Text =_LocalDrivingApplication.LocalDrivingLicenseApplicationID.ToString();
                _Mode = enMode.eUpdate;
                lblTitle.Text = "Update Local License Application";
                this.Text=lblTitle.Text;
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                _LocalDrivingLicenseApplicationID = _LocalDrivingApplication.LocalDrivingLicenseApplicationID;
            }
            else
            {
                MessageBox.Show("Error : Faild to Save Data", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            _Next();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Save();
        }


        private void frmAddLocalLicense_Load(object sender, EventArgs e)
        {
            _Load();
        }

        private void frmAddLocalLicense_Activated(object sender, EventArgs e)
        {
            ctrlPersonCardWithFilter1.FilterFocus();
        }
    }
}
