using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;

namespace YoRent.Models
{
    //all the order info and the car info
    //if the car was deleted will only show the order
    public class FullOrderData 
    {
        public static FullOrderData Create(Order order)
        {
            return new FullOrderData(order);
        }

        public FullOrderData(Order order)
        {
            ID = order.Id;
            Start = order.startdate;
            End = order.enddate;
            ReturnDate = order.returndate;
            if (order.carid != null && order.CarsForRent.cartype != null)
            {
                DailyCost = order.CarsForRent.CarType1.dailycost;
                LateFee = order.CarsForRent.CarType1.latefee;
                Model = order.CarsForRent.CarType1.model;
                Manufacturer = order.CarsForRent.CarType1.manufacturer;
            }
            if (order.userid != null)
            {
                FullName = order.User.fullname;
            }
        }
        public int ID { get; set; }
        public string Model { get; private set; }
        public string Manufacturer { get; private set; }
        public string FullName { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int DailyCost { get; set; }
        public int LateFee { get; set; }
    }
}