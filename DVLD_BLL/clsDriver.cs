using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DVLD_BLL
{
    public class clsDriver
    {
        enum enMode { eAdd=1,eUpdate=2}
        
        public int DriverID { get; set; }

        public int PersonID { get; set; }

        public clsPeople PersonInfo { get; set; }

        public int CreatedByUserID { get; set; }

        public clsUser UserInfo { get; set; }

        public DateTime CreatedDate { get; set; }

        private enMode _Mode;
        
        //--------------
        public clsDriver ()
        {
            DriverID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.MinValue;
            _Mode = enMode.eAdd;
        }

        public clsDriver(int DriverID,int PersonID,int CreatedByUserID,DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.PersonInfo=clsPeople.FindPerson(PersonID);
            this.CreatedByUserID = CreatedByUserID;
            this.UserInfo=clsUser.FindUserByUserID(CreatedByUserID);
            this.CreatedDate = CreatedDate;
            _Mode=enMode.eUpdate;
        }
        //--------------

        private bool _AddDriver()
        {
            this.DriverID=DVLD_DAL.clsDriversData.AddDriver(PersonID,CreatedByUserID,CreatedDate);
            return this.DriverID>0;
        }

        private bool _UpdateDriver()
        {
            return DVLD_DAL.clsDriversData.UpdateDriver(DriverID,PersonID, CreatedByUserID, CreatedDate);
        }

        public bool Save()
        {
            switch(_Mode)
            {
                case enMode.eAdd:
                    _Mode = enMode.eUpdate;
                    return _AddDriver();
                case enMode.eUpdate:
                    return _UpdateDriver();
            }
            return false;
        }

        public static clsDriver FindDriver(int DriverID)
        {
            int PersonID = -1, CreatedByUser = -1;
            DateTime CreatedDate=DateTime.MinValue;
            if (DVLD_DAL.clsDriversData.GetDriverByDriverID(DriverID, ref PersonID, ref CreatedByUser, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUser, CreatedDate);
            }
            else
                return null;


        }

        public static clsDriver FindDriverByPersonID(int PersonID)
        {
            int DriverID = -1, CreatedByUser = -1;
            DateTime CreatedDate = DateTime.MinValue;
            if (DVLD_DAL.clsDriversData.GetDriverByPersonID(PersonID, ref DriverID, ref CreatedByUser, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUser, CreatedDate);
            }
            else
                return null;


        }

        public static DataTable GetAllDrivers()
        {
            return DVLD_DAL.clsDriversData.GetAllDrivers();
        }

        public static bool IsThisPersonDriver(int PersonID)
        {
            return DVLD_DAL.clsDriversData.IsThisPersonDriver(PersonID);
        }

    }
}
