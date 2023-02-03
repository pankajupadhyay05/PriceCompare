using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PriceCompare.Models
{
    public class Picture
    {
        public int Id { get; set; }

        [StringLength(70)]
        public string PictureUrl { get; set; }
    }
}
