//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Order
    {
        public int Id { get; set; }
        public System.DateTime startdate { get; set; }
        public System.DateTime enddate { get; set; }
        public Nullable<System.DateTime> returndate { get; set; }
        public string carid { get; set; }
        public string userid { get; set; }
    
        public virtual CarsForRent CarsForRent { get; set; }
        public virtual User User { get; set; }
    }
}