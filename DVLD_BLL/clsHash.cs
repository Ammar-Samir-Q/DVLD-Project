using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_BLL
{
    public class clsHash
    {
        /// <summary>
        /// Takes Data In Sting Format And return Data Hashed With SH256
        /// </summary>
        /// <param name="Data">Data You want to Hash</param>
        /// <returns>Hashed Data</returns>
        public static string HashData(string Data)
        {

            using (SHA256 SH = SHA256.Create())
            {
                byte[] HashBytes = SH.ComputeHash(Encoding.UTF8.GetBytes(Data));
                return BitConverter.ToString(HashBytes).Replace("-", "").ToLower();
            }

        }

    }
}
