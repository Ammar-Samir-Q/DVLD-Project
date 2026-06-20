using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DVLD_BLL
{
    public class clsPeople
    {
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
        
        public string ThirdName { get; set; }
        
        public string LastName { get; set; }

        public string FullName()
        {
            string FullName = FirstName + " " + SecondName;
            if (!string.IsNullOrEmpty(this.ThirdName))
                FullName +=(" "+ThirdName);
            else
            {
                FullName += (" " + LastName);
                return FullName;
            }
            FullName +=(" "+ LastName);
            return FullName;
        }
        public DateTime DateOfBirth { get; set; }
        
        public byte Gendor { get; set; }
        
        public string Address { get; set; }
        
        public string Phone { get; set; }
        
        public string Email { get; set; }
       
        public int NationalityCountryID { get; set; }

        public clsCountry CountryInfo;

        public string ImagePath { get; set; }

        private enum enMode { eAdd,eUpdate}
        private enMode Mode;
        //--------------------------------------------------------------------------------------------------

       public  clsPeople ()
        {
            Mode= enMode.eAdd;
            PersonID = -1;
            NationalNo = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth= DateTime.Now;
            Gendor = 1;
            Address = "";
            Phone = "";
            Email = "";
            NationalityCountryID = -1;
            ImagePath = "";
        }
        public clsPeople(int PersonID, string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
           DateTime DateOfBirth, byte Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            Mode = enMode.eUpdate;
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.CountryInfo=DVLD_BLL.clsCountry.Find(this.NationalityCountryID);
            this.ImagePath = ImagePath;
        }
     
        //------------------------------------------------------------------------------------------------------

    private bool _AddPerson()
        {
            this.PersonID = DVLD_DAL.clsPersonData.AddNewPerson(NationalNo, FirstName, SecondName,
                ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
            return this.PersonID > 0;
        }
    private bool _UpdatePerson()
        {
            return DVLD_DAL.clsPersonData.UpdatePerson(this.PersonID, NationalNo, FirstName, SecondName,ThirdName,LastName, DateOfBirth, Gendor, Address, Phone, Email, NationalityCountryID, ImagePath);
        }

    public bool Save()
        {
            switch(Mode)
            {
                case enMode.eAdd:
                    Mode = enMode.eUpdate;
                    if (_AddPerson())
                    {
                        Mode=enMode.eUpdate;
                        return true;
                    }
                    else
                        return false;
                    
                case enMode.eUpdate:
                    return _UpdatePerson();
                    
                default: return false;

            }

             

        }

    public static clsPeople FindPerson(int PersonID)
        {
            string NationalNo = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "",Address="",Phone="",Email=" ",ImagePath="";
            DateTime DateOfBirth=DateTime.MinValue;
            byte Gendor=0;
            int NatonalityCountryID = -1;

            if(DVLD_DAL.clsPersonData.FindPersonByPersonID(PersonID,ref NationalNo,ref FirstName,ref SecondName,ref ThirdName,ref LastName,ref DateOfBirth,ref Gendor,ref Address,ref Phone,ref Email,ref NatonalityCountryID,ref ImagePath))
            {
                return new clsPeople(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NatonalityCountryID, ImagePath);
            }
            else
            {
                return null;    
            }
        }

        public static clsPeople FindPerson(string NationalNo)
        {
            int PersonID = -1;
                string FirstName = " ", SecondName = " ", ThirdName = " ", LastName = " ", Address = " ", Phone = " ", Email = " ", ImagePath = " ";
            DateTime DateOfBirth = DateTime.MinValue;
            byte Gendor = 0;
            int NatonalityCountryID = -1;

            if (DVLD_DAL.clsPersonData.FindPersonByNationalNo(NationalNo, ref PersonID, ref FirstName, ref SecondName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gendor, ref Address, ref Phone, ref Email, ref NatonalityCountryID, ref ImagePath))
            {
                return new clsPeople(PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth, Gendor, Address, Phone, Email, NatonalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }


        public static bool DeletePerson(int PersonID)
        {
            return DVLD_DAL.clsPersonData.DeletePerson(PersonID);
        }

        public static bool IsPersonExists(string NationaltyNo)
        {
            return DVLD_DAL.clsPersonData.IsPersonExists(NationaltyNo);
        }

        public static bool IsPersonExists(int PersonID)
        {
            return DVLD_DAL.clsPersonData.IsPersonExists(PersonID);
        }

        public static DataTable GetAllPeople()
        {
            return DVLD_DAL.clsPersonData.GetAllPeople();
        }


    }
}
