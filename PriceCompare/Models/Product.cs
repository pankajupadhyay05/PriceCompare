using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PriceCompare.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Product Name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Short Desciption")]
        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Enter Full Description or copy and paste from above")]
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string FullDescription { get; set; }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string MetaKeywords { get; set; }
        
        public decimal? BasePrice { get; set; }

        public int CategoryId { get; set; }
        public int? BrandId { get; set; }
        public int PictureId { get; set; }
        public DateTime? lastFetch { get; set; }

        public virtual Category Category { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Picture Picture { get; set; }

        public virtual ICollection<Retailer_Product_Price> Retailer_Product_Prices { get; set; }
    }
}