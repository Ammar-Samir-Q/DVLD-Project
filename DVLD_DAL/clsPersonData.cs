using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace DVLD_DAL
{
    public class clsPersonData
    {
        public static  int AddNewPerson(string NationalNo,string FirstName,string SecondName,string ThirdName,string LastName,
           DateTime DateOfBirth,byte Gendor,string Address,string Phone,string Email,int NationalityCountryID,string ImagePath)
        {
            int PersonID = -1;
            SqlConnection Connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"INSERT INTO People (NationalNo,FirstName,SecondName,ThirdName,LastName,DateOfBirth,Gendor,
                                               Address,Phone,Email,NationalityCountryID,ImagePath)
                                               VALUES
                                               (@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,@Gendor,
                                               @Address,@Phone,@Email,@NationalityCountryID,@ImagePath);
                                               SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(Query,Connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName",FirstName);
            command.Parameters.AddWithValue("@SecondName",SecondName);
            //Null Handling
            if (string.IsNullOrEmpty(ThirdName))
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ThirdName", ThirdName);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address",Address);
            command.Parameters.AddWithValue("@Phone",Phone);
            //Null Handling
            if (string.IsNullOrEmpty(Email))
            command.Parameters.AddWithValue("@Email",DBNull.Value);
            else
            command.Parameters.AddWithValue("@Email", Email);

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            //Null Handling
            if (string.IsNullOrEmpty (ImagePath))
            command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            else
            command.Parameters.AddWithValue("@ImagePath", ImagePath);

            try
            {
                Connection.Open();
               object Result=command.ExecuteScalar();
                if(Result!=null&&int.TryParse(Result.ToString(),out int ID))
                    PersonID = ID;
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            {
                Connection.Close();
            }

            return PersonID;
        }

        public static  bool UpdatePerson(int PersonID,string NationalNo, string FirstName, string SecondName, string ThirdName, string LastName,
            DateTime DateOfBirth, byte Gendor, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            bool IsUpdated = false;
            SqlConnection Connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"UPDATE People
                                               SET NationalNo = @NationalNo
                                                  ,FirstName = @FirstName
                                                  ,SecondName = @SecondName
                                                  ,ThirdName = @ThirdName
                                                  ,LastName = @LastName
                                                  ,DateOfBirth = @DateOfBirth
                                                  ,Gendor = @Gendor
                                                  ,Address = @Address
                                                  ,Phone = @Phone
                                                  ,Email = @Email
                                                  ,NationalityCountryID = @NationalityCountryID
                                                  ,ImagePath = @ImagePath
                                             WHERE PersonID=@PersonID;";

            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            //Null Handling
            if (string.IsNullOrEmpty(ThirdName))
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ThirdName", ThirdName);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            //Null Handling
            if (string.IsNullOrEmpty(Email))
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Email", Email);

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            //Null Handling
            if (string.IsNullOrEmpty(ImagePath))
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ImagePath", ImagePath);

            try
            {
                Connection.Open();
                int RowsAffected = command.ExecuteNonQuery();
                IsUpdated = (RowsAffected > 0);
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            {
                Connection.Close();
            }

            return IsUpdated;
        }

        public static bool FindPersonByPersonID(int PersonID, ref string NationalNo, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
           ref DateTime DateOfBirth, ref byte Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM People WHERE PersonID=@PersonID;";
            
            SqlCommand command=new SqlCommand(Query,Connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                Connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if(Reader.Read())
                {
                    NationalNo = (string)Reader["NationalNo"];
                    FirstName = (string)Reader["FirstName"];
                    SecondName = (string)Reader["SecondName"];
                    //Null Handling
                    if (Reader["ThirdName"] == DBNull.Value)
                        ThirdName = "";
                    else
                    ThirdName = (string)Reader["ThirdName"];

                    LastName = (string)Reader["LastName"];
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    Gendor = (byte)Reader["Gendor"];
                    Address = (string)Reader["Address"];
                    Phone = (string)Reader["Phone"];
                    //Null Handling
                    if (Reader["Email"] == DBNull.Value)
                        Email = "";
                    else
                    Email = (string)Reader["Email"];

                    NationalityCountryID = (int)Reader["NationalityCountryID"];
                    //Null Handling
                    if (Reader["ImagePath"] == DBNull.Value)
                        ImagePath = "";
                    else
                    ImagePath = (string)Reader["ImagePath"];
                    IsFound=true;
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }


        public static bool FindPersonByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName,
     ref DateTime DateOfBirth, ref byte Gendor, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool IsFound = false;
            SqlConnection Connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT * FROM People WHERE NationalNo=@NationalNo;";

            SqlCommand command = new SqlCommand(Query, Connection);
            command.Parameters.AddWithValue("@NationalNo",NationalNo);
            try
            {
                Connection.Open();
                SqlDataReader Reader = command.ExecuteReader();
                if (Reader.Read())
                {
                    PersonID = (int)Reader["PersonID"];
                    FirstName = (string)Reader["FirstName"];
                    SecondName = (string)Reader["SecondName"];
                    if (Reader["ThirdName"] == DBNull.Value)
                        ThirdName = "";
                    else
                        ThirdName = (string)Reader["ThirdName"];

                    LastName = (string)Reader["LastName"];
                    DateOfBirth = (DateTime)Reader["DateOfBirth"];
                    Gendor = (byte)Reader["Gendor"];
                    Address = (string)Reader["Address"];

                    Phone = (string)Reader["Phone"];
                    if (Reader["Email"] == DBNull.Value)
                        Email = "";
                    else
                        Email = (string)Reader["Email"];

                    NationalityCountryID = (int)Reader["NationalityCountryID"];
                    if (Reader["ImagePath"] == DBNull.Value)
                        ImagePath = "";
                    else
                        ImagePath = (string)Reader["ImagePath"];
                    IsFound = true;
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally
            {
                Connection.Close();
            }
            return IsFound;
        }



        public static bool DeletePerson(int PersonID)
        {
            bool IsDeleted = false;
            SqlConnection connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"DELETE FROM People WHERE PersonID=@PersonID;";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                int AffectedRows=command.ExecuteNonQuery();
                IsDeleted=(AffectedRows>0);
            }
            catch(Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            { connection.Close(); }
            return IsDeleted;   
        }

        public static bool IsPersonExists(int PersonID)
        {
            bool IsExists = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT 1 FRom People WHERE PersonID=@PersonID;";
            SqlCommand command=new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@PersonID",PersonID);
 
            try
            {
                connection.Open();
                SqlDataReader reader=command.ExecuteReader();
                IsExists=(reader.HasRows);
                reader.Close();
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            { connection.Close();}
            return IsExists;  
        }

        public static bool IsPersonExists(string NationalNo)
        {
            bool IsExists = false;
            SqlConnection connection = new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT 1 FROM People WHERE NationalNo=@NationalNo;";
            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                IsExists = (reader.HasRows);
                reader.Close();
            }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);
            }
            finally
            { connection.Close(); }
            return IsExists;
        }


        public static DataTable GetAllPeople()
        {
            DataTable DataTable = new DataTable();
            SqlConnection Connection=new SqlConnection(DataHelper.ConnectionString);
            string Query = @"SELECT PersonID,NationalNo,FirstName,SecondName,ThirdName,LastName,
                                            CASE
                                            WHEN People.Gendor=0 THEN 'Male'
                                            ELSE 'Female'
                                            END
                                             As Gendor
            ,DateOfBirth,Countries.CountryName As Nationalty,Phone,Email FROM People
            join Countries on People.NationalityCountryID=Countries.CountryID;";
            SqlCommand Command=new SqlCommand(Query, Connection);
            try
            {
                Connection.Open();
                SqlDataReader Reader = Command.ExecuteReader();
                if (Reader.Read())
                {
                    DataTable.Load(Reader); 
                }
                Reader.Close();
                    }
            catch (Exception ex)
            {
                Util.Logger.LogHelper.Log("DVLD", ex.Message, System.Diagnostics.EventLogEntryType.Error);

            }
            finally 
            { 
                Connection.Close();
            }
                return DataTable;
        }


    }
}
