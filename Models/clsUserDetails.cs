using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcoLeafPlates.Models
{
    
        public class clsUserDetails
        {
            [DisplayName("UserId:")]
            public int userId { get; set; }
            [DisplayName("Name")]
            [Required]
            public string name { get; set; }
            [DisplayName("Phone No")]
            [Required]
            public string phoneNo { get; set; }
            [DisplayName("Address")]
            [Required]
            public string address { get; set; }
            [DisplayName("Mail Id")]
            public string mailId { get; set; }
            [DisplayName("Passowrd")]
            [Required]
            public string passowrd { get; set; }
            [DisplayName("Date")]
            public String date { get; set; }
            [DisplayName("Active:")]
            public bool isActive { get; set; }

            public string Role { get; set; }

        }
    
}