using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel.DataAnnotations;

namespace YoRent.Models
{
    public class CarTypeValidation
    {
        [Required, MaxLength(50)]
        public string Model { get; set; }

        [Required, MaxLength(50)]
        public string Manufacturer { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public bool IsManual { get; set; }

        [Required]
        public int DailyCost { get; set; }

        [Required]
        public int LateFee { get; set; }

        public string Picture { get; set; }

        //implict conversion to cartype object so i can use it in the entity
        public static implicit operator CarType(CarTypeValidation carType)
        {
            return new CarType()
            {
                model = carType.Model,
                manufacturer = carType.Manufacturer,
                year = carType.Year,
                ismanual = carType.IsManual,
                dailycost = carType.DailyCost,
                latefee = carType.LateFee,
                picture = carType.Picture
            };
        }
    }
}