using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EcoLeafPlates.Models.DBManager
{
    public class ContactManager
    {

        public static int addContact(Contact con)
        {
            int result = 0;
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;
                //DateTime.Parse(System.DateTime.Today.Date.ToString("dd/MM/yyyy"));
                con.Date = DateTime.Now.ToShortDateString();
                con.Viwed = false;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand command = new SqlCommand("SP_CONTACTINSERTDELETEDISPLAY", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CONID", con.ConId));
                    command.Parameters.Add(new SqlParameter("@NAME", con.Name));
                    command.Parameters.Add(new SqlParameter("@PHONENO", con.PhoneNo));
                    command.Parameters.Add(new SqlParameter("@DATE", con.Date));
                    command.Parameters.Add(new SqlParameter("@QUERY", con.Query));
                    command.Parameters.Add(new SqlParameter("@VIWED", con.Viwed));
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

        public static List<Contact> displayContactDetails()
        {
            List<Contact> lstContactDetails = new List<Contact>();


            try
            {

                String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {


                    SqlCommand command = new SqlCommand("SP_CONTACTINSERTDELETEDISPLAY", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CONID", 1));
                    command.Parameters.Add(new SqlParameter("@NAME", String.Empty));
                    command.Parameters.Add(new SqlParameter("@PHONENO", String.Empty));
                    command.Parameters.Add(new SqlParameter("@DATE", String.Empty));
                    command.Parameters.Add(new SqlParameter("@QUERY", String.Empty));
                    command.Parameters.Add(new SqlParameter("@VIWED", false));
                    command.Parameters.Add(new SqlParameter("@STATEMENTTYPE", "SELECT"));
                    connection.Open();
                    SqlDataReader registerReader = command.ExecuteReader();
                    if (registerReader != null)
                    {

                        while (registerReader.Read())
                        {
                            Contact objContactDetails = new Contact();
                            objContactDetails.ConId = Convert.ToInt32(registerReader["CONID"]);
                            objContactDetails.Name = Convert.ToString(registerReader["Name"]);
                            objContactDetails.PhoneNo = Convert.ToInt64(registerReader["PHONENO"]);
                            objContactDetails.Date = Convert.ToString(registerReader["Date"]);
                            objContactDetails.Query = Convert.ToString(registerReader["QUERY"]);
                            objContactDetails.Viwed = Convert.ToBoolean(registerReader["Viwed"]);
                            lstContactDetails.Add(objContactDetails);

                        }
                    }
                }


            }
            catch (Exception ex)
            {

            }
            return lstContactDetails;
        }

        public static DataTable getContactDetailsForReports()
        {
            DataTable lstContactDetails = new DataTable();


            try
            {

                String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {


                    SqlCommand command = new SqlCommand("SP_CONTACTINSERTDELETEDISPLAY", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CONID", 1));
                    command.Parameters.Add(new SqlParameter("@NAME", String.Empty));
                    command.Parameters.Add(new SqlParameter("@PHONENO", String.Empty));
                    command.Parameters.Add(new SqlParameter("@DATE", String.Empty));
                    command.Parameters.Add(new SqlParameter("@QUERY", String.Empty));
                    command.Parameters.Add(new SqlParameter("@VIWED", false));
                    command.Parameters.Add(new SqlParameter("@STATEMENTTYPE", "SELECT"));
                    connection.Open();
                    SqlDataReader registerReader = command.ExecuteReader();
                    if (registerReader != null)
                    {


                        lstContactDetails.Load(registerReader);


                    }
                }


            }
            catch (Exception ex)
            {

            }
            return lstContactDetails;
        }

        public static int editContactDetails(Contact contactDetails)
        {
            int result = 0;
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand command = new SqlCommand("SP_CONTACTINSERTDELETEDISPLAY", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CONID", contactDetails.ConId));
                    command.Parameters.Add(new SqlParameter("@NAME", contactDetails.Name));
                    command.Parameters.Add(new SqlParameter("@PHONENO", contactDetails.PhoneNo));
                    command.Parameters.Add(new SqlParameter("@DATE", contactDetails.Date));
                    command.Parameters.Add(new SqlParameter("@QUERY", contactDetails.Query));
                    command.Parameters.Add(new SqlParameter("@VIWED", contactDetails.Viwed));
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

        public static int deleteContactDetails(int conId)
        {
            int result = 0;
            List<Contact> lstContactDetails = new List<Contact>();


            try
            {

                String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {


                    SqlCommand command = new SqlCommand("SP_CONTACTINSERTDELETEDISPLAY", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CONID", conId));
                    command.Parameters.Add(new SqlParameter("@NAME", String.Empty));
                    command.Parameters.Add(new SqlParameter("@PHONENO", String.Empty));
                    command.Parameters.Add(new SqlParameter("@DATE", String.Empty));
                    command.Parameters.Add(new SqlParameter("@QUERY", String.Empty));
                    command.Parameters.Add(new SqlParameter("@VIWED", false));
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