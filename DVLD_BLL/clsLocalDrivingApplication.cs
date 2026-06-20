using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLD_DAL;

namespace DVLD_BLL
{
    public class clsLocalDrivingApplication :clsApplication
    {
        private enum enMode { eAdd=1,eUpdate=2}
        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        public clsLicenseClasse LicenseClassInfo;

        private enMode _Mode;

        //---------------Constructors-------------
        public clsLocalDrivingApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
            this._Mode = enMode.eAdd;
        }

        public clsLocalDrivingApplication(int LocalDrivingLicenseApplicationID,int LicenseClassID, int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
            enStatus ApplicationStatus, DateTime LastStatusDate,
            float PaidFees, int CreatedByUserID)
        {
            this.LocalDrivingLicenseApplicationID=LocalDrivingLicenseApplicationID;            
            this.LicenseClassID=LicenseClassID;
            this.LicenseClassInfo=clsLicenseClasse.Find(LicenseClassID);
            this.ApplicationID=ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.PersonInfo = clsPeople.FindPerson(ApplicantPersonID);
            this.ApplicationDate=ApplicationDate;
            this.ApplicationTypeID = (enApplicationType)ApplicationTypeID;
            this.ApplicationStatus=ApplicationStatus;
            this.LastStatusDate=LastStatusDate;
            this.PaidFees=PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo = clsUser.FindUserByUserID(CreatedByUserID);
            _Mode = enMode.eUpdate;
        }

        //-----------------------------

        private bool _AddLocalDrivingApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingApplicationData.AddNewLocalDrivingApplication(ApplicationID, LicenseClassID);
            return this.LocalDrivingLicenseApplicationID > 0;
        }

        private bool _UpdateLocalDrivingApplication()
        {
            return clsLocalDrivingApplicationData.UpdateLocalDrivingApplication(LocalDrivingLicenseApplicationID, ApplicationID, LicenseClassID);
        }

        public bool Save()
        {
            base._Mode =(clsApplication.enMode)this._Mode;
            if (!base.Save())
                return false;


            switch(_Mode)
            {
                case enMode.eUpdate:
                    return _UpdateLocalDrivingApplication();
                case enMode.eAdd:
                    _Mode = enMode.eUpdate;
                    return _AddLocalDrivingApplication();
            }
            return false;
        }

        public static clsLocalDrivingApplication FindLocalDrivingApplication(int LocalDrivingApplicationID)
        {
            int _LicenseClassID=-1;
            int _ApplicationID = -1, _ApplicantPersonID = 1;
            DateTime _ApplicationDate = DateTime.Now;
            int _ApplicationTypeID = -1;
            byte _ApplicationStatus=0;
            DateTime _LastStatusDate=DateTime.Now;
            float _PaidFees = 0; 
            int _CreatedByUserID = -1;

            if (DVLD_DAL.clsLocalDrivingApplicationData.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingApplicationID,ref _ApplicationID,ref _LicenseClassID))
            {
                if (DVLD_DAL.clsApplicationData.GetApplicationInfoByID(_ApplicationID, ref _ApplicantPersonID, ref _ApplicationDate, ref _ApplicationTypeID, ref _ApplicationStatus, ref _LastStatusDate, ref _PaidFees, ref _CreatedByUserID))
                {
                    return new clsLocalDrivingApplication(LocalDrivingApplicationID, _LicenseClassID, _ApplicationID, _ApplicantPersonID, _ApplicationDate, _ApplicationTypeID, (enStatus)_ApplicationStatus, _LastStatusDate, _PaidFees, _CreatedByUserID);
                }
                else
                    return null;

            }
            else
                return null;
        }

        public static clsLocalDrivingApplication FindLocalDrivingApplicationByApplicationID(int ApplicationID)
        {
            int _LicenseClassID = -1;
            int _LocalDrivingApplicationID = -1, _ApplicantPersonID = 1;
            DateTime _ApplicationDate = DateTime.Now;
            int _ApplicationTypeID = -1;
            byte _ApplicationStatus = 0;
            DateTime _LastStatusDate = DateTime.Now;
            float _PaidFees = 0;
            int _CreatedByUserID = -1;

            if (DVLD_DAL.clsLocalDrivingApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID(ApplicationID,ref _LocalDrivingApplicationID, ref _LicenseClassID))
            {
                if (DVLD_DAL.clsApplicationData.GetApplicationInfoByID(ApplicationID, ref _ApplicantPersonID, ref _ApplicationDate, ref _ApplicationTypeID, ref _ApplicationStatus, ref _LastStatusDate, ref _PaidFees, ref _CreatedByUserID))
                {
                    return new clsLocalDrivingApplication(_LocalDrivingApplicationID, _LicenseClassID, ApplicationID, _ApplicantPersonID, _ApplicationDate, _ApplicationTypeID, (enStatus)_ApplicationStatus, _LastStatusDate, _PaidFees, _CreatedByUserID);
                }
                else
                    return null;

            }
            else
                return null;
        }

        public static clsLocalDrivingApplication FindLocalDrivingApplicationByLicenseID(int LicenseID)
        {
            int _LicenseClassID = -1, _ApplicationID = -1 ;
            int _LocalDrivingApplicationID = -1, _ApplicantPersonID = 1;
            DateTime _ApplicationDate = DateTime.Now;
            int _ApplicationTypeID = -1;
            byte _ApplicationStatus = 0;
            DateTime _LastStatusDate = DateTime.Now;
            float _PaidFees = 0;
            int _CreatedByUserID = -1;

            if (DVLD_DAL.clsLocalDrivingApplicationData.GetLocalDrivingLicenseApplicationInfoByLicenseID(LicenseID,ref _LocalDrivingApplicationID, ref _ApplicationID, ref _LicenseClassID))
            {
                if (DVLD_DAL.clsApplicationData.GetApplicationInfoByID(_ApplicationID, ref _ApplicantPersonID, ref _ApplicationDate, ref _ApplicationTypeID, ref _ApplicationStatus, ref _LastStatusDate, ref _PaidFees, ref _CreatedByUserID))
                {
                    return new clsLocalDrivingApplication(_LocalDrivingApplicationID, _LicenseClassID, _ApplicationID, _ApplicantPersonID, _ApplicationDate, _ApplicationTypeID, (enStatus)_ApplicationStatus, _LastStatusDate, _PaidFees, _CreatedByUserID);
                }
                else
                    return null;

            }
            else
                return null;
        }

        public static bool DeleteLocalDrivingApplication(int LocalDriivingApplicationID)
        {
            return DVLD_DAL.clsLocalDrivingApplicationData.DeleteLocalDrivingApplication(LocalDriivingApplicationID);
        }

        public static DataTable GetAllLocalApplicationsView()
        {
            return DVLD_DAL.clsLocalDrivingApplicationData.GetAllLocalDrivingLicensesApplications();
        }

        public static bool IsPersonHaveActiveLocalDrivingApplicationClass(int PersonID,int LicenseClassID)
        {
            return DVLD_DAL.clsLocalDrivingApplicationData.IsPersonHaveActiveLocalApplication(PersonID, LicenseClassID); ;
        }
        public static bool IsPersonHaveLocalLicense(int PersonID,int LicenseClassID)
        {
            return DVLD_DAL.clsLocalDrivingApplicationData.IsPersonHaveLocalLicense(PersonID, LicenseClassID); 
        }
        public static bool DoseAttendTestType(int LocalDrivingApplicationID,int TestType)
        {
            return clsLocalDrivingApplicationData.DoseAttendTestType(LocalDrivingApplicationID, TestType);
        }

        public bool DoseAttendTestType(int TestType)
        {
            return clsLocalDrivingApplication.DoseAttendTestType(this.LocalDrivingLicenseApplicationID, TestType);
        }

        public int TotalTrailsPerTest(int TestTypeID)
        {
            return clsTestAppointemnt.GetTrailsCount(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static int TotalTrailsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsTestAppointemnt.GetTrailsCount(LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static bool DosePassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            return clsLocalDrivingApplicationData.DosePassTestType(LocalDrivingLicenseApplicationID,TestTypeID);    
        }
        public  bool DosePassTestType(int TestTypeID)
        {
            return clsLocalDrivingApplicationData.DosePassTestType(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }

         public bool IsThereAnActiveSchedualTest(int TestTypeID)
        {
            return clsTestAppointmentsData.DoseHaveActiveAppointment(this.LocalDrivingLicenseApplicationID, TestTypeID);
        }

        public static bool IsThereAnActiveSchedualTest(int LocalDrivingApplicationID,int TestTypeID)
        {
            return clsTestAppointmentsData.DoseHaveActiveAppointment(LocalDrivingApplicationID, TestTypeID);
        }

      
        public int GetPassedTestsCount()
        {
            return clsTest.GetPassedTestsCount(this.LocalDrivingLicenseApplicationID);
        }

        public bool PassedAllTests()
        {
            return clsTest.GetPassedTestsCount(this.LocalDrivingLicenseApplicationID)==3;
        }

        public int GetLastestActiveLicense()
        {
            return clsLicensesData.GetActiveLicenseIDByPersonID(this.ApplicantPersonID,this.LicenseClassID);
        }

        //GetPassedTestCount
        //GetActiveLiceseID
        public int IssueLicnseForFirstTime(string Notes,int CreatedByUserID)
        {
            //Cheack If The Person Is Already Driver...
            clsDriver Driver;
            if (!clsDriver.IsThisPersonDriver(this.ApplicantPersonID))
            {
                Driver = new clsDriver();
                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                Driver.CreatedDate = DateTime.Now;
                if (!Driver.Save())
                {
                    return -1;
                }
            }
            else
            {
                //Get Driver info
                Driver = clsDriver.FindDriverByPersonID(this.ApplicantPersonID);
            }
            //Create The New License
            clsLicense License = new DVLD_BLL.clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = Driver.DriverID;
            License.LicenseClass = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = License.IssueDate.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = DVLD_BLL.clsLicense.enIssueReason.eFirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                this.SetComplete();
                return License.LicenseID;
            }
            else
                return -1;




        }
        //IsLicneseIssued
    }
}
