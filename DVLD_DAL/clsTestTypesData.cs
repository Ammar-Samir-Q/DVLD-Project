using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DVLD_DAL
{
    public class clsTestTypesData
    {
        public static bool UpdateTestType(int TypeID, string Title,string Description, float Fees)
        {
            bool IsUpdated = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"UPDATE TestTypes 
                    Set TestTypeTitle=@TestTypeTitle,
                    TestTypeDescription=@TestTypeDescription,
                    TestTypeFees=@TestTypeFees
                    WHERE TestTypeID=@TestTypeID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeTitle", Title);
            command.Parameters.AddWithValue("@TestTypeDescription", Description);
            command.Parameters.AddWithValue("@TestTypeFees", Fees);
            command.Parameters.AddWithValue("@TestTypeID", TypeID);
            try
            {
                connection.Open();
                int RowsAffected = command.ExecuteNonQuery();
                IsUpdated = (RowsAffected > 0);


            }
            catch (Exception ex)
            {

            }
            finally
            { connection.Close(); }
            return IsUpdated;


        }

        public static DataTable GetAllTestTypes()
        {
            DataTable Data = new DataTable();
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM TestTypes ORDER BY TestTypeID;";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    Data.Load(reader);
                }

            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }
            return Data;
        }

        public static bool Find(int TypeID, ref string Title,ref string Description, ref float Fee)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM TestTypes WHERE TestTypeID=@TestTypeID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Title = (string)reader["TestTypeTitle"];
                    Description = (string)reader["TestTypeDescription"];
                    Fee = float.Parse(reader["TestTypeFees"].ToString());
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

        public static int AddNewTestType(string Title,string Description, float Fees)
        {
            int TypeID = -1;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"Insert Into TestTypes (TestTypeTitle,TestTypeDescription,TestTypeFees) values (@TestTypeTitle,@TestTypeDescription,
                             @TestTypeFees);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeTitle", Title);
            command.Parameters.AddWithValue("@TestTypeDescription",Description);
            command.Parameters.AddWithValue("@TestTypeFees", Fees);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                    TypeID = ID;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                connection.Close();
            }

            return TypeID;

        }

    }
}
