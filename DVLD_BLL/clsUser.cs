using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace DVLD_BLL
{
    public class clsUser
    {
        enum enMode { eAdd,eUpdate}
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PersonID { get; set; }
        public clsPeople PersonInfo;

        public bool IsActive { get; set; }

        private enMode _Mode=enMode.eAdd;

        //-------------------------------------------

        public clsUser()
        {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive=false;
            this.PersonID = -1;
            _Mode = enMode.eAdd;
        }
        public clsUser(int UserID,int PersonID,string UserName,string Password,bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive=IsActive;
            this.PersonInfo=clsPeople.FindPerson(PersonID);
            _Mode = enMode.eUpdate;
        }
        //------------------------------------------

        private bool _AddUser()
        {
            Password=clsHash.HashData(this.Password);
            int UserID = DVLD_DAL.clsUserData.AddUser(PersonID, UserName, Password, IsActive);
            this.UserID=UserID;
            return (UserID>0);
        }

        private bool _UpdateUser()
        {
            Password = clsHash.HashData(Password);
            return (DVLD_DAL.clsUserData.UpdateUser(UserID,PersonID,UserName,Password,IsActive));
        }

         public bool Save()
        {
            switch(_Mode)
            {
                case enMode.eAdd:
                    _Mode = enMode.eUpdate;
                    return _AddUser();
                case enMode.eUpdate:
                    return _UpdateUser();
            }
            return false;
        }

       static  public clsUser FindUserByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;
            if (DVLD_DAL.clsUserData.FindUser(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
                return null;

        }

        static public clsUser FindUserByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;
            if (DVLD_DAL.clsUserData.FindUserByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive))
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
                return null;

        }

       static  public bool DeleteUser(int UserID)
        {
            return DVLD_DAL.clsUserData.DeleteUser(UserID);
        }
       static public DataTable GetAllUsers()
        {
            return DVLD_DAL.clsUserData.GetAllUsers();
        }

        static public clsUser FindUserByUserNameAndPassword(string UserName,string Password)
        {
            int PersonID = -1, UserID = -1;
            bool IsActive = false;
            if (DVLD_DAL.clsUserData.FindUserByUserNameAndPassword(UserName, clsHash.HashData(Password), ref PersonID, ref UserID, ref IsActive))
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            else
                return null;

        }

        static public bool IsUserExists(string UserName)
        {
            return DVLD_DAL.clsUserData.IsUserExists(UserName);
        }

        static public bool IsPersonHaveUser(int PerosnID)
        {
            return DVLD_DAL.clsUserData.IsUserExistsByPersonID(PerosnID);
        }

    }
}
