using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using System.ComponentModel.DataAnnotations;
using MvcUtilitys;

namespace YoRent.Models
{
    //search object the get sent to the database 
    public class SearchBy:ISearch
    {
        //take 4 cars from search result 
        public const int TAKE = 4;
        public int Take { get { return TAKE; } }
        public int Skip { get; set; }

        [Required]
        public int Branch { get; set; }

        public int? Year { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public bool? IsManual { get; set; }

        [Required]
        public DateTime Start { get { return start; } set { start = value; } }
        DateTime start = DateTime.Now;

        [Required]
        public DateTime End { get { return end; } set { end = value; } }
        DateTime end= DateTime.Now;

        [ValidateDate(FutureOrder =true)]
        public RentTime Rent
        {
            get
            {
                return new RentTime() { Start = start, End = end };
            }
        }
    }
}