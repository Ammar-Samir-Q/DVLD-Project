using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVLD_DAL;
namespace DVLD_BLL
{
    public class clsApplicationType
    {

        public enum enApplicationType { NewLocalDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicecnse = 3, ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicense = 5, NewInternationalLicense = 6, RetakeTest = 7 }

        enum enMode {eAdd,eUpdate}
        public int ApplicationTypeID { get; set; }
        public string ApplicationTypeTitle { get; set; }

        public float ApplicationTypeFee { get; set; }

        private enMode _Mode;

        //------------------------

        public clsApplicationType()
        {
            _Mode = enMode.eAdd;
            this.ApplicationTypeID = -1;
            this.ApplicationTypeTitle = "";
            this.ApplicationTypeFee = 0;
        }

        public clsApplicationType(int ApplicationTypeID,string ApplicationTypeTitle,float ApplicationTypeFees)
        {
            _Mode=enMode.eUpdate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationTypeFee = ApplicationTypeFees;
        }


        //------------------------
        public static clsApplicationType Find(int ApplicationTypeID)
        {
            string Title = "";
            float Fee = 0;
            if (clsApplicationTypesData.Find(ApplicationTypeID,ref Title,ref Fee))
            {
                return new clsApplicationType(ApplicationTypeID, Title, Fee);
            }
            else
                return null;
        }

        private bool _Update()
        {
          return clsApplicationTypesData.UpdateTypes(ApplicationTypeID, ApplicationTypeTitle, ApplicationTypeFee);
        }

        private bool _Add()
        {
            this.ApplicationTypeID = DVLD_DAL.clsApplicationTypesData.AddNewApplicationType(this.ApplicationTypeTitle, this.ApplicationTypeFee);
             return (this.ApplicationTypeID>0);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.eAdd:
                    _Mode=enMode.eUpdate;
                    return _Add();
                case enMode.eUpdate:
                    return _Update();
            }
            return false;
        }

        public static DataTable GetAllApplicationTypes()
        {
            return DVLD_DAL.clsApplicationTypesData.GetAllApplicationTypes();
        }


    }
}
