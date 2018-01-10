using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YoRent.Models
{
    //object to validate user login
    public class UserLoginValidation
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }

        public string Error { get; set; }

    }
}