using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EcoLeafPlates.Models.DBManager
{
    public class OrderManager
    {
        public static int addAnOrder(clsOrder orderDetials,List<clsOrderDetails> lstorderDetails)
        {
            int result = 0;
            int detResult = 0;
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;

                //Adding rows to order table
                using (SqlConnection connection = new SqlConnection(connectionString))
                {


                    SqlCommand command = new SqlCommand("SP_ORDERINSERTUPDATEDELETEDISPLAY", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    
                                        

                    command.Parameters.Add(new SqlParameter("@ORDERID", orderDetials.orderId));
                    command.Parameters.Add(new SqlParameter("@USERID",Convert.ToInt16(HttpContext.Current.Session["userId"])));
                    command.Parameters.Add(new SqlParameter("@ORDERAMT", orderDetials.orderAmt));
                    command.Parameters.Add(new SqlParameter("@ISDELIVERED", orderDetials.isDelivered));
                    command.Parameters.Add(new SqlParameter("@DATE", orderDetials.date));
     
                    command.Parameters.Add(new SqlParameter("@STATEMENTTYPE", "INSERT"));
                    connection.Open();
                    result = command.ExecuteNonQuery();
                    connection.Close();


                    //Adding rows to orderdetils table
                    if (result > 0)
                    {
                        detResult = 0;
                        string query = "SELECT MAX(ORDERID) FROM TBL_ORDERS";
                        connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;

                        SqlConnection conn = new SqlConnection(connectionString);
                        SqlCommand comm = new SqlCommand(query, conn);
                        comm.CommandType = CommandType.Text;
                        comm.Connection.Open();
                        int count = Convert.ToInt32(comm.ExecuteScalar());
                        conn.Close();
                        foreach(clsOrderDetails orderDetails in lstorderDetails)
                        {
                            query = "INSERT INTO TBL_ORDERDETAILS(ORDERID,ORDERITEM,ORDERQUANTITY,PRICE,ORDERSTATUS) VALUES (" +
                                count + ",'" + orderDetails.orderItem + "'," + orderDetails.orderQuantity + "," 
                                + orderDetails.price + ",'" + "Order Received" + "')";
                            conn = new SqlConnection(connectionString);
                            comm = new SqlCommand(query, conn);
                            comm.CommandType = CommandType.Text;
                            comm.Connection.Open();
                            detResult = comm.ExecuteNonQuery();
                            conn.Close();

                        }

                        //Deleting the rows from cart table after order details are inserted to orderdetails table
                        if(detResult > 0)
                        {
                            query = "DELETE FROM TBL_CARTDETAILS WHERE USERID = " + Convert.ToInt16(HttpContext.Current.Session["userId"]);                                             
                            conn = new SqlConnection(connectionString);
                            comm = new SqlCommand(query, conn);
                            comm.CommandType = CommandType.Text;
                            comm.Connection.Open();
                            detResult = comm.ExecuteNonQuery();
                            conn.Close();
                        }

                       

                    }

                }
            }
            catch (Exception ex)
            {

            }

            return (detResult);
        }

        public static List<clsOrderDetails> displayPlacedOrderDetails()
        {
            List<clsOrderDetails> lstOrderDetails = new List<clsOrderDetails>();
        
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;
                string query = String.Empty;

                if (HttpContext.Current.Session["userRole"].ToString() == "Admin")
                {
                    query = "SELECT OD.ORDERDETAILID,O.ORDERID,OD.ORDERITEM,OD.ORDERQUANTITY,OD.PRICE,OD.ORDERSTATUS  FROM TBL_ORDERS O INNER JOIN TBL_ORDERDETAILS  OD ON O.ORDERID = OD.ORDERID";
                }
                else
                {
                    query = "SELECT OD.ORDERDETAILID,O.ORDERID,OD.ORDERITEM,OD.ORDERQUANTITY,OD.PRICE,OD.ORDERSTATUS  FROM TBL_ORDERS O INNER JOIN TBL_ORDERDETAILS  OD ON O.ORDERID = OD.ORDERID WHERE O.USERID = " + Convert.ToInt16(HttpContext.Current.Session["userId"]);
                }
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand comm = new SqlCommand(query, conn);
                comm.CommandType = CommandType.Text;
                comm.Connection.Open();

                SqlDataReader OrderReader = comm.ExecuteReader();
              

                if (OrderReader != null)
                {

                    while (OrderReader.Read())
                    {
                        clsOrderDetails objclsOrderDetails = new clsOrderDetails();
                        objclsOrderDetails.orderDetailId = Convert.ToInt32(OrderReader["ORDERDETAILID"]);
                        objclsOrderDetails.orderId = Convert.ToInt32(OrderReader["ORDERID"]);
                        objclsOrderDetails.orderItem = (OrderReader["ORDERITEM"]).ToString();
                        objclsOrderDetails.orderQuantity = Convert.ToInt32(OrderReader["ORDERQUANTITY"]);
                        objclsOrderDetails.price = Convert.ToInt32(OrderReader["PRICE"]);
                        objclsOrderDetails.orderStatus = (OrderReader["ORDERSTATUS"]).ToString();


                        lstOrderDetails.Add(objclsOrderDetails);

                    }
                }



                conn.Close();
            }
            catch (Exception ex)
            {

            }

            return (lstOrderDetails);
        }

        public static int EditPlacedOrderDetails(clsOrderDetails objOrderDetails)
        {
            int result = 0;
            try
            {
                String connectionString = ConfigurationManager.ConnectionStrings["MKGCS"].ConnectionString;

                string query = "UPDATE TBL_ORDERDETAILS SET ORDERITEM = '" 
                    + objOrderDetails.orderItem + "', ORDERQUANTITY = " 
                    + objOrderDetails.orderQuantity + ", PRICE = " 
                    + objOrderDetails.price + ", ORDERSTATUS = '" + objOrderDetails.orderStatus 
                    + "' WHERE ORDERDETAILID = " + objOrderDetails.orderDetailId;

                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand comm = new SqlCommand(query, conn);
                comm.CommandType = CommandType.Text;
                comm.Connection.Open();
                result = comm.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {

            }

            return (result);
        }
    }
}