using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Windows.Forms;
using Microsoft.Win32;
namespace DVLD_BLL
{
    public class clsGlobalSettings
    {
        static private clsUser _User;
        static private int _UserID;

       static  public int UserID { set { _UserID = value; }  get { return _UserID; }}
       static public clsUser CurrentUserInfo { set { _User = value; } get { return _User; } }

       static string  KeyPath = "HKEY_LOCAL_MACHINE\\SOFTWARE\\DVLD";


        static public void SaveRememberMe(string UserName,string Paswword)
        {
            try
            {
                string SavePath = System.IO.Directory.GetCurrentDirectory();
                SavePath += "\\LoginData.txt";
                if (UserName == "" && File.Exists(SavePath))
                {
                    File.Delete(SavePath);
                    return;
                }

                string DataToSave = UserName + "#//#" + Paswword;

                using (StreamWriter Writer = new StreamWriter(SavePath))
                {
                    Writer.WriteLine(DataToSave);
                    return;
                }

            }
            catch (Exception ex)
            {
                //Handel Error
                return;
            }
            
        }

        static public bool ReadRememberMe(ref string UserName,ref string Password)
        {
            try
            {
                string SavePath = System.IO.Directory.GetCurrentDirectory();
                SavePath += "\\LoginData.txt";
                if (!File.Exists(SavePath))
                {
                    return false;
                }

                using (StreamReader Reader = new StreamReader(SavePath))
                {
                    string Line = "";
                    while ((Line = Reader.ReadLine()) != null)
                    {
                        Console.WriteLine(Line);
                        string[] Result = Line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                        UserName = Result[0];
                        Password = Result[1];
                        return true;
                    }

                }
            }
            catch { Exception ex; }
            {
                //Handel
                return false;            
            }


        }

        static public void SaveLoginInfoToReg(string Username,string Password)
        {
            try
            {
                Registry.SetValue(KeyPath, "Username",Username) ;
                Registry.SetValue(KeyPath, "Password", Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        }

        static public bool ReadLoginIInfoFromReg(ref string Username,ref string Password)
        {
            try
            {
                Username=Registry.GetValue(KeyPath,"Username",null) as string;
                Password=Registry.GetValue(KeyPath, "Password", null) as string;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }



        }

    }
}
