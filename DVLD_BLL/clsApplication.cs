using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DAL;

namespace DVLD_BLL
{
    public class clsApplication
    {
        public enum enApplicationType { NewLocalDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicecnse = 3, ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicense = 5, NewInternationalLicense = 6, RetakeTest = 7 }
        public enum enStatus { New = 1, Cancelled = 2, Completed = 3 }
        protected enum enMode { eAdd = 1, eUpdate = 2 }

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }

        //ReDo every Compostion In The Project like This Method
        public clsPeople PersonInfo { get; set; }
      
        public DateTime ApplicationDate { get; set; }

        public enApplicationType ApplicationTypeID { get; set; }

        public clsApplicationType ApplicationTypeInfo { get; set; }
        

        public enStatus ApplicationStatus { get; set; }

        public DateTime LastStatusDate { get; set; }

        public float PaidFees { get; set; }

        public int CreatedByUserID { get; set; }

        public clsUser UserInfo;
       

        protected enMode _Mode;

        //--------------------------------------

        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicationTypeID =(enApplicationType.NewLocalDrivingLicense);
            this.ApplicantPersonID = -1;
            this.CreatedByUserID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationStatus = enStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this._Mode=enMode.eAdd;
        }

        public clsApplication(int ApplicationID,int ApplicantPersonID,
            DateTime ApplicationDate,int ApplicationTypeID,
            enStatus ApplicationStatus,DateTime LastStatusDate,
            float PaidFees,int CreatedByUserID)
        {
            this.ApplicationID=ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            PersonInfo = clsPeople.FindPerson(ApplicantPersonID);
            this.ApplicationDate=ApplicationDate;
            this.ApplicationTypeID=(enApplicationType)ApplicationTypeID;
            this.ApplicationTypeInfo = clsApplicationType.Find(ApplicationTypeID);
            this.ApplicationStatus=ApplicationStatus;
            this.LastStatusDate=LastStatusDate;
            this.PaidFees=PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo=clsUser.FindUserByUserID(CreatedByUserID);
            this._Mode = enMode.eUpdate;
        }

        //-------------------------------------------------

        private bool _AddNewApplication()
        {
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, (int)this.ApplicationTypeID
                , (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

            return this.ApplicationID > 0;
        }

        private bool _UpdateApplication()
        {
          return  clsApplicationData.UpdateApplication(this.ApplicationID,this.ApplicantPersonID, this.ApplicationDate, (int)this.ApplicationTypeID
                , (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch(this._Mode)
            {
                case enMode.eAdd:
                    this._Mode=enMode.eUpdate;
                    return _AddNewApplication();
                case enMode.eUpdate:
                    return _UpdateApplication();    
            }
            return false;
        }

        public static bool DeleteAplication(int ApplicationID)
        {
            return clsApplicationData.DeleteApplication(ApplicationID);
        }

        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int AppPersonID = -1, AppTypeID = -1, UserID = -1;
            DateTime AppDate= DateTime.MinValue,LastStatusDate=DateTime.MinValue;
            byte AppStatus = 1;
            float PaidFees = 0;
            if (clsApplicationData.GetApplicationInfoByID(ApplicationID, ref AppPersonID, ref AppDate, ref AppTypeID, ref AppStatus, ref LastStatusDate, ref PaidFees, ref UserID))
            {
                return new clsApplication(ApplicationID, AppPersonID, AppDate, AppTypeID, (enStatus)AppStatus, LastStatusDate, PaidFees, UserID); ;
            }
            else
                return null;

        }

        public bool Cancel()
        {
           return UpdateApplicationStatus(this.ApplicationID, 2);
        }

        public bool SetComplete()
        {
            return UpdateApplicationStatus(this.ApplicationID, 3);
        }

        public static bool UpdateApplicationStatus(int ApplicationID,int StatusID)
        {
            return clsApplicationData.UpdateApplicationStatus(ApplicationID,StatusID);
        }


    }
}
