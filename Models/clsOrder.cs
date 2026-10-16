using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoLeafPlates.Models
{
    public class clsOrder
    {
        public int orderId { get; set; }
        public int userId { get; set; }
        public int orderAmt { get; set; }
        public int isDelivered { get; set; }
        public String date { get; set; }
        
    }
}