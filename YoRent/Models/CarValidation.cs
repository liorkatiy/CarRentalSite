using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel.DataAnnotations;

namespace YoRent.Models
{
    public class CarValidation
    {
        [Required]
        public double KM { get; set; }

        [Required]
        public int? CarType { get; set; }
        
        public bool Available { get; set; }

        [Required]
        public string RegistrationPlate { get; set; }

        [Required]
        public int? Branch { get; set; }

        //implict conversion to CarsForRent object so i can use it in the entity
        public static implicit operator CarsForRent(CarValidation carValidation)
        {
            return new CarsForRent()
            {
                km=carValidation.KM,
                branch=carValidation.Branch,
                registrationplate=carValidation.RegistrationPlate,
                cartype=carValidation.CarType,
                available=carValidation.Available
            };
        }
    }
}