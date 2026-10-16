using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EcoLeafPlates.Models.DBManager
{
    
        public class UserManager
        {


            public static int addUser(clsUserDetails userDetails)
            {
                int result = 0;
                try
                {
                    String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {


                        SqlCommand command = new SqlCommand("SP_USERDETAILSINSERTUPDATEDELETEDISPLAY", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@userId", userDetails.userId));
                        command.Parameters.Add(new SqlParameter("@name", userDetails.name));
                        command.Parameters.Add(new SqlParameter("@phone", userDetails.phoneNo));
                        command.Parameters.Add(new SqlParameter("@address", userDetails.address));
                        if (userDetails.mailId != null)
                        {
                            command.Parameters.Add(new SqlParameter("@MAILID", userDetails.mailId));
                        }
                        else
                        {
                            command.Parameters.Add(new SqlParameter("@MAILID", DBNull.Value));
                        }
                        command.Parameters.Add(new SqlParameter("@password", userDetails.passowrd));
                        command.Parameters.Add(new SqlParameter("@date", userDetails.date));
                        command.Parameters.Add(new SqlParameter("@isActive", userDetails.isActive));
                        command.Parameters.Add(new SqlParameter("@ROLE", userDetails.Role));

                        command.Parameters.Add(new SqlParameter("@STATEMENTTYPE", "INSERT"));
                        connection.Open();
                        result = command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {

                }
                return result;
            }

            public static clsUserDetails addUserDefaultEntries(clsUserDetails userDetails)
            {
                var dateTimeNow = DateTime.Now;
                dateTimeNow.ToShortDateString();

                userDetails.date = dateTimeNow.ToShortDateString();
                userDetails.isActive = true;
                userDetails.Role = "User";
                return userDetails;

            }

            public static List<clsUserDetails> displayUserDetails()
            {
                List<clsUserDetails> lstUserDetails = new List<clsUserDetails>();


                try
                {

                    String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {


                        SqlCommand command = new SqlCommand("SP_USERDETAILSINSERTUPDATEDELETEDISPLAY", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@userId", 1));
                        command.Parameters.Add(new SqlParameter("@name", string.Empty));
                        command.Parameters.Add(new SqlParameter("@phone", 1));
                        command.Parameters.Add(new SqlParameter("@address", string.Empty));
                        command.Parameters.Add(new SqlParameter("@MAILID", string.Empty));
                        command.Parameters.Add(new SqlParameter("@password", string.Empty));
                        command.Parameters.Add(new SqlParameter("@date", string.Empty));
                        command.Parameters.Add(new SqlParameter("@isActive", false));
                        command.Parameters.Add(new SqlParameter("@Role", string.Empty));
                        command.Parameters.Add(new SqlParameter("@STATEMENTTYPE", "SELECT"));
                        connection.Open();
                        SqlDataReader registerReader = command.ExecuteReader();
                        if (registerReader != null)
                        {

                            while (registerReader.Read())
                            {
                                clsUserDetails objclsUserDetails = new clsUserDetails();
                                objclsUserDetails.userId = Convert.ToInt32(registerReader["USERID"]);
                                objclsUserDetails.name = Convert.ToString(registerReader["Name"]);
                                objclsUserDetails.phoneNo = Convert.ToString(registerReader["Phone"]);
                                objclsUserDetails.address = Convert.ToString(registerReader["address"]);
                                objclsUserDetails.mailId = Convert.ToString(registerReader["MailId"]);
                                objclsUserDetails.passowrd = Convert.ToString(registerReader["password"]);
                                objclsUserDetails.date = Convert.ToString(registerReader["Date"]);
                                objclsUserDetails.isActive = Convert.ToBoolean(registerReader["IsActive"]);
                                objclsUserDetails.Role = Convert.ToString(registerReader["role"]);
                                lstUserDetails.Add(objclsUserDetails);

                            }
                        }
                    }


                }
                catch (Exception ex)
                {

                }
                return lstUserDetails;
            }

            public static int editUserDetails(clsUserDetails userDetails)
            {
                int result = 0;
                try
                {
                    String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {

                        SqlCommand command = new SqlCommand("SP_USERDETAILSINSERTUPDATEDELETEDISPLAY", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@userId", userDetails.userId));
                        command.Parameters.Add(new SqlParameter("@name", userDetails.name));
                        command.Parameters.Add(new SqlParameter("@phone", userDetails.phoneNo));
                        command.Parameters.Add(new SqlParameter("@address", userDetails.address));
                        if (userDetails.mailId != null)
                        {
                            command.Parameters.Add(new SqlParameter("@MAILID", userDetails.mailId));
                        }
                        else
                        {
                            command.Parameters.Add(new SqlParameter("@MAILID", DBNull.Value));
                        }
                        command.Parameters.Add(new SqlParameter("@password", userDetails.passowrd));
                        command.Parameters.Add(new SqlParameter("@date", userDetails.date));
                        command.Parameters.Add(new SqlParameter("@isActive", userDetails.isActive));
                        command.Parameters.Add(new SqlParameter("@Role", userDetails.Role));
                        command.Parameters.Add(new SqlParameter("@STATEMENTTYPE", "EDIT"));
                        connection.Open();
                        result = command.ExecuteNonQuery();

                    }
                }
                catch (Exception ex)
                {

                }
                return result;
            }


            public static int deleteUserDetails(int UserId)
            {
                int result = 0;


                try
                {

                    String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {


                        SqlCommand command = new SqlCommand("SP_USERDETAILSINSERTUPDATEDELETEDISPLAY", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@userId", UserId));
                        command.Parameters.Add(new SqlParameter("@name", string.Empty));
                        command.Parameters.Add(new SqlParameter("@phone", 1));
                        command.Parameters.Add(new SqlParameter("@address", string.Empty));
                        command.Parameters.Add(new SqlParameter("@MAILID", string.Empty));
                        command.Parameters.Add(new SqlParameter("@password", string.Empty));
                        command.Parameters.Add(new SqlParameter("@date", string.Empty));
                        command.Parameters.Add(new SqlParameter("@isActive", false));
                        command.Parameters.Add(new SqlParameter("@Role", string.Empty));

                        command.Parameters.Add(new SqlParameter("@STATEMENTTYPE", "DELETE"));
                        connection.Open();
                        result = command.ExecuteNonQuery();
                    }


                }
                catch (Exception ex)
                {

                }
                return result;
            }

        
    }
}