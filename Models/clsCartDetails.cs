using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EcoLeafPlates.Models
{
    public class clsCartDetails
    {
        public int cartId { set; get; }
        public int userId { set; get; }
        public int productId { set; get; }
        public int quantity { set; get; }
        public int price { set; get; }
    }
    public class clsCartDetailsToDisplay
    {
        public int cartId { set; get; }
        public string ProductName { set; get; }
        [DisplayName("Quantity")]
        public int quantity { set; get; }
        [DisplayName("Price")]
        public int price { set; get; }
        public int total { set; get; }
    }
}