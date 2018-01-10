using Entity;
using MvcUtilitys;
using System;
using System.ComponentModel.DataAnnotations;

namespace YoRent.Models
{
    //validate user registration
    public class UserRegisterValidation
    {
        [Required]
        [ValidateUser(ErrorMessage ="User Already In DataBase")]
        public string UserName { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required, StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        [Required,RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$")]
        public string Email { get; set; }

        [Required,ValidateIdCard]
        public string IdCard { get; set; }

        public DateTime? BirthDate { get; set; }

        [Required]
        public Gender Sex { get; set; }

        //implict conversion to User object so i can use it in the entity
        public static implicit operator User(UserRegisterValidation user)
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