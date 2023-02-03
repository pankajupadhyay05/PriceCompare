using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PriceCompare.Models
{
    public class mobile
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile Name")]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Brand { get; set; }

        [StringLength(20)]
        public string Display { get; set; }

        [StringLength(80)]
        public string Processor { get; set; }

        [StringLength(40)]
        public string RAM { get; set; }

        [StringLength(40)]
        public string Internal { get; set; }

        [StringLength(40)]
        public string Expandable { get; set; }

        [StringLength(90)]
        public string OS { get; set; }

        [StringLength(30)]
        public string PrimaryCam { get; set; }

        [StringLength(30)]
        public string SecondaryCam { get; set; }

        [StringLength(30)]
        public string Video { get; set; }

        public decimal? Price { get; set; }

        [StringLength(300)]
        public string PictureUrl { get; set; }

        [StringLength(1500)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Resolution { get; set; }

        public int ProductId { get; set; }


    }
}