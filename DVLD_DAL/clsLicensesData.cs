using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DVLD_DAL
{
    public class clsLicensesData
    {
        public static int AddLicense(int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate
            , DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"INSERT INTO Licenses
           (ApplicationID
           ,DriverID
           ,LicenseClass
           ,IssueDate
           ,ExpirationDate
           ,Notes
           ,PaidFees
           ,IsActive
           ,IssueReason
           ,CreatedByUserID)
     VALUES
           (@ApplicationID
           ,@DriverID
           ,@LicenseClass
           ,@IssueDate
           ,@ExpirationDate
           ,@Notes
           ,@PaidFees
           ,@IsActive
           ,@IssueReason
           ,@CreatedByUserID)
		   SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);

            if (string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", Notes);

            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                    LicenseID = ID;
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return LicenseID;
        }

        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID, DateTime IssueDate
            , DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            bool IsUpdated = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"UPDATE Licenses
   SET ApplicationID = @ApplicationID
      ,DriverID = @DriverID
      ,LicenseClass = @LicenseClass
      ,IssueDate = @IssueDate
      ,ExpirationDate= @ExpirationDate
      ,Notes= @Notes
      ,PaidFees= @PaidFees
      ,IsActive= @IsActive
      ,IssueReason = @IssueReason
      ,CreatedByUserID = @CreatedByUserID
 WHERE LicenseID=@LicenseID;
";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            if (string.IsNullOrEmpty(Notes))
                command.Parameters.AddWithValue("@Notes", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                int RowsAffected = command.ExecuteNonQuery();
                IsUpdated = (RowsAffected > 0);
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return IsUpdated;


        }

        public static bool GetLicenseInfoByID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate
            , ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM Licenses WHERE LicenseID=@LicenseID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                        Notes = (string)reader["Notes"];

                    PaidFees = (float)((decimal)reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    IsFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return IsFound;
        }

        public static bool GetLicenseInfoByLocalLicenseApplicationID(int LocalDrivingApplicationID, ref int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClassID, ref DateTime IssueDate
           , ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT Licenses.* FROM Licenses Join Applications On Licenses.ApplicationID=Applications.ApplicationID JOIN
LocalDrivingLicenseApplications on Applications.ApplicationID=LocalDrivingLicenseApplications.ApplicationID
WHERE LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LicenseID = (int)reader["LicenseID"];
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClassID = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                        Notes = (string)reader["Notes"];

                    PaidFees = (float)((decimal)reader["PaidFees"]);
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];

                    IsFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return IsFound;
        }


        public static bool DeleteLicense(int LicenseID)
        {
            bool IsDeleted = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = "DELETE FROM Licenses WHERE LicenseID=@LicenseID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                int RowsAffected = command.ExecuteNonQuery();
                IsDeleted = (RowsAffected > 0);
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally { connection.Close(); }
            return IsDeleted;

        }

        public static DataTable GetAllLicenses()
        {
            DataTable DTLicenses = new DataTable();
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM Licenses;";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    DTLicenses.Load(reader);
                }
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            { connection.Close(); }
            return DTLicenses;
        }

        public static DataTable GetAllLicensesForPersonID(int PersonID)
        {
            DataTable DTLicenses = new DataTable();
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT Licenses.LicenseID,Applications.ApplicationID,LicenseClasses.ClassName,
Licenses.IssueDate,Licenses.ExpirationDate,Licenses.IsActive From Licenses Join Applications On Licenses.ApplicationID=Applications.ApplicationID
Join LicenseClasses On Licenses.LicenseClass=LicenseClasses.LicenseClassID
Join Drivers ON Applications.ApplicantPersonID=Drivers.PersonID
WHERE ApplicantPersonID=@PersonID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    DTLicenses.Load(reader);
                }
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            { connection.Close(); }
            return DTLicenses;
        }

        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LinceseID = -1;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT TOP 1 Licenses.LicenseID FROM Licenses JOIN Applications On Licenses.ApplicationID=Applications.ApplicationID 
WHERE ApplicantPersonID=@PersonID AND LicenseClass=@LicenseClass
ORDER BY LicenseID DESC;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                    LinceseID = ID;

            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return LinceseID;
        }

        //GetAllDriverLinceses
        public static bool ActiveDeActivateLicense(int LicenseID,bool IsActive)
        {
            bool IsUpdated = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"UPDATE Licenses 
                            SET IsActive=@IsActive
                            WHERE Licenses.LicenseID=@LicenseID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@IsActive", IsActive);

            try
            {
                connection.Open();
                int RowsAffected = 0;
                RowsAffected = command.ExecuteNonQuery();
                IsUpdated = RowsAffected > 0;
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return IsUpdated;
        }

      

    }
}
