using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DAL
{
    public class clsTests
    {
        public static int AddNewTest(int TestAppointmentID,byte TestResult,string Notes,int CreatedByUserID)
        {
            int TestID = -1;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"INSERT INTO Tests
           (TestAppointmentID
           ,TestResult
           ,Notes
           ,CreatedByUserID)
     VALUES
           (@TestAppointmentID
           ,@TestResult
           ,@Notes
           ,@CreatedByUserID);
            Update TestAppointments 
            Set IsLocked=1
            WHERE TestAppointments.TestAppointmentID=@TestAppointmentID;
            SELECT SCOPE_IDENTITY();";
            SqlCommand command=new SqlCommand(Query,connection);
            command.Parameters.AddWithValue("@TestAppointmentID",TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult",TestResult);
            if (!string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", Notes);
            else
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null&&int.TryParse(result.ToString(),out int ID))
                    {
                    TestID = ID;
                    }

            }
            catch(Exception ex)
            {

            }
            finally { connection.Close(); }
            return TestID;

        }

        public static bool UpdateTest(int TestID,int TestAppointmentID, byte TestResult, string Notes, int CreatedByUserID)
        {
            bool IsUpdated = false ;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"UPDATE Tests
   SET TestAppointmentID = TestAppointmentID
      ,TestResult = TestResult
      ,Notes = Notes
      ,CreatedByUserID = CreatedByUserID
 WHERE TestID=@TestID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestID",TestID);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            if(!string.IsNullOrEmpty(Notes))
            command.Parameters.AddWithValue("@Notes", Notes);
            else
            command.Parameters.AddWithValue("@Notes",DBNull.Value);
        
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
               int RowsAffected=command.ExecuteNonQuery();
                IsUpdated = RowsAffected > 0;
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }
            return IsUpdated;

        }

        public static bool GetTestInfoByID(int TestID,ref int TestAppointmentID,ref byte TestResult,ref string Notes,ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM Tests WHERE TestID=@TestID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (byte)reader["TestResult"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    if (reader["Notes"]== DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                        Notes = (string)reader["Notes"];

                    IsFound = true;
                }




            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }
            return IsFound;
        }

        public static bool GetTestInfoByTestAppointmentID(int TestAppointmentID , ref int TestID, ref byte TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM Tests WHERE TestAppointmentID=@TestAppointmentID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    TestID = (int)reader["TestID"];
                    TestResult = (byte)reader["TestResult"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                        Notes = (string)reader["Notes"];

                    IsFound = true;
                }




            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }
            return IsFound;
        }

        public static bool DeleteTest(int TestID)
        {
            bool IsDELETED = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"DELETE FROM Tests WHERE TestID=@TestID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);
            try
            {
                connection.Open();
                int RowsAffected = command.ExecuteNonQuery();
                IsDELETED = RowsAffected > 0;
            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }
            return IsDELETED;
        }

        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM Tests;";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }
            return dt;


        }

        public static int CountPassedTests(int LocalDrivingApplicationID)
        {
            int PassedTestCount = 0;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT Count(Tests.TestID) FROM Tests JOIN TestAppointments on Tests.TestAppointmentID=TestAppointments.TestAppointmentID
            WHERE TestResult=1 AND TestAppointments.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID;";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingApplicationID);

            try
            {
                connection.Open();
                object result=command.ExecuteScalar();
                if(result != null&&int.TryParse(result.ToString(),out int PassedCount))
                {
                    PassedTestCount=PassedCount;
                }

            }
            catch(Exception ex)
            { }
            finally { connection.Close(); }
            return PassedTestCount;
        }

        public static int GetTestResultByLDAID(int LocalDrivingApplicationID,int TestTypeID)
        {
            bool Result = false;
            bool IsFaild = false;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT TOP 1 TestResult FROM Tests JOIN TestAppointments on Tests.TestAppointmentID=TestAppointments.TestAppointmentID
            WHERE TestAppointments.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID AND TestAppointments.TestTypeID=@TestTypeID
			ORDER BY TestResult DESC;";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",LocalDrivingApplicationID);
            command.Parameters.AddWithValue("@TestTypeID",TestTypeID);

            try
            {
                connection.Open();
                object result=command.ExecuteScalar();
                if( result != null && bool.TryParse(result.ToString(),out bool _Result))
                { 
                                    Result=_Result;
                             IsFaild=Result==false?true:false;
                }
            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }
            return Result==true?1:IsFaild==true?0:-1;
        }


    }
}
