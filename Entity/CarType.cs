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
    
    public partial class CarType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CarType()
        {
            this.CarsForRents = new HashSet<CarsForRent>();
        }
    
        public int id { get; set; }
        public string manufacturer { get; set; }
        public string model { get; set; }
        public int year { get; set; }
        public bool ismanual { get; set; }
        public int dailycost { get; set; }
        public int latefee { get; set; }
        public string picture { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CarsForRent> CarsForRents { get; set; }
    }
}