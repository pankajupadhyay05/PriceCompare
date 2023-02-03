using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PriceCompare.Models
{
    public class Term
    {
        public int Id { get; set; }

        public string Query { get; set; }
    }
}
