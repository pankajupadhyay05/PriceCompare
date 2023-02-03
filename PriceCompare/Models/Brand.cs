using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PriceCompare.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter a Brand Name")]
        [StringLength(60)]
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}