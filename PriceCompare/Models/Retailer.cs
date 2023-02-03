using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PriceCompare.Models
{
    public class Retailer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Retailer Name")]
        [StringLength(60)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Must Enter the Url")]
        [StringLength(80)]
        public string Url { get; set; }

        public int? PictureId { get; set; }
        public int? Rating { get; set; }
        public int? ClickCount { get; set; }

        public virtual Picture Picture { get; set; }
        public virtual ICollection<Retailer_Product_Price> Retailer_Product_Prices { get; set; }
    }
}