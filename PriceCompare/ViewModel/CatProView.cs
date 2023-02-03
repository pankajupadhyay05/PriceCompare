using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PriceCompare.Models;

namespace PriceCompare.ViewModel
{
    public class CatProView
    {
        public string Name;
        public ICollection<Category> SubCategories { get; set; }
        public PagedList.IPagedList<Product> PagedProducts { get; set; }
    }
}