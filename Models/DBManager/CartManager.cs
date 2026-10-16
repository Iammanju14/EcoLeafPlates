using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EcoLeafPlates.Models.DBManager
{
    public class CartManager
    {
        public static int addToCart(clsCartDetails cartDetails)
        {
            int result = 0;
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {


                    SqlCommand command = new SqlCommand("SP_CARTDETAILSINSERTUPDATEDELETEDISPLAY", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@CARTID", cartDetails.cartId));
                    command.Parameters.Add(new SqlParameter("@USERID", cartDetails.userId));
                    command.Parameters.Add(new SqlParameter("@PRODUCTID", cartDetails.productId));
                    command.Parameters.Add(new SqlParameter("@QUANTITY", cartDetails.quantity));
                    command.Parameters.Add(new SqlParameter("@PRICE", cartDetails.price));
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

        public static int getProductPrice(int productId)
        {

            int price = 0;
            try
            {

                String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("SP_GETPRODUCTPRICE", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@productId", productId));
                    command.Parameters.Add("@Price",SqlDbType.Int);
                    command.Parameters["@Price"].Direction = ParameterDirection.Output;
                    connection.Open();
                    command.ExecuteNonQuery();
                    price = Convert.ToInt32(command.Parameters["@Price"].Value);   
                }
            }
            catch (Exception ex)
            {

            }
            return price;
        }


        public static List<clsCartDetails> displayCartDetails()
        {
            List<clsCartDetails> lstCartDetails = new List<clsCartDetails>();


            try
            {

                String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {


                    SqlCommand command = new SqlCommand("SP_CARTDETAILSINSERTUPDATEDELETEDISPLAY", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CARTID", 1));
                    command.Parameters.Add(new SqlParameter("@USERID", Convert.ToInt16(HttpContext.Current.Session["userId"])));
                    command.Parameters.Add(new SqlParameter("@PRODUCTID", 1));
                    command.Parameters.Add(new SqlParameter("@QUANTITY", 1));
                    command.Parameters.Add(new SqlParameter("@PRICE", 1));
                    command.Parameters.Add(new SqlParameter("@STATEMENTTYPE", "SELECT"));
                    connection.Open();
                    SqlDataReader registerReader = command.ExecuteReader();
                    if (registerReader != null)
                    {

                        while (registerReader.Read())
                        {
                            clsCartDetails objclsCartDetails = new clsCartDetails();
                           
                            objclsCartDetails.cartId = Convert.ToInt32(registerReader["cartId"]);
                            objclsCartDetails.userId = Convert.ToInt32(registerReader["userId"]);
                            objclsCartDetails.productId = Convert.ToInt32(registerReader["productId"]);
                            objclsCartDetails.quantity = Convert.ToInt32(registerReader["quantity"]);

                            objclsCartDetails.price = Convert.ToInt32(registerReader["price"]);

                            lstCartDetails.Add(objclsCartDetails);

                        }
                    }
                }


            }
            catch (Exception ex)
            {

            }
            return lstCartDetails;
        }


        public static int editCartDetails(clsCartDetailsToDisplay cartDetails)
        {
            int result = 0;
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    SqlCommand command = new SqlCommand("SP_CARTDETAILSINSERTUPDATEDELETEDISPLAY", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@CARTID", cartDetails.cartId));
                    command.Parameters.Add(new SqlParameter("@USERID", Convert.ToInt16(HttpContext.Current.Session["userId"])));
                    command.Parameters.Add(new SqlParameter("@PRODUCTID", 1));
                    command.Parameters.Add(new SqlParameter("@QUANTITY", cartDetails.quantity));
                    command.Parameters.Add(new SqlParameter("@PRICE", cartDetails.price));
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

        public static int deleteCartDetails(int cartId)
        {
            int result = 0;


            try
            {

                String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {


                    SqlCommand command = new SqlCommand("SP_CARTDETAILSINSERTUPDATEDELETEDISPLAY", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@CARTID", cartId));
                    command.Parameters.Add(new SqlParameter("@USERID", Convert.ToInt16(HttpContext.Current.Session["userId"])));
                    command.Parameters.Add(new SqlParameter("@PRODUCTID", 1));
                    command.Parameters.Add(new SqlParameter("@QUANTITY", 1));
                    command.Parameters.Add(new SqlParameter("@PRICE", 1));

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