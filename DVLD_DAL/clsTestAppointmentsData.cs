using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DVLD_DAL
{
    public class clsTestAppointmentsData
    {
        public static int AddNewTestAppointement(int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate,float PaidFees,int CreatedByUserID,bool IsLocked,int RetakeTestID)
        {
            int TestAppointmentID = -1;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"
                        INSERT INTO TestAppointments
                                   (TestTypeID
                                   ,LocalDrivingLicenseApplicationID
                                   ,AppointmentDate
                                   ,PaidFees
                                   ,CreatedByUserID
                                   ,IsLocked
                                   ,RetakeTestApplicationID)
                             VALUES
                                   (@TestTypeID
                                   ,@LocalDrivingLicenseApplicationID
                                   ,@AppointmentDate
                                   ,@PaidFees
                                   ,@CreatedByUserID
                                   ,@IsLocked
                                   ,@RetakeTestApplicationID);
                                    SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeID",TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate",AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees",PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked",IsLocked);
            if(RetakeTestID > 0)
            command.Parameters.AddWithValue("@RetakeTestApplicationID",RetakeTestID);
            else
            command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);




            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                    TestAppointmentID = ID;
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return TestAppointmentID;
        }

        public static bool UpdateTestAppointement(int TestAppointementID,int TestTypeID, int LocalDrivingLicenseApplicationID,
           DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestID)
        {
            bool IsUpdated = false ;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"UPDATE TestAppointments
                   SET TestTypeID = @TestTypeID
                      ,LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                      ,AppointmentDate = @AppointmentDate
                      ,PaidFees = @PaidFees
                      ,CreatedByUserID = @CreatedByUserID
                      ,IsLocked = @IsLocked
                      ,RetakeTestApplicationID = @RetakeTestApplicationID
                 WHERE TestAppointmentID=@TestAppointmentID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID",TestAppointementID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            if (RetakeTestID > 0)
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestID);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);




            try
            {
                connection.Open();
                int AffectedRows=command.ExecuteNonQuery();
                IsUpdated=AffectedRows > 0;
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return IsUpdated;
        }


        public static bool DeleteLocalDrivingApplication(int TestAppointmentID)
        {
            bool IsDELETED = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"DELETE FROM TestAppointments WHERE TestAppointmentID=@TestAppointmentID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                int RowsAffected = command.ExecuteNonQuery();
                IsDELETED = RowsAffected > 0;
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return IsDELETED;
        }

        public static bool GetTestAppointmentInfoByID(int TestAppointementID,ref int TestTypeID,ref int LocalDrivingLicenseApplicationID,
           ref DateTime AppointmentDate, ref float PaidFees,ref int CreatedByUserID,ref bool IsLocked, ref int RetakeTestID)
        {
            bool IsFound=false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM TestAppointments WHERE TestAppointmentID=@TestAppointmentID;";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointementID);
            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();  
                if(reader.Read())
                {
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (float)((decimal)reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];
                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                    {
                        RetakeTestID = -1;
                    }
                    else
                        RetakeTestID = (int)reader["RetakeTestApplicationID"];

                    IsFound = true;
                }




            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally { connection.Close(); }
            return IsFound;
        }

        public static bool GetTestAppointmentInfoByLocalDrivingAppID(int LocalDrivingLicenseApplicationID,ref int TestAppointementID, ref int TestTypeID,
         ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM TestAppointments WHERE LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    TestAppointementID = (int)reader["TestAppointmentID"];
                    TestTypeID = (int)reader["TestTypeID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (float)((decimal)reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];
                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                    {
                        RetakeTestID = -1;
                    }
                    else
                        RetakeTestID = (int)reader["RetakeTestApplicationID"];

                    IsFound = true;
                }




            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally { connection.Close(); }
            return IsFound;
        }

        public static bool GetLastTestAppointmentInfoByLocalDrivingAppID(int LocalDrivingLicenseApplicationID, ref int TestAppointementID, ref int TestTypeID,
         ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT TOP 1 FROM TestAppointments WHERE LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID
                                Order By TestAppointmentId DESC;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    TestAppointementID = (int)reader["TestAppointmentID"];
                    TestTypeID = (int)reader["TestTypeID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (float)((decimal)reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];
                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                    {
                        RetakeTestID = -1;
                    }
                    else
                        RetakeTestID = (int)reader["RetakeTestApplicationID"];

                    IsFound = true;
                }




            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally { connection.Close(); }
            return IsFound;
        }


        public static bool IsLastTestAppointmentLocked(int LocalDrivingLicenseAppID,int TestTypeID)
        {
            bool IsLocked = false;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT TOP 1 IsLocked FROM TestAppointments WHERE LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID AND TestTypeID=@TestTypeID
ORDER BY TestAppointmentID DESC ";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID",LocalDrivingLicenseAppID);
            command.Parameters.AddWithValue("@TestTypeID",TestTypeID);
            try
            {
                connection.Open();
                object result=command.ExecuteScalar();
                if (result != null && bool.TryParse(result.ToString(), out bool _IsLocked))
                    IsLocked = _IsLocked;


            }
            catch(Exception ex)
            {

                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally { connection.Close(); }
            return IsLocked;
        }


        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM TestAppointments_View Order by ;";
            SqlCommand command=new SqlCommand(Query, connection);   
            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();
                if(reader.Read())
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return dt;


        }

        public static DataTable GetAllTestAppointmentsForLocalLicenseID(int LocalDrivingLicenseApplicationID,int TestTypeID)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT TestAppointments.TestAppointmentID,TestAppointments.AppointmentDate,TestAppointments.PaidFees,TestAppointments.IsLocked
                FROM TestAppointments WHERE TestAppointments.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID AND TestTypeID=@TestTypeID;;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dt.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return dt;


        }

        public static bool DoseHaveActiveAppointment(int LocalDrivingApplicationID, int TestTypeID)
        {
            bool IsActive = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT TOP 1 * FROM TestAppointments WHERE 
LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID AND IsLocked=0 AND TestTypeID=@TestTypeID
Order By TestAppointmentID DESC;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsActive = reader.HasRows;
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally { connection.Close(); }
            return IsActive;
        }


    }
}
