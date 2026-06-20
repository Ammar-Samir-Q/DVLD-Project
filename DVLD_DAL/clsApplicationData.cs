using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_DAL
{
    public class clsApplicationData
    {
        public static int AddNewApplication(int PersonID,DateTime ApplicationDate,
            int ApplicationTypeID,byte ApplicationStatus,DateTime LastStatusDate,float PaidFees,int CreatedByUserID)
        {
            int ApplicationID = -1;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"INSERT INTO Applications
                               (ApplicantPersonID
                               ,ApplicationDate
                               ,ApplicationTypeID
                               ,ApplicationStatus
                               ,LastStatusDate
                               ,PaidFees
                               ,CreatedByUserID)
                         VALUES
                               (@ApplicantPersonID,
                               @ApplicationDate,
                               @ApplicationTypeID,
                               @ApplicationStatus,
                               @LastStatusDate,
                               @PaidFees,
                               @CreatedByUserID);
           SELECT SCOPE_IDENTITY();";
            SqlCommand command=new SqlCommand(Query,connection);
            command.Parameters.AddWithValue("@ApplicantPersonID",PersonID);
            command.Parameters.AddWithValue("@ApplicationDate",ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID",ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus",ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate",LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees",PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                    ApplicationID = ID;


            }
            catch(Exception ex)
            {

            }
            finally { connection.Close(); } 
            return ApplicationID;   
        }

        public static bool UpdateApplication(int ApplicationID,int PersonID, DateTime ApplicationDate,
           int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            bool IsUpdated =false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"
                    UPDATE Applications
                   SET ApplicantPersonID = @ApplicantPersonID
                      ,ApplicationDate= @ApplicationDate
                      ,ApplicationTypeID = @ApplicationTypeID
                      ,ApplicationStatus = @ApplicationStatus
                      ,LastStatusDate = @LastStatusDate
                      ,PaidFees = @PaidFees
                      ,CreatedByUserID = @CreatedByUserID
                 WHERE ApplicationID=@ApplicationID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                int RowsAffected = command.ExecuteNonQuery();
                IsUpdated=RowsAffected > 0;


            }
            catch (Exception ex)
            {

            }
            finally { connection.Close(); }
            return IsUpdated;
        }

        public static bool GetApplicationInfoByID(int ApplicationID,ref int PersonID, ref DateTime ApplicationDate,
           ref int ApplicationTypeID,ref byte ApplicationStatus,ref DateTime LastStatusDate,ref float PaidFees,ref int CreatedByUserID)
        {
            bool IsFound = false;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM Applications WHERE ApplicationID=@ApplicationID;";
            SqlCommand command =new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID",ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();  
                if(reader.Read())
                {
                    PersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToInt32(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
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

        public static bool DeleteApplication(int ApplicationID)
        {
            bool IsDeleted = false;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"DELETE FROM Applications WHERE ApplicationID=@ApplicationID;";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                int rowsAffected=command.ExecuteNonQuery();
                IsDeleted = rowsAffected > 0;

            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }
            return IsDeleted;

        }


        public static DataTable GetAllApplications()
        {
            DataTable Data = new DataTable();
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM Applications;";
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
        
        public static bool UpdateApplicationStatus(int ApplicationID,int Status)
        {
            bool IsUpdated = false;
            SqlConnection connection =new SqlConnection(DataHelper.ConnectionString);
            string Query = @"UPDATE Applications
                SET Applications.ApplicationStatus=@ApplicationStatus,
                     Applications.LastStatusDate=@LastStatusDate
                WHERE Applications.ApplicationID=@ApplicationID;      ";
            SqlCommand command =new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID",ApplicationID);
            command.Parameters.AddWithValue("@ApplicationStatus",Status);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            try
            {
                connection.Open();
                int rowsAffected= command.ExecuteNonQuery();
                IsUpdated = rowsAffected > 0;
            }
            catch(Exception ex)
            {

            }
            finally { connection.Close(); }
            return IsUpdated;

        }

        static int GetActiveApplicationID(int PersonID,int ApplicationType)
        {
            int AppID = -1;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = "SELECT Top 1 Applications.ApplicationID FROM Applications WHERE ApplicantPersonID = @ApplicantPersonID AND ApplicationTypeID = @ApplicationTypeID ";
            SqlCommand command =new SqlCommand(Query,connection);
            command.Parameters.AddWithValue("@ApplicantPersonID",PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID",ApplicationType);

            try
            {
                connection.Open();
                object result=command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                    AppID = ID;
            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }
            return AppID;
        }
        static int GetActiveApplicationLicenseClass(int PersonID, int ApplicationType, int LicenseClassID)
        {
            int AppID = -1;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = "SELECT Top 1 Applications.ApplicationID FROM Applications Join LocalDrivingLicenseApplications On Applications.ApplicationID=LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationIDWHERE ApplicantPersonID = @ApplicantPersonID AND ApplicationTypeID = @ApplicationTypeID AND LicenseClassID = @LicenseClassID; ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationType);
            command.Parameters.AddWithValue("@LicenseClassID",LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                    AppID = ID;
            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }
            return AppID;
        }
        
        
        static bool DosePersonHaveActiveApplication(int PersonID)
        {
            bool IsActive=false;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT top 1 1 FROM Applications WHERE ApplicantPersonID=@PersonID;";
            SqlCommand command = new SqlCommand(Query,connection);
            command.Parameters.AddWithValue("@PersonID",PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();   
                IsActive=reader.Read(); 
                reader.Close();


            }
            catch(Exception ex)
            { }
            finally
            { connection.Close(); }
            return IsActive;
        }


    }
}
