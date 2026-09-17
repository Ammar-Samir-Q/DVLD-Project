using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_DAL
{
    public class clsDetaiedLicensesData
    {
        public static int AddDetainedLicense(int LicenseID,DateTime DetainDate,float FineFees
            ,int CreatedByUserID,bool IsRelesed,DateTime ReleseDate,int ReleasedByUserID,int ReleaseApplicationID)
        {
            int DetainID = -1;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"Insert Into DetainedLicenses
(LicenseID,DetainDate,FineFees,CreatedByUserID,IsReleased,ReleaseDate,ReleasedByUserID,ReleaseApplicationID)
VALUES
(@LicenseID,@DetainDate,@FineFees,@CreatedByUserID,@IsReleased,@ReleaseDate,@ReleasedByUserID,@ReleaseApplicationID);
SELECT SCOPE_IDENTITY();";
            SqlCommand command=new SqlCommand(Query,connection);
            command.Parameters.AddWithValue("@LicenseID",LicenseID);
            command.Parameters.AddWithValue("@DetainDate",DetainDate);
            command.Parameters.AddWithValue("@FineFees",FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID",CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased",IsRelesed);

            if (ReleseDate != null)
            command.Parameters.AddWithValue("@ReleaseDate",ReleseDate);
            else
                command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);

            if(ReleasedByUserID > 0)
            command.Parameters.AddWithValue("@ReleasedByUserID",ReleasedByUserID);
            else
                command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);


            if(ReleaseApplicationID > 0)    
            command.Parameters.AddWithValue("@ReleaseApplicationID",ReleaseApplicationID);
            else
                command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);


            try
            {
            connection.Open();
            object obj = command.ExecuteScalar();
                if (obj != null && int.TryParse(obj.ToString(), out int ID))
                    DetainID = ID;
            
            
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return DetainID;

        }

        public static bool UpdateDetainedLicense(int DetainID,int LicenseID,
            DateTime DetainDate, float FineFees, int CreatedByUserID, bool IsRelesed, DateTime ReleseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            bool IsUpdated = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"UPDATE DetainedLicenses 
SET LicenseID=@LicenseID,
DetainDate=@DetainDate,
FineFees=@FineFees,
CreatedByUserID=@CreatedByUserID,
IsReleased=@IsReleased,
ReleaseDate=@ReleaseDate,
ReleasedByUserID=@ReleasedByUserID,
ReleaseApplicationID=@ReleaseApplicationID
WHERE DetainID=@DetainID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsRelesed);

            if (ReleseDate != null)
                command.Parameters.AddWithValue("@ReleaseDate", ReleseDate);
            else
                command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);

            if (ReleasedByUserID > 0)
                command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            else
                command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);


            if (ReleaseApplicationID > 0)
                command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            else
                command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);


            try
            {
                connection.Open();
                int RowsAffected=command.ExecuteNonQuery();
                    IsUpdated = RowsAffected>0;


            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return IsUpdated;

        }

        public static bool GetDetaiedLicenseInfoByID(int DetainID,ref int LicenseID,
            ref DateTime DetainDate,ref float FineFees,ref int CreatedByUserID,ref bool IsRelesed,ref DateTime ReleseDate,ref int ReleasedByUserID,ref int ReleaseApplicationID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection();
            string Query = @"SELECT * FROM DetainedLicenses WHERE DetainID=@DetainID; ";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            

            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();
                if(reader.Read())
                {
                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (int)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUser"];
                    IsRelesed = (bool)reader["IsReleased"];

                    if (reader["ReleaseDate"] == DBNull.Value)
                        ReleseDate = DateTime.MinValue;
                    else
                        ReleseDate =(DateTime)reader["ReleaseDate"];

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                        ReleasedByUserID = -1;
                    else
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    if (reader["ReleasedApplicationID"] == DBNull.Value)
                        ReleaseApplicationID = -1;
                    else
                        ReleaseApplicationID = (int)reader["ReleasedApplicationID"];

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

        public static bool GetDetaiedLicenseInfoByLicenseID(int LicenseID,ref int DetainID,
            ref DateTime DetainDate, ref float FineFees, ref int CreatedByUserID, ref bool IsRelesed, ref DateTime ReleseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM DetainedLicenses WHERE LicenseID=@LicenseID
                            ORDER BY DetainDate DESC";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);


            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DetainID = (int)reader["DetainID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = (float)((decimal)reader["FineFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsRelesed = (bool)reader["IsReleased"];

                    if (reader["ReleaseDate"] == DBNull.Value)
                        ReleseDate = DateTime.MinValue;
                    else
                        ReleseDate = (DateTime)reader["ReleaseDate"];

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                        ReleasedByUserID = -1;
                    else
                        ReleasedByUserID = (int)reader["ReleasedByUserID"];

                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                        ReleaseApplicationID = -1;
                    else
                        ReleaseApplicationID = (int)reader["ReleaseApplicationID"];

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

        public static DataTable GetAllDetaiedLicenses()
        {
            DataTable dt=new DataTable();
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM  DetainedLicenses_View
ORDER BY DetainID DESC;";
            SqlCommand command=new SqlCommand(Query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();
                if(reader.HasRows)
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

        public static bool IsLicenseDetained(int LicenseID)
        {
            bool IsDetain = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT TOP 1 1 FROM DetainedLicenses WHERE LicenseID=@LicenseID AND IsReleased=0;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsDetain = reader.HasRows;
                reader.Close();

            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return IsDetain;
        }
    
        public static bool ReleaseDetainLicense(int DetainID,int ReleasedByUserID,int ReleaseAppID)
        {
            bool IsReleased=false;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"UPDATE DetainedLicenses
                              SET ReleaseDate=@ReleaseDate,
                                  IsReleased=1,
                                  ReleaseApplicationID=@ReleaseApplicationID,
                                  ReleasedByUserID=@ReleasedByUserID
                                  WHERE DetainID=@DetainID;";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@ReleaseDate",DateTime.Now );
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseAppID);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            
            try
            {
                connection.Open();
                int RowsAffected = -1;
                RowsAffected = command.ExecuteNonQuery();
                IsReleased=RowsAffected > 0;
            }
            catch(Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return IsReleased;


        }
    }
}
