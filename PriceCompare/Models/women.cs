using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace PriceCompare.Models
{
    [Table("womens")]
    public class women
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter a Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Select a Brand")]
        public int? ClothingBrandId { get; set; }

        public string Type { get; set; }

        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Please Enter Picture Url")]
        public string Picture1 { get; set; }

        public string Picture2 { get; set; }

        public string Instock { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Please Select a Seller")]
        public int? RetailerId { get; set; }

        [Required(ErrorMessage = "Please Enter Product Page URL")]
        public string FetchUrl { get; set; }

        public virtual ClothingBrand ClothingBrand { get; set; }

        public virtual Retailer Retailer { get; set; }
    }
}