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
    
    public partial class CarsForRent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarsForRent()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public string registrationplate { get; set; }
        public Nullable<int> cartype { get; set; }
        public double km { get; set; }
        public bool available { get; set; }
        public Nullable<int> branch { get; set; }
    
        public virtual Branch Branch1 { get; set; }
        public virtual CarType CarType1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
