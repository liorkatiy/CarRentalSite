using System;
using Entity;
using System.ComponentModel.DataAnnotations;
using MvcUtilitys;

namespace YoRent.Models
{
    public class OrderValidation 
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public DateTime? ReturnDate { get; set; }

        [ValidateDate]
        public RentTime Rent
        {
            get
            {
                return new RentTime()
                {
                    Start = StartDate,
                    End = EndDate
                };
            }
        }

        [Required, MaxLength(50)]
        public string UserId { get; set; }

        [Required, MaxLength(50)]
        public string CarId { get; set; }

        //implict conversion to Order object so i can use it in the entity
        public static implicit operator Order(OrderValidation orderValidation)
        {
            return new Order()
            {
                startdate = orderValidation.StartDate,
                enddate = orderValidation.EndDate,
                returndate = orderValidation.ReturnDate,
                carid = orderValidation.CarId,
                userid = orderValidation.UserId
            };
        }
    }
}