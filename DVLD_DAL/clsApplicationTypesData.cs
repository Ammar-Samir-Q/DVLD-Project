using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DVLD_DAL
{
    public class clsApplicationTypesData
    {
        public static bool UpdateTypes(int TypeID,string Title,float Fees)
        {
            bool IsUpdated=false;
            SqlConnection connection= new SqlConnection(DataHelper.ConnectionString);
            string Query = @"UPDATE ApplicationTypes
                            SET ApplicationTypeTitle=@ApplicationTypeTitle,
                            ApplicationFees=@ApplicationFees
                            WHERE ApplicationTypeID=@ApplicationTypeID;";
            SqlCommand command= new SqlCommand(Query,connection);  
            command.Parameters.AddWithValue("@ApplicationTypeTitle",Title);
            command.Parameters.AddWithValue("@ApplicationFees",Fees);
            command.Parameters.AddWithValue("@ApplicationTypeID", TypeID);
            try
            {
                connection.Open();
                int RowsAffected=command.ExecuteNonQuery();
                IsUpdated=(RowsAffected>0);


            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            { connection.Close(); }
            return IsUpdated;


        }

        public static DataTable GetAllApplicationTypes()
        {
            DataTable Data = new DataTable();
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM ApplicationTypes;";
            SqlCommand command=new SqlCommand(Query,connection);
            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();
                if(reader.HasRows)
                {
                    Data.Load(reader);
                }

            }
            catch(Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally { connection.Close(); }
            return Data;
        }

        public static bool Find(int TypeID,ref string Title,ref float Fee)
        {
            bool IsFound = false;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM ApplicationTypes WHERE ApplicationTypeID=@ApplicationTypeID;";
            SqlCommand command=new SqlCommand(Query,connection);
            command.Parameters.AddWithValue("@ApplicationTypeID",TypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if(reader.Read())
                {
                    Title=(string)reader["ApplicationTypeTitle"];
                    Fee = float.Parse(reader["ApplicationFees"].ToString());
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

        public static int AddNewApplicationType(string Title,float Fees)
        {
            int TypeID = -1;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"Insert Into ApplicationTypes (ApplicationTitle,ApplicationFees) values (@ApplicationTitle,@ApplicationFees);
                             SELECT SCOPE_IDENTITY();";
            SqlCommand command=new SqlCommand(Query,connection);
            command.Parameters.AddWithValue("@ApplicationTitle",Title);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);
            try
            {
                connection.Open();
                object result=command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                    TypeID = ID;

            }
            catch(Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);


            }
            finally
            {
                connection.Close();
            }

            return TypeID;

        }
    
    }
}
