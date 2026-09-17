using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.Security.Cryptography;
namespace DVLD_BLL
{
    public class clsUtil
    {



        public static bool CreateFolderIfNotExists(string FolderPath)
        {
            if (!File.Exists(FolderPath))
            {
                try
                {
                    Directory.CreateDirectory(FolderPath);
                }
                catch(IOException ex)
                {
                    Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
                    return false;
                }
                return true;
            }
            else
                return true;
        }

        private static string GenrateGUID()
        {
            Guid guid = Guid.NewGuid();
            return guid.ToString();

        }

        private static string CreatePathWithGUID(string SourceFile)
        {
            string FileName = SourceFile;
            FileInfo fi=new FileInfo(FileName);
            string Ext=fi.Extension;
            return GenrateGUID() + Ext;

        }

        public static bool  CopyImageToFileLocation(ref string SourceFile)
        {
            string DestinationFolder = @"D:\DVLD-People-Images\";
            if(CreateFolderIfNotExists(DestinationFolder))
            {
                string DestinationPath=DestinationFolder+CreatePathWithGUID(SourceFile);
                try
                {
                    File.Copy(SourceFile, DestinationPath,true);
                }
                catch(IOException ex)
                {
                    Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
                    return false;
                }
                SourceFile=DestinationPath;
                return true;
            }
            else
            {
                return false;
            }


        }

        public static bool DeleteImage(string ImagePath)
        {
            if (!string.IsNullOrEmpty(ImagePath))
            {
                try
                {
                    File.Delete(ImagePath);
                }
                catch (IOException ex)
                {
                    Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
                    return false;
                }
                return true;
            }
            else
                return false;

        }


        /// <summary>
        /// Takes Data In Sting Format And return Data Hashed With SH256
        /// </summary>
        /// <param name="Data">Data You want to Hash</param>
        /// <returns>Hashed Data</returns>
        public static string HashData(string Data)
        {

            using (SHA256 SH=SHA256.Create())
            {
                byte[] HashBytes = SH.ComputeHash(Encoding.UTF8.GetBytes(Data));
                return BitConverter.ToString(HashBytes).Replace("-","").ToLower();
            }

        }

    }
}
