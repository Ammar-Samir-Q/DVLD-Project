using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLD_DAL;
namespace DVLD_BLL
{
    public class clsLicense
    {
        private enum enMode { eAdd = 1, eUpdate = 2 }
        public enum enIssueReason { eFirstTime = 1, eRenew = 2, eReplacementForDamaged = 3, eReplacementForLost = 4 }

        public int LicenseID { get; set; }
        public int ApplicationID { get; set; }
        public clsApplication ApplicationInfo { get; set; }

        public int DriverID { get; set; }
        public clsDriver DriverInfo { get; set; }

        public int LicenseClass { get; set; }
        public clsLicenseClasse LicesneClassInfo { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public string Notes { get; set; }

        public float PaidFees { get; set; }

        public bool IsActive { get; set; }

        public enIssueReason IssueReason { get; set; }

        public int CreatedByUserID { get; set; }

        public  clsDetainedLicense DetainedInfo;

        public clsUser UserInfo { get; set; }

        private enMode _Mode;

        //--------------------------

        public clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.MinValue;
            this.ExpirationDate = DateTime.MinValue;
            this.Notes = "";
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = enIssueReason.eFirstTime;
            this.CreatedByUserID = -1;
            _Mode = enMode.eAdd;
        }

        public clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate
            , DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            this.LicenseID= LicenseID;
            this.DetainedInfo=clsDetainedLicense.FindByLicenseID(LicenseID);
            this.ApplicationID= ApplicationID;
            ApplicationInfo = clsApplication.FindBaseApplication(ApplicationID);
            this.DriverID= DriverID;
            DriverInfo=clsDriver.FindDriver(DriverID);
            this.LicenseClass= LicenseClassID;
            LicesneClassInfo=clsLicenseClasse.Find(LicenseClassID);
            this.IssueDate= IssueDate;
            this.ExpirationDate= ExpirationDate;
            this.Notes= Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = (enIssueReason)IssueReason;
            this.CreatedByUserID= CreatedByUserID;
            UserInfo = clsUser.FindUserByUserID(CreatedByUserID);
            _Mode = enMode.eUpdate;
        }

        //-------------------------

        private bool _AddLicense()
        {
            this.LicenseID = DVLD_DAL.clsLicensesData.AddLicense(ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (byte)IssueReason, CreatedByUserID);

            return this.LicenseID > 0;
        }

        private bool _UpdateLicense()
        {
            return DVLD_DAL.clsLicensesData.UpdateLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (byte)IssueReason, CreatedByUserID);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.eAdd:
                    _Mode = enMode.eUpdate;
                    return _AddLicense();
                case enMode.eUpdate:
                    return _UpdateLicense();   
            }
            return false;
        }

        public static clsLicense FindLicense(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1;
            DateTime IssueDate=DateTime.MinValue,ExpirationDate=DateTime.MinValue;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 1;
            if (DVLD_DAL.clsLicensesData.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            }
            else
                return null;
        
        }

        public static clsLicense FindLicenseByLocalLicenseID(int LocalLicenseApplicationID)
        {
            int LicenseID=-1,ApplicationID = -1, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.MinValue, ExpirationDate = DateTime.MinValue;
            string Notes = "";
            float PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 1;
            if (DVLD_DAL.clsLicensesData.GetLicenseInfoByLocalLicenseApplicationID(LocalLicenseApplicationID,ref LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID);
            }
            else
                return null;

        }


        public static DataTable GetAllLicenses()
        {
          return DVLD_DAL.clsLicensesData.GetAllLicenses();
        }

        public static DataTable GetAllLicensesForPersonID(int PersonID)
        {
            return DVLD_DAL.clsLicensesData.GetAllLicensesForPersonID(PersonID);
        }

        public static bool DeleteLicense(int LicenseID)
        {
            return DVLD_DAL.clsLicensesData.DeleteLicense(LicenseID);
        }

        public string GetIssueReasonText()
        {
            switch(this.IssueReason)
            {
                case enIssueReason.eFirstTime:
                    return "First Time";
                case enIssueReason.eRenew:
                    return "Renew";
                case enIssueReason.eReplacementForDamaged :
                    return "Replacement For Damaged";
                case enIssueReason.eReplacementForLost:
                    return "Replacement For Lost";
            }
            return "Error";
        }
        public bool DeactivateCurrentLicense()
        {
            return clsLicensesData.ActiveDeActivateLicense(this.LicenseID, false);
        }

        public bool ActiveCurrentLicense()
        {
            return clsLicensesData.ActiveDeActivateLicense(this.LicenseID, true);
        }

        public clsLicense Renew(string Notes,int CreatedByUserID)
        {
            //Returens Renewed License


            clsApplication Application=new clsApplication();
            Application.ApplicantPersonID = this.ApplicationInfo.ApplicantPersonID;
            Application.ApplicationDate=DateTime.Now;
            Application.ApplicationTypeID = clsApplication.enApplicationType.RenewDrivingLicense;
            Application.ApplicationStatus = clsApplication.enStatus.New;
            Application.LastStatusDate=DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)clsApplicationType.enApplicationType.RenewDrivingLicense).ApplicationTypeFee;
            Application.CreatedByUserID = CreatedByUserID;



            if (!Application.Save())
                return null;

            clsLicense NewLicense=new clsLicense();
            NewLicense.ApplicationID=Application.ApplicationID;
            NewLicense.DriverID=this.DriverID;
            NewLicense.LicenseClass=this.LicenseClass;
            NewLicense.IssueDate=DateTime.Now;
            NewLicense.ExpirationDate = NewLicense.IssueDate.AddYears(clsLicenseClasse.Find(NewLicense.LicenseClass).DefaultValidityLength);
            NewLicense.Notes=Notes;
            NewLicense.PaidFees = this.PaidFees;
            NewLicense.IsActive=true;
            NewLicense.IssueReason = enIssueReason.eRenew;
            NewLicense.CreatedByUserID=CreatedByUserID;

            if (!NewLicense.Save())
                return null;

            Application.SetComplete();
            this.DeactivateCurrentLicense();
            return NewLicense;


        }

        public clsLicense ReplaceForDamaged(string Notes,int CreatedByUserID)
        {
            //Returens Replaced LicenseID


            clsApplication Application = new clsApplication();
            Application.ApplicantPersonID = this.ApplicationInfo.ApplicantPersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            Application.ApplicationStatus = clsApplication.enStatus.New;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)clsApplicationType.enApplicationType.ReplaceDamagedDrivingLicense).ApplicationTypeFee;
            Application.CreatedByUserID = CreatedByUserID;



            if (!Application.Save())
                return null;


            clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = NewLicense.IssueDate.AddYears(clsLicenseClasse.Find(NewLicense.LicenseClass).DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = 0; //No Fees Because Its Replacement
            NewLicense.IsActive = true;
            NewLicense.IssueReason = enIssueReason.eReplacementForDamaged;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if (!NewLicense.Save())
                return null;

            Application.SetComplete();
            this.DeactivateCurrentLicense();
            return NewLicense;


        }

        public clsLicense ReplaceForLost(string Notes,int CreatedByUserID)
        {
            //Returens Replaced License


            clsApplication Application = new clsApplication();
            Application.ApplicantPersonID = this.ApplicationInfo.ApplicantPersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = clsApplication.enApplicationType.ReplaceLostDrivingLicecnse;
            Application.ApplicationStatus = clsApplication.enStatus.New;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)clsApplicationType.enApplicationType.ReplaceLostDrivingLicecnse).ApplicationTypeFee;
            Application.CreatedByUserID = CreatedByUserID;



            if (!Application.Save())
                return null;


            clsLicense NewLicense = new clsLicense();
            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = NewLicense.IssueDate.AddYears(clsLicenseClasse.Find(NewLicense.LicenseClass).DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = 0; //No Fees Because Its Replacement
            NewLicense.IsActive = true;
            NewLicense.IssueReason = enIssueReason.eReplacementForLost;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if (!NewLicense.Save())
                return null;

            Application.SetComplete();
            this.DeactivateCurrentLicense();
            return NewLicense;


        }


        public bool IsLicenseExpired()
        {
            return this.ExpirationDate < DateTime.Now;
        }

        public bool IsDetained()
        {
            return clsDetainedLicense.IsLicenseDetain(this.LicenseID);
        }

        public clsDetainedLicense Detain(float FineFees,int CreatedByUserID)
        {
            clsDetainedLicense DetainedLicnese=new clsDetainedLicense();
            DetainedLicnese.LicenseID = this.LicenseID;
            DetainedLicnese.DetainDate=DateTime.Now;
            DetainedLicnese.FineFees = FineFees;
            DetainedLicnese.CreatedByUserID=CreatedByUserID;
            DetainedLicnese.IsReleased = false;
            DetainedLicnese.ReleasedByUserID = -1;
            DetainedLicnese.ReleaseApplicationID = -1;
            DetainedLicnese.ReleaseDate = DateTime.Now;

            if (!DetainedLicnese.Save())
                return null;


            this.DeactivateCurrentLicense();
                return DetainedLicnese;

        }

        public bool Release(int ReleasedByUserID,ref int ApplicationID)
        {
            clsApplication Application=new clsApplication();
            
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = clsApplication.enApplicationType.ReleaseDetainedDrivingLicense;
            Application.ApplicationStatus = clsApplication.enStatus.New;
            Application.LastStatusDate=DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)clsApplicationType.enApplicationType.ReleaseDetainedDrivingLicense).ApplicationTypeFee;
            Application.CreatedByUserID = ReleasedByUserID;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;
            }
                ApplicationID=Application.ApplicationID;

            return this.DetainedInfo.ReleaseDetainLicense(ReleasedByUserID, Application.ApplicationID);
            
            
        }

    }
}
