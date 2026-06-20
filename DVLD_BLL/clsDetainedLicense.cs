using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DAL;
using System.Data;
namespace DVLD_BLL
{
    public class clsDetainedLicense
    {
        enum enMode { eAddNew=1,Update=2}
        private enMode  _Mode=enMode.eAddNew;
        public int DetainID { get; set; }
        public int LicenseID { get; set; }

        public DateTime DetainDate { get; set; }

        public float FineFees { get; set; }

        public int CreatedByUserID { get; set; }

        public bool IsReleased { get; set; }

        public DateTime ReleaseDate { get; set; }

        public int ReleasedByUserID { get; set; }

        public int ReleaseApplicationID { get; set; }

        //------------------------------------
        public clsDetainedLicense()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate=DateTime.MinValue;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.ReleaseDate = DateTime.MinValue;
            this.ReleasedByUserID = -1;
            this.ReleaseApplicationID = -1;
            _Mode = enMode.eAddNew;
        }

        public clsDetainedLicense(int DetainID,int LicenseID,DateTime DetainDate,float FineFees,int CreatedByUserID,bool IsReleased,DateTime ReleaseDate,int ReleasedByUserID,int ReleaseApplicationID)
        {
            this.DetainID=DetainID;
            this.LicenseID=LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees=FineFees;
            this.CreatedByUserID=CreatedByUserID;
            this.IsReleased=IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID=ReleaseApplicationID;
            _Mode = enMode.Update;
        }
        //-----------------------------------

        private bool _AddDetainLicense()
        {
            this.DetainID = clsDetaiedLicensesData.AddDetainedLicense(LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            return this.DetainID > 0;
        }

        private bool _UpdateDetainLicense()
        {
            return clsDetaiedLicensesData.UpdateDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID,IsReleased, ReleaseDate,ReleasedByUserID, ReleaseApplicationID);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.eAddNew:
                    _Mode = enMode.Update;
                    return _AddDetainLicense();
                case enMode.Update:
                    return _UpdateDetainLicense();


            }

            return false;

        }

        public static clsDetainedLicense FindByID(int DetainID)
        {
            int LicenseID = -1, CreatedByUserId = -1, ReleasedByUserId = -1, ReleaseAppID = -1;
            DateTime DetainDate=DateTime.MinValue,ReleaseDate=DateTime.MinValue;
            float FineFees = 0;
            bool IsReleased=false;

            if (clsDetaiedLicensesData.GetDetaiedLicenseInfoByID(DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserId, ref IsReleased, ref ReleaseDate, ref ReleasedByUserId, ref ReleaseAppID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserId, IsReleased, ReleaseDate, ReleasedByUserId, ReleaseAppID); ;
            }
            else
                return null;
        }

        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1, CreatedByUserId = -1, ReleasedByUserId = -1, ReleaseAppID = -1;
            DateTime DetainDate = DateTime.MinValue, ReleaseDate = DateTime.MinValue;
            float FineFees = 0;
            bool IsReleased = false;

            if (clsDetaiedLicensesData.GetDetaiedLicenseInfoByLicenseID(LicenseID, ref DetainID, ref DetainDate, ref FineFees, ref CreatedByUserId, ref IsReleased, ref ReleaseDate, ref ReleasedByUserId, ref ReleaseAppID))
            {
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserId, IsReleased, ReleaseDate, ReleasedByUserId, ReleaseAppID); ;
            }
            else
                return null;
        }

        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetaiedLicensesData.GetAllDetaiedLicenses();
        }
   
        public static bool IsLicenseDetain(int LicenseID)
        {
            return clsDetaiedLicensesData.IsLicenseDetained(LicenseID);
        }

        public bool ReleaseDetainLicense(int ReleasedByUserID,int ReleaseApplicationID)
        {
            return clsDetaiedLicensesData.ReleaseDetainLicense(this.DetainID, ReleasedByUserID, ReleaseApplicationID);  
        }
    }
}
