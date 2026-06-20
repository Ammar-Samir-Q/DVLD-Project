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
    public partial class ctrlScheduleTest : UserControl
    {
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 }
        private enTestType _TestType = enTestType.VisionTest;

        private enum enMode { AddNew = 1, Update = 2 }
        private enMode _Mode = enMode.AddNew;

        public enum enCreationMode { FirstTimeTest = 0, RetakeTest = 1 }
        private enCreationMode _CreationMode = enCreationMode.FirstTimeTest;

        public ctrlScheduleTest()
        {
            InitializeComponent();
        }
        private int _LocalDrivingApplicationdID = -1;
        private clsLocalDrivingApplication _LocalDrivingLicenseApplication;
        private int _TestAppointemntID = -1;
        private clsTestAppointemnt _TestAppointment;

        public int Mode { get { return (int)_Mode; } set { _Mode = (enMode)value; } }
        public int TestAppointemntID { get { return _TestAppointemntID; } }
        public DVLD_BLL.clsTestAppointemnt TestAppointmentInfo { get { return _TestAppointment; } }

        public enTestType TestType
        {
            get { return _TestType; }
            set
            {
                _TestType = value;
                switch (_TestType)
                {
                    case enTestType.VisionTest:
                        //Change form Mode To The TestType Vision-Test
                        pbTest.Image = Properties.Resources.png_VisionTest;
                        lblTitle.Text = "Vision-Test";
                        gbTestInfo.Text = lblTitle.Text;
                        break;
                    case enTestType.WrittenTest:
                        //Change form Mode To The TestType Written-Test
                        pbTest.Image = Properties.Resources.png_WrittenTest;
                        lblTitle.Text = "Written-Test";
                        gbTestInfo.Text = lblTitle.Text;
                        break;
                    case enTestType.StreetTest:
                        //Change form Mode To The TestType Street-Test
                        pbTest.Image = Properties.Resources.png_StreetTest;
                        lblTitle.Text = "Street-Test";
                        gbTestInfo.Text = lblTitle.Text;
                        break;

                }

            }

        }



        public DateTime GetDate()
        {
            return dtpDate.Value;
        }


        public void LoadInfo(int LocalDrivingApplicationID, int AppointmentID = -1)
        {
            if (AppointmentID > 0)
                _Mode = enMode.Update;
            else
                _Mode = enMode.AddNew;

            _LocalDrivingApplicationdID = LocalDrivingApplicationID;
            _TestAppointemntID = AppointmentID;
            _LocalDrivingLicenseApplication = clsLocalDrivingApplication.FindLocalDrivingApplication(_LocalDrivingApplicationdID);

            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error : Faild To Load The Local Driving Application", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }

            //Decide The Creation Mode
            if (_LocalDrivingLicenseApplication.DoseAttendTestType((int)_TestType))
            {
                _CreationMode = enCreationMode.RetakeTest;
            }
            else
                _CreationMode = enCreationMode.FirstTimeTest;


            if (_CreationMode == enCreationMode.RetakeTest)
            {
                gbRetakeTestInfo.Enabled = true;
                lblRAppFees.Text = clsApplicationType.Find((int)clsApplicationType.enApplicationType.RetakeTest).ApplicationTypeFee.ToString();
                lblRTestAppID.Text = "0";
                lblTitle.Text = "Schedule Retake Test";

            }
            else
            {
                gbRetakeTestInfo.Enabled = false;
                lblRAppFees.Text = "0";
                lblRTestAppID.Text = "N/A";
                lblTitle.Text = "Schedule Test";
            }

            //Fill Control Info From The LocalApplication Object
            lblDLApp.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblName.Text = _LocalDrivingLicenseApplication.PersonInfo.FullName();
            lblTrial.Text = _LocalDrivingLicenseApplication.TotalTrailsPerTest((int)_TestType).ToString();

            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestType.Find((int)_TestType).Fees.ToString();
                dtpDate.MinDate = DateTime.Now;
                lblRTestAppID.Text = "N/A";
                _TestAppointment = new clsTestAppointemnt();
            }
            else
            {
                if (!_LoadTestAppointmentData())
                    return;
            }

            lblTotalFees.Text=(Convert.ToSingle(lblFees.Text)+Convert.ToSingle(lblRAppFees.Text)).ToString();

            if (!_HandelActiveTestAppointmentConstraint())
                return;
            if(!_HandelLockedTestAppointmentConstraint())
                return ;
            if (!_HandelPreviousTestAppointmentConstraint())
                return;
        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsTestAppointemnt.Find(_TestAppointemntID);

            if (_TestAppointment == null)
            {
                MessageBox.Show("Error : Error While Loading The Test Appointment Data", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }
            lblTitle.Text = "Update Test";
            

            lblFees.Text = _TestAppointment.PaidFees.ToString();

            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointemntDate) < 0)
                dtpDate.MinDate = DateTime.Now;
            else
                dtpDate.MinDate = _TestAppointment.AppointemntDate;

            dtpDate.Value = _TestAppointment.AppointemntDate;

            if (_TestAppointment.RetakeTestApplicationID > 0)
            {
                lblRTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Edit Retake Test";
                lblRAppFees.Text = _TestAppointment.RetakeTestApplicationInfo.PaidFees.ToString();
            } 
            else
            {
                
                gbRetakeTestInfo.Enabled = false;
                lblRTestAppID.Text = "N/A";
                lblRAppFees.Text = "0";
            }
            return true;

        }

        private bool _HandelActiveTestAppointmentConstraint()
        {
            if(_Mode ==enMode.AddNew&&_LocalDrivingLicenseApplication.IsThereAnActiveSchedualTest((int)_TestType))
            {
                lblMessage.Visible = true;
                lblMessage.Text = "This Person Already Have Active TestAppointment For This TestType";
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return false;
            }
            else
                return true;
        }

        private bool _HandelLockedTestAppointmentConstraint()
        {
            if (_Mode == enMode.AddNew && _LocalDrivingLicenseApplication.DosePassTestType((int)_TestType))
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Person Already Sat For This Test";
                btnSave.Enabled = false;
                dtpDate.Enabled = false;
                return false;
            }
            else
                return true;
        }

        private bool _HandelPreviousTestAppointmentConstraint()
        {
            switch(_TestType)
            {
                case enTestType.VisionTest:
                    lblMessage.Visible = false;
                    return true;
                case enTestType.WrittenTest:
                    if(!_LocalDrivingLicenseApplication.DosePassTestType((int)enTestType.VisionTest))
                    {
                        lblMessage.Visible=true;
                        lblMessage.Text = "Cannot Sechdule ,Vision Test Should Be Passed First...";
                        dtpDate.Enabled=false;
                        btnSave.Enabled=false;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                case enTestType.StreetTest:
                    if (!_LocalDrivingLicenseApplication.DosePassTestType((int)enTestType.WrittenTest))
                    {
                        lblMessage.Visible = true;
                        lblMessage.Text = "Cannot Sechdule ,Written Test Should Be Passed First...";
                        dtpDate.Enabled = false;
                        btnSave.Enabled = false;
                        return false;
                    }
                    else
                    {
                        return true;
                    }


            }

            return false;
        }

        private bool _HandelRetakeTestApplication()
        {
            if(_Mode==enMode.AddNew && _CreationMode==enCreationMode.RetakeTest)
            {
                clsApplication Application=new clsApplication();

                Application.ApplicantPersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                Application.ApplicationTypeID = clsApplication.enApplicationType.RetakeTest;
                Application.ApplicationDate=DateTime.Now;
                Application.ApplicationStatus = clsApplication.enStatus.New;
                Application.CreatedByUserID = clsGlobalSettings.UserID;
                Application.LastStatusDate = DateTime.Now;
                Application.PaidFees = clsApplicationType.Find((int)clsApplicationType.enApplicationType.RetakeTest).ApplicationTypeFee;
                
                if(!Application.Save())
                {
                    MessageBox.Show("Error : Faild To Create Retake Test Application", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return false;
                }
                Application.LastStatusDate=DateTime.Now;
                Application.SetComplete();
                MessageBox.Show("Retake Test Application Created Successfully With ID : " + Application.ApplicationID.ToString(),"Application Done",MessageBoxButtons.OK,MessageBoxIcon.Information);
                _TestAppointment.RetakeTestApplicationID= Application.ApplicationID;
                lblRTestAppID.Text = Application.ApplicationID.ToString();
            }
            return true;
        }

        private void _Save()
        {
            if (!_HandelRetakeTestApplication())
                return;

            _TestAppointment.TestTypeID = (int)TestType;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointemntDate = dtpDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreatedByUserID = clsGlobalSettings.UserID;

            if(_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Test Appointment Saved Successfully With ID : "+ _TestAppointment.TestAppointmentID.ToString(), "Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);  
            }
            else
                MessageBox.Show("Error : Error While Saving The Test Appointment", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Save();
        }
    }
}

