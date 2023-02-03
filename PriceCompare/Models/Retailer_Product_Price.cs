using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PriceCompare.Models
{
    [Table("Retailer_Product_Prices")]
    public class Retailer_Product_Price
    {
        public int Id { get; set; }

        public decimal? Price { get; set; }
        [StringLength(600)]
        public string Url { get; set; }
        
        public int ProductId { get; set; }
        public int RetailerId { get; set; }

        [StringLength(400)]
        public string FetchUrl { get; set; }

        [StringLength(100)]
        public string Offer { get; set; }

        [StringLength(3)]
        public string Stock { get; set; }

        public virtual Product Product { get; set; }
        public virtual Retailer Retailer { get; set; }
    }
}