using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PriceCompare.Models
{
    public class CategoryListModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string RootId { get; set; }
        public string RootName { get; set; }
    }
}