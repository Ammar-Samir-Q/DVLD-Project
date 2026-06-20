using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DVLD_BLL
{
    public class clsTest
    {
        enum enMode { eAdd=1,eUpdate=2}

        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }

        public clsTestAppointemnt TestAppointmentInfo { get; set; }

        public byte TestResult { get; set; }

        public string Notes { get; set; }

        public int CreatedByUserID { get; set; }

        public clsUser UserInfo { get; set; }

        private enMode _Mode;

        //---------------

        public clsTest ()
        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = 0;
            this.Notes = "";
            this.CreatedByUserID = -1;

            _Mode = enMode.eAdd;
        }

        public clsTest(int TestID,int TestAppointmentID,byte TestResult,string Notes,int CreatedByUserID)
        {
            this.TestID=TestID;
            this.TestAppointmentID=TestAppointmentID;
            this.TestAppointmentInfo=clsTestAppointemnt.Find(TestAppointmentID);
            this.TestResult=TestResult;
            this.Notes=Notes;
            this.CreatedByUserID=CreatedByUserID;
            this.UserInfo=clsUser.FindUserByUserID(CreatedByUserID);

            _Mode = enMode.eUpdate;
        }
        
        //---------------

        private bool _AddTest()
        {
            this.TestID = DVLD_DAL.clsTests.AddNewTest(TestAppointmentID, TestResult, Notes, CreatedByUserID);
            return TestID > 0;
        }

        private bool _UpdateTest()
        {
            return DVLD_DAL.clsTests.UpdateTest(TestID,TestAppointmentID, TestResult,Notes, CreatedByUserID);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.eAdd:
                    _Mode = enMode.eUpdate;
                    return _AddTest();
                case enMode.eUpdate:
                    return _UpdateTest();
            }
            return false;
        }
        
        public static clsTest Find(int TestID)
        {
            int TestAppointmentID = -1, CreatedByUserID = -1;
            byte TestResult = 0;
            string Notes = "";
            if (DVLD_DAL.clsTests.GetTestInfoByID(TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            else
                return null;

        }

        public static clsTest FindByTestAppointment(int TestAppointmentID)
        {
            int TestID = -1, CreatedByUserID = -1;
            byte TestResult = 0;
            string Notes = "";
            if (DVLD_DAL.clsTests.GetTestInfoByTestAppointmentID(TestAppointmentID, ref TestID, ref TestResult, ref Notes, ref CreatedByUserID))
            {
                return new clsTest(TestID, TestAppointmentID, TestResult, Notes, CreatedByUserID);
            }
            else
                return null;

        }


        public DataTable GetAllTests()
        {
            return DVLD_DAL.clsTests.GetAllTests();
        }

        public static int GetPassedTestsCount(int LocalDrivingApplicationID)
        {
            return DVLD_DAL.clsTests.CountPassedTests(LocalDrivingApplicationID);
        }

        public static int GetTestResultByLocalDrivingAppID(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            return DVLD_DAL.clsTests.GetTestResultByLDAID(LocalDrivingLicenseApplicationID,TestTypeID);   
        }
    
        public static bool PassedAllTests(int LocalDrivingApplicationID)
        {
            return GetPassedTestsCount(LocalDrivingApplicationID) == 3;
        }

    }
}
