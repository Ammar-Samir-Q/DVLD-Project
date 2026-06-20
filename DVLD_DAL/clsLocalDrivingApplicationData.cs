using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_DAL
{
    //LDLA == Local Driving License Application
    public class clsLocalDrivingApplicationData
    {
        public static int AddNewLocalDrivingApplication(int ApplicationID, int LicenseClassID)
        {
            int LocalDrivingApplicationID = -1;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"INSERT INTO LocalDrivingLicenseApplications
           (ApplicationID
           ,LicenseClassID)
                    VALUES
           (@ApplicationID,@LicenseClassID);
                           SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                    LocalDrivingApplicationID = ID;
            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }
            return LocalDrivingApplicationID;
        }

        public static bool UpdateLocalDrivingApplication(int LocalDrivinglicenseAppID, int ApplicationID, int LicenseClassID)
        {
            bool IsUpdated = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"
                    UPDATE LocalDrivingLicenseApplications
                       SET ApplicationID = @ApplicationID,
                          LicenseClassID = @LicenseClassID
                     WHERE LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID;";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivinglicenseAppID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                int RowsAffected = command.ExecuteNonQuery();
                IsUpdated = RowsAffected > 0;
            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }
            return IsUpdated;
        }

        public static bool DeleteLocalDrivingApplication(int LocalDrivinglicenseAppID)
        {
            bool IsDELETED = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"DELETE FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID;";
            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivinglicenseAppID);

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

        public static DataTable GetAllLocalDrivingLicensesApplications()
        {
            DataTable DT = new DataTable();
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM LocalDrivingLicenseApplications_View
                                 Order By ApplicationDate DESC;";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    DT.Load(reader);
                }
                reader.Close();
            }
            catch
            {

            }
            finally { connection.Close(); }
            return DT;

        }

        public static bool GetLocalDrivingLicenseApplicationInfoByID(int LocalDrivingApplicationID, ref int ApplicationID, ref int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
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

        public static bool GetLocalDrivingLicenseApplicationInfoByApplicationID(int ApplicationID,ref int LocalDrivingApplicationID, ref int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationID=@ApplicationID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    LocalDrivingApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
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

        public static bool GetLocalDrivingLicenseApplicationInfoByLicenseID(int LicenseID, ref int LocalDrivingApplicationID,ref int ApplicationID, ref int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT LocalDrivingLicenseApplications.* FROM LocalDrivingLicenseApplications Join Licenses On LocalDrivingLicenseApplications.ApplicationID=Licenses.ApplicationID
                    WHERE LicenseID=@LicenseID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LocalDrivingApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
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

        public static bool IsPersonHaveActiveLocalApplication(int PersonID,int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT 1 FROM LocalDrivingLicenseApplications 
join Applications on LocalDrivingLicenseApplications.ApplicationID=Applications.ApplicationID
join LicenseClasses on LocalDrivingLicenseApplications.LicenseClassID=LicenseClasses.LicenseClassID
WHERE Applications.ApplicantPersonID=@PersonID AND LicenseClasses.LicenseClassID=@LicenseClassID And Applications.ApplicationStatus=1;";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID",PersonID);
            command.Parameters.AddWithValue("@LicenseClassID",LicenseClassID);

            try 
            {
                connection.Open();
            SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
                reader.Close ();
            }
            catch(Exception ex)
            {


            }
            finally { connection.Close(); }
            return IsFound;
        }

        public static bool IsPersonHaveLocalLicense(int PersonID, int LicenseClassID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT 1 FROM Licenses JOIN Applications ON Licenses.ApplicationID=Applications.ApplicationID
WHERE ApplicantPersonID=@ApplicantPersonID AND Licenses.LicenseClass=@LicenseClass;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClassID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsFound = reader.HasRows;
                reader.Close();
            }
            catch(Exception ex)
            {


            }
            finally { connection.Close(); }
            return IsFound;
        }

        public static int TotalTrailsPerTestType(int LocalDrivingApplicationID, int TestTypeID)
        {
            int TrailCount = 0;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT Count(Tests.TestID) FROM Tests JOIN TestAppointments on Tests.TestAppointmentID=TestAppointments.TestAppointmentID
            WHERE TestResult=0 AND TestAppointments.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID AND TestAppointments.TestTypeID=@TestTypeID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                {
                    TrailCount = ID;
                }

            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }
            return TrailCount;
        }

       public static bool DosePassTestType(int LocalDrivingApplicationID,int TestTypeID)
        {
            bool DosePass = false;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT TOP 1 1 FROM Tests Join TestAppointments On Tests.TestAppointmentID=TestAppointments.TestAppointmentID
        WHERE TestResult=1 AND TestAppointments.TestTypeID=@TestTypeID AND TestAppointments.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID;";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader= command.ExecuteReader();
                DosePass = reader.HasRows;
                reader.Close();
            }
            catch(Exception ex)
            { }
            finally { connection.Close(); }
            return DosePass;
        }
        public static bool DoseAttendTestType(int LocalDrivingApplicationID,int TestTypeID)
        {
            bool IsAttendTheTest=false;
            SqlConnection connection =new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT TOP 1 1 FROM Tests JOIN TestAppointments ON Tests.TestAppointmentID=TestAppointments.TestAppointmentID 
WHERE TestAppointments.LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID AND TestAppointments.TestTypeID=@TestTypeID; ";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsAttendTheTest = reader.HasRows;
                reader.Close();
            }
            catch(Exception ex)
            {

            }
            finally { connection.Close(); }
            return IsAttendTheTest;

        }
        //bool IsThereAnActiveScheduledTest(LocalDrivingApplicationID)


    }



}
