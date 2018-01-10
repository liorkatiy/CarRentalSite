using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Entity;

namespace MvcUtilitys
{
    /// <summary>
    /// Validate The User Name Isnt Already In The DataBase
    /// </summary>
    public class ValidateUser : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string username = (string)value;
            if (value != null)
            {
                using (UsersData db = new UsersData())
                {
                    return !db.Contains(username);
                }
            }
            return false;
        }
    }

   /// <summary>
   /// Validate The The Start Date Is Less Than End Date
   /// </summary>
    public class ValidateDate : ValidationAttribute
    {
        /// <summary>
        /// If Set To True Wont Check That Start Date Is Greater Than Today
        /// </summary>
        public bool FutureOrder { get; set; }

        public override bool IsValid(object value)
        {
            if (value != null && value.GetType() == typeof(RentTime))
            {
                RentTime date = (RentTime)value;
                return (date.Start.Date >= DateTime.Now.Date ||
                    !FutureOrder)
                    && date.End.Date >= date.Start.Date;
            }
            return false;
        }
    }

    /// <summary>
    /// Validate The ID Card
    /// </summary>
    public class ValidateIdCard : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string idCrard = value as string;
            if (idCrard == null || idCrard.Length != 9)
                return false;
            int sum = 0;
            int n;
            for (int i = 0; i < idCrard.Length; i++)
            {
                if (int.TryParse(idCrard[i].ToString(), out n))
                {
                    n *= i % 2 + 1;
                    sum += n > 9 ? (n / 10) + (n % 10) : n;
                }
                else return false;
            }
            return sum % 10 == 0;
        }
    }
}