using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PriceCompare.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string RatingValue { get; set; }
        public string RatingCount { get; set; }

        public int? ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}