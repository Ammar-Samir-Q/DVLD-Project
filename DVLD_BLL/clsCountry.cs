using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DVLD_BLL
{
    public class clsCountry
    {
        public int CountryID { get; set; } 
        public string CountryName { get; set; }
        //---------------------------------------
        public clsCountry()
        {
            this.CountryID = -1;
            this.CountryName = "";
        }
        public clsCountry(int CountryID,string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }


        public static DataTable GetAllCountries()
        {
            return DVLD_DAL.clsCountryData.GetAllCountries();
        }

        public static clsCountry Find(int CountryID)
        {
            string CountryName = "";
            if (DVLD_DAL.clsCountryData.Find(CountryID, ref CountryName))
            {
                return new clsCountry(CountryID, CountryName);
            }
            else
                return null;
        }

        public static clsCountry Find(string CountryName)
        {
            int CountryID = -1;
            if (DVLD_DAL.clsCountryData.Find(CountryName, ref CountryID))
            {
                return new clsCountry(CountryID, CountryName);
            }
            else
                return null;
        }


    }
}
