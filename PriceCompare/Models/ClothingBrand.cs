using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PriceCompare.Models
{
    [Table("ClothingBrands")]
    public class ClothingBrand
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter a Brand Name")]
        [StringLength(60)]
        public string Name { get; set; }

        public virtual ICollection<men> mens { get; set; }
        public virtual ICollection<women> womens { get; set; }
    }
}