using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DVLD_BLL
{
    public class clsTestAppointemnt
    {
        enum enMode { eAdd = 1, eUpdate = 2 }

        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }
        public DVLD_BLL.clsTestType TestTypeInfo { get; set; }
        public int LocalDrivingLicenseApplicationID { get; set; }

        public DVLD_BLL.clsLocalDrivingApplication LocalDrivingApplicationInfo { get; set; }

        public DateTime AppointemntDate { get; set; }   

        public float PaidFees { get; set; }

        public int CreatedByUserID { get; set; }

        public DVLD_BLL.clsUser UserInfo { get; set; }

        public bool IsLocked { get; set; }

        public int RetakeTestApplicationID { get; set; }

        public clsApplication RetakeTestApplicationInfo;

        private enMode _Mode;

        //------------------------
        public clsTestAppointemnt()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = -1;
            this.LocalDrivingLicenseApplicationID= -1;
            this.AppointemntDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = -1;
            _Mode = enMode.eAdd;
        }

        public clsTestAppointemnt(int TestAppointmentID,int TestTypeID,int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate,float PaidFees,int CreatedByUserID,bool IsLocked,int RetakeTestApplicationID)
        {
        this.TestAppointmentID=TestAppointmentID;
        this.TestTypeID=TestTypeID;
        TestTypeInfo=DVLD_BLL.clsTestType.Find(TestTypeID);
        this.LocalDrivingLicenseApplicationID=LocalDrivingLicenseApplicationID;
        LocalDrivingApplicationInfo = DVLD_BLL.clsLocalDrivingApplication.FindLocalDrivingApplication(LocalDrivingLicenseApplicationID);
        this.AppointemntDate = AppointmentDate;
        this.PaidFees=PaidFees;
        this.CreatedByUserID=CreatedByUserID;
        UserInfo=DVLD_BLL.clsUser.FindUserByUserID(CreatedByUserID);
        this.IsLocked = IsLocked;
        this.RetakeTestApplicationID=RetakeTestApplicationID;
            if (RetakeTestApplicationID > 0)
                this.RetakeTestApplicationInfo = clsApplication.FindBaseApplication(RetakeTestApplicationID);
        _Mode = enMode.eUpdate;
        }
        //------------------------
                         

        private bool _AddNewTestAppointment()
        {
            this.TestAppointmentID = DVLD_DAL.clsTestAppointmentsData.AddNewTestAppointement(TestTypeID, LocalDrivingLicenseApplicationID, AppointemntDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            return this.TestAppointmentID > 0;

        }
        
        private bool _UpdateTestAppointment()
        {
            return DVLD_DAL.clsTestAppointmentsData.UpdateTestAppointement(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointemntDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.eAdd:
                    _Mode=enMode.eUpdate;
                    return _AddNewTestAppointment();
                case enMode.eUpdate:
                    return _UpdateTestAppointment();
            }
            return false;



        }

        public static clsTestAppointemnt Find(int TestAppointmentID)
        {
            int TestTypeID = -1, LocalDrivingApp = -1,CreatedByUserID=-1,RetakeTestApp=-1;
            DateTime AppointmentDate=DateTime.MinValue;
            float PaidFees = 0;
            bool IsLocked = false;
            if (DVLD_DAL.clsTestAppointmentsData.GetTestAppointmentInfoByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingApp, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApp))
            {
                return new clsTestAppointemnt(TestAppointmentID, TestTypeID, LocalDrivingApp, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApp); ;
            }
            else
                return null;
        }

        public static clsTestAppointemnt FindByLocalDrivingLicenseApplicationID(int LocalDrivingLicenseApplicationID)
        {
            int TestTypeID = -1, TestAppointmentID = -1, CreatedByUserID = -1, RetakeTestApp = -1;
            DateTime AppointmentDate = DateTime.MinValue;
            float PaidFees = 0;
            bool IsLocked = false;
            if (DVLD_DAL.clsTestAppointmentsData.GetTestAppointmentInfoByLocalDrivingAppID(LocalDrivingLicenseApplicationID,ref TestAppointmentID, ref TestTypeID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApp))
            {
                return new clsTestAppointemnt(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApp); ;
            }
            else
                return null;
        }


        public static DataTable GetAllTestAppointments()
        {
            return DVLD_DAL.clsTestAppointmentsData.GetAllTestAppointments();
        }

        public static DataTable GetAllTestAppointmentsByLocalLicenseID(int LocalLicenseApplicationID,int TestTypeID)
        {
            return DVLD_DAL.clsTestAppointmentsData.GetAllTestAppointmentsForLocalLicenseID(LocalLicenseApplicationID,TestTypeID);
        }

        public static int GetTrailsCount(int LocalDrivingApplicationID,int TestTypeID)
        {
            return DVLD_DAL.clsLocalDrivingApplicationData.TotalTrailsPerTestType(LocalDrivingApplicationID,TestTypeID); ;
        }
    
           public static bool IsTestAppointmentLockedForLDAID(int LocalDrivingApplicationID, int TestTypeID)
        {
            return DVLD_DAL.clsTestAppointmentsData.IsLastTestAppointmentLocked(LocalDrivingApplicationID, TestTypeID); 
        }

        
    }
}
