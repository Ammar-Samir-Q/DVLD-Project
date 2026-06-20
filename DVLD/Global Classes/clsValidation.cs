using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace DVLD_BLL
{
    public class clsValidation
    {
        public static bool ValidateEmail(string Email)
        {
            var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var regex = new Regex(pattern);

                return regex.IsMatch(Email);
        }

        public static bool IsNationalNoExists(string NationalNo)
        {
            if (DVLD_BLL.clsPeople.IsPersonExists(NationalNo))
              return true;
            else
            return false;
        }




    }
}
