using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace PriceCompare.Models
{
    public class Application
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Date { get; set; }

        [Required(ErrorMessage = "Please Enter Tag: wp, ios or android")]
        [StringLength(50)]
        public string Tag { get; set; }

        [Required(ErrorMessage = "Please Enter Category Name")]
        [StringLength(80)]
        public string AppName { get; set; }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string ArticleName { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string ArticleContent { get; set; }

        [Required(ErrorMessage = "Please Enter Store Link")]
        [StringLength(400)]
        public string AppLink { get; set; }

        [Required(ErrorMessage = "Please Enter Meta Description")]
        [StringLength(400)]
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }

        [StringLength(400)]
        public string ArticleImage { get; set; }
    }
}