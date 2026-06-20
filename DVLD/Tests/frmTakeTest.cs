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
    public partial class frmTakeTest : Form
    {
        private int _TestAppointmentID = -1;
        private clsTestType.enTestType _TestTypeID =clsTestType.enTestType.VisionTest;
        private clsTestAppointemnt _TestAppointment;
        private int _TestID = -1;
        clsTest _Test;

        public frmTakeTest(int TestAppointmentID,clsTestType.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestAppointmentID=TestAppointmentID;
            _TestTypeID = TestTypeID;
        }
    
        private void _LoadData()
        {
            _TestAppointment = DVLD_BLL.clsTestAppointemnt.Find(_TestAppointmentID);
            if (_TestAppointment != null)
            {
                gbTestInfo.Text = _TestTypeID == clsTestType.enTestType.VisionTest ? "Vision-Test" : _TestTypeID == clsTestType.enTestType.WrittenTest ? "Written-Test" : "Street-Test";
                rbPass.Checked = true;
                lblDLApp.Text = _TestAppointment.LocalDrivingLicenseApplicationID.ToString();
                lblClass.Text = _TestAppointment.LocalDrivingApplicationInfo.LicenseClassInfo.ClassName;
                lblName.Text = _TestAppointment.LocalDrivingApplicationInfo.PersonInfo.FullName();
                lblTrial.Text = DVLD_BLL.clsTestAppointemnt.GetTrailsCount(_TestAppointment.LocalDrivingLicenseApplicationID, _TestAppointment.TestTypeID).ToString();
                lblDate.Text = _TestAppointment.AppointemntDate.ToString();
                lblFees.Text = _TestAppointment.PaidFees.ToString();
                lblTestID.Text = "Not Taken Yet";
            }
            else
            {
                btnSave.Enabled=false;
                rbFail.Enabled = false;
                rbPass.Enabled = false;
            }

            _Test = clsTest.FindByTestAppointment(_TestAppointmentID);
            if (_Test != null)
            {
                _TestID = _Test.TestID;
                if (_Test.TestResult == 1)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = false;

                txtNotes.Text = _Test.Notes.ToString();

                lblMesseage.Visible = true;
                rbPass.Enabled = false;
                rbFail.Enabled = false;
            }
            else
                _Test = new clsTest();

        }
    
        private void _Save()
        {
            if(MessageBox.Show("Are You Sure You Want To Save This Test ? After That You Can not Change The Result", "Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
            {
                return;
            }
            _Test.TestAppointmentID = _TestAppointmentID;
            _Test.TestResult = rbPass.Checked == true ? (byte)1 : (byte)0 ;
            if(string.IsNullOrEmpty(txtNotes.Text))
            {
                _Test.Notes = "";
            }
            else
                _Test.Notes=txtNotes.Text;

            _Test.CreatedByUserID=clsGlobalSettings.UserID;

            
                if (_Test.Save())
                {
                    lblTestID.Text = _Test.TestID.ToString();
                    MessageBox.Show("Test Saved Succussfully", "Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                btnSave.Enabled = false;
                    return;
                }
                else
                    MessageBox.Show("Error : Faild To Save The test..", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            


        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _Save();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
