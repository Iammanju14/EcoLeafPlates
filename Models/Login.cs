using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcoLeafPlates.Models
{

    public class Login
    {
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }


    }
}