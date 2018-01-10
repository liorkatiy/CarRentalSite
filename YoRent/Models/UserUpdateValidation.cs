using Entity;
using MvcUtilitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YoRent.Models
{
    //needed this class because of the validation
    //password isnt required here
    public class UserUpdateValidation
    {
            [Required]
            public string UserName { get; set; }

            [Required]
            public string FullName { get; set; }

            public string Password { get; set; }

            [Required, RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$")]
            public string Email { get; set; }

            [Required, ValidateIdCard]
            public string IdCard { get; set; }

            public DateTime? BirthDate { get; set; }

            [Required]
            public Gender Sex { get; set; }

        //implict conversion to User object so i can use it in the entity
        public static implicit operator User(UserUpdateValidation user)
            {
                return new User()
                {
                    username = user.UserName,
                    fullname = user.FullName,
                    birthdate = user.BirthDate,
                    email = user.Email,
                    idcard = user.IdCard,
                    password = user.Password,
                    sex = (byte)user.Sex
                };
            }
        
    }
}