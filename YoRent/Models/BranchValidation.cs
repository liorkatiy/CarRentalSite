using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace YoRent.Models
{
    public class BranchValidation
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Longitude { get; set; }
        [Required]
        public double Latitude { get; set; }

        //implict conversion to branch object so i can use it in the entity
        public static implicit operator Branch(BranchValidation branchValidation)
        {
            return new Branch()
            {
                Id = branchValidation.ID,
                longitude = branchValidation.Longitude,
                latitude = branchValidation.Latitude,
                name = branchValidation.Name
            };
        }

    }
}