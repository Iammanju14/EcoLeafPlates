using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EcoLeafPlates.Models.DBManager
{
    public class ProductManager
    {
        public static List<clsProductDetails> displayProductDetails()
        {
            List<clsProductDetails> lstProductDetails = new List<clsProductDetails>();


            try
            {

                String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {


                    SqlCommand command = new SqlCommand("SP_PRODUCTDETAILSINSERTUPDATEDELETEDISPLAY", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@PRODUCTID", 1));
                    command.Parameters.Add(new SqlParameter("@PRODUCTNAME", 1));
                    command.Parameters.Add(new SqlParameter("@PRODUCTPRICE", 1));
                    command.Parameters.Add(new SqlParameter("@ISACTIVE", 1));

                    command.Parameters.Add(new SqlParameter("@STATEMENTTYPE", "SELECT"));
                    connection.Open();
                    SqlDataReader registerReader = command.ExecuteReader();
                    if (registerReader != null)
                    {

                        while (registerReader.Read())
                        {
                            clsProductDetails objclsProductDetails = new clsProductDetails();

                            objclsProductDetails.productId = Convert.ToInt32(registerReader["PRODUCTID"]);
                            objclsProductDetails.productName = Convert.ToString(registerReader["PRODUCTNAME"]);
                            objclsProductDetails.productPrice = Convert.ToInt32(registerReader["PRODUCTPRICE"]);
                            objclsProductDetails.isActive = Convert.ToBoolean(registerReader["ISACTIVE"]);



                            lstProductDetails.Add(objclsProductDetails);

                        }
                    }
                }


            }
            catch (Exception ex)
            {

            }
            return lstProductDetails;
        }
    }
}