using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DVLD_DAL
{
    public class clsLicencesClassData
    {
        public static DataTable GetAllLicenseClasses()
        {
            DataTable DataTable = new DataTable();
            SqlConnection Connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM LicenseClasses";
            SqlCommand Command = new SqlCommand(Query, Connection);
            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.HasRows)
                {
                    DataTable.Load(Reader);
                }
                Reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                Connection.Close();
            }
            return DataTable;
        }

        public static bool GetLicenseClassInfoByID(int LicenseClassID,ref string ClassName,ref string ClassDec,ref int MinAllowedAge,ref int DefaultValidityLength,ref float ClassFees)
        {
            bool IsFound = false;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM LicenseClasses WHERE LicenseClassID=@LicenseClassID";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseClassID",LicenseClassID);
            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();
                if(reader.Read())
                {

                    ClassName = (string)reader["ClassName"];
                    ClassDec = (string)reader["ClassDescription"];
                    MinAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    ClassFees = (float)((decimal)reader["ClassFees"]);
                    IsFound = true;
                }
                    reader.Close();
            }
            catch(Exception ex)
            {

            }
            finally { connection.Close(); }
            return IsFound;
        }

        public static bool GetLicenseClassInfoByClassName(string ClassName,ref int LicenseClassID, ref string ClassDec, ref int MinAllowedAge, ref int DefaultValidityLength, ref float ClassFees)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM LicenseClasses WHERE ClassName=@ClassName";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LicenseClassID = (int)reader["LicenseClassID"];
                    ClassDec = (string)reader["ClassDescription"];
                    MinAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    ClassFees = (float)((decimal)reader["ClassFees"]);
                    IsFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }
            return IsFound;
        }
        //AddNew
        //Update

    }
}
