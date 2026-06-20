using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DVLD_DAL
{
    public class clsUserData
    {

        public static int AddUser(int PersonID,string UserName,string Password,bool IsActive)
        {
            int UserID = -1;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"INSERT INTO Users
                                            VALUES
                                            (@PersonID,@UserName,@Password,@IsActive);
                                            SELECT SCOPE_IDENTITY();";

            SqlCommand command= new SqlCommand(Query,connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName",UserName);
            command.Parameters.AddWithValue("Password",Password);
            command.Parameters.AddWithValue("@IsActive",IsActive);
            try
            {
                connection.Open();
                object result=command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int ID))
                    UserID = ID;
            }
            catch(Exception ex)
            {
                //Handl Error
            }
            finally { connection.Close(); }
            return UserID;
        }

        public static bool UpdateUser(int UserID,int PersonID,string UserName,string Password,bool IsActive)
        {
            bool IsUpdated=false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"UPDATE Users
SET PersonID=@PersonID,
	UserName=@UserName,
	Password=@Password,
	IsActive=@IsActive
	WHERE UserID=@UserID;";
            SqlCommand command = new SqlCommand(Query,connection);
            command.Parameters.AddWithValue("@UserID",UserID);
            command.Parameters.AddWithValue("@PersonID",PersonID);
            command.Parameters.AddWithValue("@UserName",UserName);
            command.Parameters.AddWithValue("@Password",Password);
            command.Parameters.AddWithValue("@IsActive",IsActive);
            try
            {
                connection.Open();
                int RowsAffected=command.ExecuteNonQuery();
                IsUpdated=(RowsAffected>0);
            }
            catch (Exception ex)
            {
                //Handl
            }
            finally { connection.Close(); }
            return IsUpdated;


        }

        public static bool FindUser(int UserID,ref int PersonID,ref string UserName,ref string Password,ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM Users WHERE UserID=@UserID;";
            SqlCommand command=new SqlCommand(Query,connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader= command.ExecuteReader();
                if(reader.Read())
                {
                    PersonID = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                    IsFound=true;
                }
            reader.Close();
            }
            catch(Exception ex)
            { }
            finally { connection.Close(); }
            return IsFound;
        }

        public static bool FindUserByPersonID(int PersonID, ref int UserID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM Users WHERE PersonID=@PersonID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserID = (int)reader["UserID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];
                    IsFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }
            return IsFound;
        }

        public static bool FindUserByUserNameAndPassword(string UserName, string Password,ref int PersonID, ref int UserID,ref bool IsActive)
        {
            bool IsFound = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM Users WHERE UserName=@UserName And Password=@Password;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserID = (int)reader["UserID"];
                    PersonID = (int)reader["PersonID"];
                    IsActive = (bool)reader["IsActive"];
                    IsFound = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }
            return IsFound;
        }


        public static bool DeleteUser(int UserID)
        {
            bool IsDeleted = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = "DELETE FROM Users WHERE UserID=@UserID;";
            SqlCommand command= new SqlCommand(Query, connection); 
            command.Parameters.AddWithValue("@UserID",UserID);
            try
            {
                connection.Open();
                int RowsAffected=command.ExecuteNonQuery();
                IsDeleted = (RowsAffected > 0);
            }
            catch(Exception ex)
            {

            }
            finally { connection.Close(); }
            return IsDeleted;


        }

        public static DataTable GetAllUsers()
        {
            DataTable dtUsers=new DataTable();
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT Users.UserID,Users.PersonID,People.FirstName+' '+People.SecondName+' '+People.ThirdName+' '+People.LastName As FullName,
Users.UserName,Users.IsActive FROM Users join People ON Users.PersonID=People.PersonID;";
            SqlCommand command = new SqlCommand(Query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();
                if(reader.HasRows)
                {
                    dtUsers.Load(reader);
                }
            }
            catch(Exception ex)
            {

            }
            finally
            { connection.Close(); }
            return dtUsers;
        }

        public static bool IsUserExists(string UserName)
        {
            bool IsExists = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT 1 FROM Users WHERE UserName=@UserName;";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@UserName",UserName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsExists= (reader.HasRows);
                reader.Close();
            }
            catch (Exception ex)
            { }
            finally { connection.Close(); }
            return IsExists;
        }

        public static bool IsUserExistsByPersonID(int PersonID)
        {
            bool IsExists = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT 1 FROM Users WHERE PersonID=@PersonID;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID",PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsExists = (reader.HasRows);
                reader.Close();
            }
            catch (Exception ex)
            { 
            
            }
            finally { connection.Close(); }
            return IsExists;
        }

    }
}
