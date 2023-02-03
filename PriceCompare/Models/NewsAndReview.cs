using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PriceCompare.Models
{
    public class NewsAndReview
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter a Title")]
        [StringLength(400)]
        [DataType(DataType.MultilineText)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Enter a Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [StringLength(400)]
        [DataType(DataType.MultilineText)]
        public string MetaKeyword { get; set; }

        [StringLength(400)]
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }
    }
}