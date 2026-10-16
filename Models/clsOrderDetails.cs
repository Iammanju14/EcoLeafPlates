using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EcoLeafPlates.Models
{
    public class clsOrderDetails
    {

         
        public int orderDetailId {get;set;}

        [DisplayName("Order Id:")]
        public int orderId { get; set; }
         [DisplayName("Ordered Item:")]
        public string orderItem { get; set; }
        [DisplayName("Order Quantity:")]
        public int orderQuantity { get; set; }
        [DisplayName("Price:")]
        public int price { get; set; }
        [DisplayName("Status:")]
        public string orderStatus { get; set; }
    }

   
}
