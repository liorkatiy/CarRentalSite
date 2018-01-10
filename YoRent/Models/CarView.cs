using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;

namespace YoRent.Models
{
    //the object that been display for the user 
    //needed the registrationplate for the primarykey
    //so car can be added as foreign key to the new order
    public class CarView:CarType
    {
        public string registrationPlate { get; set; }

        public static CarView Create(CarsForRent car)
        {
            return new CarView()
            {
                picture = car.CarType1.picture,
                ismanual = car.CarType1.ismanual,
                model = car.CarType1.model,
                manufacturer = car.CarType1.manufacturer,
                year = car.CarType1.year,
                dailycost = car.CarType1.dailycost,
                latefee = car.CarType1.latefee,
                registrationPlate = car.registrationplate
            };
        }
    }
}