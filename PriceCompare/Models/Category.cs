using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PriceCompare.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Category Name")]
        [StringLength(60)]
        public string Name { get; set; }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string MetaKeywords { get; set; }
        
        public int? ParentCategoryId { get; set; }
        public int? PictureId { get; set; }

        [StringLength(250)]
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }
        
        [ForeignKey("ParentCategoryId")]
        public virtual Category ParentCategory { get; set; }

        [InverseProperty("ParentCategory")]
        public virtual ICollection<Category> SubCategories { get; set; }

        public virtual Picture Picture { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}