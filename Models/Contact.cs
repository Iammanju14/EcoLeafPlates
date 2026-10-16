using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcoLeafPlates.Models
{
    public class Contact
    {
        public int ConId { set; get; }
        public String Name { set; get; }
        public long PhoneNo { set; get; }
        public String Date { set; get; }
        public String Query { set; get; }
        public bool Viwed { set; get; }
    }
}