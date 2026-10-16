using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoLeafPlates.Models
{
    public class clsProductDetails
    {
        public int productId { set; get; }
        public string productName { set; get; }
        public int productPrice { set; get; }
        public bool isActive { set; get; }
    }
}