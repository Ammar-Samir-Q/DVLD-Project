using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DVLD_BLL
{
    public class clsLicenseClasse
    {
       
        public int LicenseClassID { get; set; }

        public string ClassName { get; set; }

        public string ClassDescription { get; set; }

        public int MinimumAllowedAge { get; set; }

        public int DefaultValidityLength { get; set; }

        public float ClassFees { get; set; }

        
    //---------------------
    public clsLicenseClasse(int LicenseClassID,string ClassName,string ClassDecription,int MinAllowedAge,int DefaultValidityLength,float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription=ClassDecription;
            this.MinimumAllowedAge = MinAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
        }

    //------------------

     public static clsLicenseClasse Find(int LicenseClassID)
        {
            string ClassName = "", ClassDec = "";
            int MinimumAllowedAge = 0,DefaultValidityLength=0;
            float ClassFees = 0;
            if (DVLD_DAL.clsLicencesClassData.GetLicenseClassInfoByID(LicenseClassID, ref ClassName, ref ClassDec, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClasse(LicenseClassID, ClassName, ClassDec, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
                return null;

        }

     public static clsLicenseClasse Find(string ClassName)
        {
            int LicenseClassID = -1;
                string ClassDec = "";
            int MinimumAllowedAge = 0, DefaultValidityLength = 0;
            float ClassFees = 0;
            if (DVLD_DAL.clsLicencesClassData.GetLicenseClassInfoByClassName(ClassName,ref LicenseClassID, ref ClassDec, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClasse(LicenseClassID, ClassName, ClassDec, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
                return null;

        }


        public static DataTable GetAllLicenseClasses()
        {
            return DVLD_DAL.clsLicencesClassData.GetAllLicenseClasses();
        }

        //AddNew
        //Update

    }
}
