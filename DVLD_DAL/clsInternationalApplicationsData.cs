using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DVLD_DAL
{
    public class clsInternationalApplicationsData
    {
        public static int AddNewInternationalLicense(int ApplicationID,int DriverID,int IssuedUsingLocalLicenseID,DateTime IssueDate,DateTime ExpiratioDate,bool IsActive,int CreatedByUserID)
        {
            int InternationalLicecnseID = -1;
            SqlConnection connection =new SqlConnection(DataHelper.ConnectionString);
            string Query = @"
    UPDATE InternationalLicenses 
Set IsActive=0
WHERE DriverID=@DriverID;


INSERT INTO InternationalLicenses
           (ApplicationID
           ,DriverID
           ,IssuedUsingLocalLicenseID
           ,IssueDate
           ,ExpirationDate
           ,IsActive
           ,CreatedByUserID)
     VALUES
           (@ApplicationID,
           @DriverID,
           @IssuedUsingLocalLicenseID,
           @IssueDate,
           @ExpirationDate,
           @IsActive,
           @CreatedByUserID)
		   SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID",ApplicationID);
            command.Parameters.AddWithValue("@DriverID",DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID",IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate",IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate",ExpiratioDate);
            command.Parameters.AddWithValue("@IsActive",IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if(result != null&&int.TryParse(result.ToString(),out int ID))
                {
                    InternationalLicecnseID = ID;
                }

            }
            catch(Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return InternationalLicecnseID;
        }

        public static bool UpdateInternationalLicense(int InternationalLinceseID,int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpiratioDate, bool IsActive, int CreatedByUserID)
        {
            bool IsUpdated=false;   
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"
UPDATE InternationalLicenses
   SET [ApplicationID] = @ApplicationID
      ,[DriverID] = @DriverID
      ,[IssuedUsingLocalLicenseID] = @IssuedUsingLocalLicenseID
      ,[IssueDate] = @IssueDate
      ,[ExpirationDate] =@ExpirationDate
      ,[IsActive] =@IsActive
      ,[CreatedByUserID] = @CreatedByUserID
 WHERE InternationalLicenseID=@InternationalLicenseID;


";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLinceseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpiratioDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                int AffectedRows=command.ExecuteNonQuery();
                IsUpdated = AffectedRows > 0;
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
             return IsUpdated ;
        }
    
        public static bool GetInternationalLicenseByID(int InternationalLinceseID,ref int ApplicationID,ref int DriverID,ref int IssuedUsingLocalLicenseID,ref DateTime IssueDate,ref DateTime ExpiratioDate,ref bool IsActive,ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM InternationalLicenses WHERE InternationalLicenseID=@InternationalLicenseID;";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLinceseID);
            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();
                if(reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpiratioDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsFound = true;
                }
                reader.Close();
            }
            catch(Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return IsFound;


        }

        public static bool GetInternationalLicenseByLocalLicenseID(int LicenseID,ref int InternationalLinceseID, ref int ApplicationID, ref int DriverID, ref DateTime IssueDate, ref DateTime ExpiratioDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * From InternationalLicenses WHERE InternationalLicenses.IssuedUsingLocalLicenseID=@LicenseID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    InternationalLinceseID = (int)reader["InternationalLicenseID"];
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpiratioDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
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


        public static bool DeleteInternationalLicnese(int InternationalLinceseID)
        {
            bool IsDeleted = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"DELETE from InternationalLicenses WHERE InternationalLicenseID=@InternationalLicenseID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLinceseID);
          

            try
            {
                connection.Open();
                int AffectedRows = command.ExecuteNonQuery();
                IsDeleted = AffectedRows > 0;
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return IsDeleted;
        }

        public static DataTable GetAllInternationalLicenses()
        {
            DataTable DTInternationalLicenses = new DataTable();
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM InternationalLicenses;";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    DTInternationalLicenses.Load(reader);
                }
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            { connection.Close(); }
            return DTInternationalLicenses;
        }

        public static DataTable GetAllInternationalLicensesForPersonID(int PersonID)
        {
            DataTable DTLicenses = new DataTable();
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT InternationalLicenseID,InternationalLicenses.ApplicationID,IssuedUsingLocalLicenseID,IssueDate,ExpirationDate,IsActive FROM InternationalLicenses
Join Applications On InternationalLicenses.ApplicationID=Applications.ApplicationID WHERE ApplicantPersonID=@PersonID";
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

        public static bool IsThisPersonHaveInternationalLicecnse(int PersonID)
        {
            bool IsFound = false;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT InternationalLicenses.InternationalLicenseID FROM InternationalLicenses Join Applications on InternationalLicenses.ApplicationID=Applications.ApplicationID
                            WHERE ApplicantPersonID=@PersonID;";
            SqlCommand command = new SqlCommand(Query,connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();
                IsFound=reader.HasRows;
                reader.Close(); 
            }
            catch(Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally { connection.Close(); }
            return IsFound;


        }

        public static int GetActiveInternationalLicenseByDriverID(int DriverID)
        {
            int InternationalID = -1;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT TOP 1 InternationalLicenses.InternationalLicenseID FROM InternationalLicenses 
                WHERE InternationalLicenses.DriverID=@DriverID AND IsActive=1 AND GETDATE() Between IssueDate AND ExpirationDate
                ORDER BY InternationalLicenseID DESC;";
            SqlCommand command=new SqlCommand(Query,connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(),out int ID))
                    InternationalID = ID;


            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return InternationalID;

        }

    }
}
