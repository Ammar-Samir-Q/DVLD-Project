using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLD_DAL;
namespace DVLD_BLL
{
    public class clsInternationaLicense:clsApplication
    {
        enum enMode { eAdd=1,eUpdate=2}

        public int InternationalLicenseID { get; set; }

        public int DriverID { get; set; }

        public clsDriver DriverInfo { get; set; }
        
        public int IssueUsingLicenseID { get; set; }
        public clsLicense LicenseInfo { get; set; }   

        public DateTime IssueDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsActive { get; set; }

        private enMode _Mode;

        //----------------
        public clsInternationaLicense()
        {
            this.InternationalLicenseID = -1;
            this.ApplicationID = -1;
            this.IssueUsingLicenseID = -1;
            this.DriverID = -1;
            this.IssueDate=DateTime.MinValue;
            this.ExpirationDate=DateTime.MinValue;
            this.IsActive = false;
            this.CreatedByUserID = -1;
            _Mode = enMode.eAdd;
        }
        public clsInternationaLicense(int ApplicationID,int ApplicantPersonID,DateTime ApplicationDate,int ApplicationTypeID,enStatus ApplicationStatus,DateTime LastStatusDate,float PaidFees, int CreatedByUserID, int InternationalLicenseID,int DriverID,int IssuedUsingLicenseID,DateTime IssueDate,DateTime ExpirationDate,bool IsActive)
        {
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.PersonInfo = clsPeople.FindPerson(ApplicantPersonID);
            base.ApplicationStatus = ApplicationStatus;
            base.ApplicationDate = ApplicationDate;
            base.LastStatusDate = LastStatusDate;
            base.CreatedByUserID = CreatedByUserID;
            base.UserInfo=clsUser.FindUserByUserID(CreatedByUserID);
            base.PaidFees = PaidFees;
            base.ApplicationTypeID = (enApplicationType)ApplicationTypeID;
            base.ApplicationTypeInfo=clsApplicationType.Find(ApplicationTypeID);

            this.InternationalLicenseID=InternationalLicenseID;
            this.DriverID=DriverID;
            this.DriverInfo = clsDriver.FindDriver(DriverID);
            this.IssueUsingLicenseID=IssuedUsingLicenseID;
            this.LicenseInfo=clsLicense.FindLicense(IssuedUsingLicenseID);
            this.IssueDate=IssueDate;
            this.ExpirationDate =ExpirationDate;
            this.IsActive =IsActive;
            this.CreatedByUserID=CreatedByUserID;
            this.UserInfo = clsUser.FindUserByUserID(CreatedByUserID);
            _Mode = enMode.eUpdate;
        }

        //---------------

        private bool _AddNew()
        {
            this.InternationalLicenseID = DVLD_DAL.clsInternationalApplicationsData.AddNewInternationalLicense(ApplicationID, DriverID, IssueUsingLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
            return this.InternationalLicenseID > 0;
        }
        private bool _Update()
        {
            return DVLD_DAL.clsInternationalApplicationsData.UpdateInternationalLicense(InternationalLicenseID, ApplicationID, DriverID, IssueUsingLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID);
        }

        public bool Save()
        {
            base._Mode=(clsApplication.enMode)this._Mode;
            if (!base.Save())
                return false;

            switch(_Mode)
            {
                case enMode.eAdd:
                    _Mode = enMode.eUpdate;
                    return _AddNew();
                case enMode.eUpdate:
                    return _Update();
            }
            return false;

        }

        public static clsInternationaLicense FindInternationalLicense(int InternationalLicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseID = -1, UserID = -1, ApplicantPersonID = -1, ApplicationTypeID = 1;
            byte ApplicationStatus=1;
            DateTime IssueDate=DateTime.MinValue,ExpirationDate=DateTime.MinValue,ApplicationDate=DateTime.MinValue,LastStatusDate=DateTime.MinValue;
            float PaidFees = 0;
            bool IsActive = false;
            if (clsInternationalApplicationsData.GetInternationalLicenseByID(InternationalLicenseID, ref ApplicationID, ref DriverID, ref LicenseID, ref IssueDate, ref ExpirationDate, ref IsActive, ref UserID))
            {
                if (clsApplicationData.GetApplicationInfoByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref UserID))
                    return new clsInternationaLicense(ApplicationID, ApplicantPersonID, ApplicationDate,ApplicationTypeID, (enStatus)ApplicationStatus, LastStatusDate, PaidFees, UserID, InternationalLicenseID, DriverID, LicenseID, IssueDate, ExpirationDate, IsActive);
                else
                    return null;
            }
            else
                return null;

        }

        public static clsInternationaLicense FindInternationalLicenseByLicenseID(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1,InternationalLicenseID= -1, UserID = -1, ApplicantPersonID = -1, ApplicationTypeID = 1;
            byte ApplicationStatus = 1;
            DateTime IssueDate = DateTime.MinValue, ExpirationDate = DateTime.MinValue, ApplicationDate = DateTime.MinValue, LastStatusDate = DateTime.MinValue;
            float PaidFees = 0;
            bool IsActive = false;
            if (clsInternationalApplicationsData.GetInternationalLicenseByLocalLicenseID(LicenseID,ref InternationalLicenseID, ref ApplicationID, ref DriverID , ref IssueDate, ref ExpirationDate, ref IsActive, ref UserID))
            {
                if (clsApplicationData.GetApplicationInfoByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref UserID))
                    return new clsInternationaLicense(ApplicationID, ApplicantPersonID, ApplicationDate,ApplicationTypeID, (enStatus)ApplicationStatus, LastStatusDate, PaidFees, UserID, InternationalLicenseID, DriverID, LicenseID, IssueDate, ExpirationDate, IsActive);
                else
                    return null;
            }
            else
                return null;

        }


        public static bool DeleteInternationalLicense(int InternationalLicenseID)
        {
            return DVLD_DAL.clsInternationalApplicationsData.DeleteInternationalLicnese(InternationalLicenseID);
        }

        public static DataTable GetAllInternationalLicecnse()
        {
            return DVLD_DAL.clsInternationalApplicationsData.GetAllInternationalLicenses();
        }
        public static DataTable GetAllInternationalLicensesForPersonID(int PersonID)
        {
            return DVLD_DAL.clsInternationalApplicationsData.GetAllInternationalLicensesForPersonID(PersonID);
        }

        public static bool IsPersonHaveInternationalLicense(int PersonID)
        {
            return DVLD_DAL.clsInternationalApplicationsData.IsThisPersonHaveInternationalLicecnse(PersonID);
        }
        public static int GetActiveInternationalLicenseID(int DriverID)
        {
            return clsInternationalApplicationsData.GetActiveInternationalLicenseByDriverID(DriverID);
        }

    }
}
