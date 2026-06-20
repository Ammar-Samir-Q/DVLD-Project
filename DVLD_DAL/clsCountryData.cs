using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DVLD_DAL
{
    public class clsCountryData
    {
        public static DataTable GetAllCountries()
        {
            DataTable DataTable = new DataTable();
            SqlConnection Connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM Countries";
            SqlCommand Command = new SqlCommand(Query, Connection);
            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.Read())
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

        public static bool Find(int CountryID,ref string CountryName)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * From Countries WHERE CountryID=@CountryID";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();
               if(reader.Read())
                {
                    CountryName = (string)reader["CountryName"];
                    IsFound=true;
                }
                reader.Close();
            }
            catch
            {

            }
            finally
            { connection.Close(); }
            return IsFound;

        }

        public static bool Find(string  CountryName, ref int CountryID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * From Countries WHERE CountryName=@CountryName";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                   CountryID= (int)reader["CountryID"];
                    IsFound = true;
                }
                reader.Close();
            }
            catch
            {

            }
            finally
            { connection.Close(); }
            return IsFound;

        }



    }
}
