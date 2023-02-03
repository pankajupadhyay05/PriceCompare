using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PriceCompare.Models
{
    public class PriceCompareEntityInitializer : DropCreateDatabaseIfModelChanges<PriceCompareEntity>
    {
        protected override void Seed(PriceCompareEntity context)
        {
            var categories = new List<Category>
            {
                new Category { Name = "Computers" },
                new Category { Name = "Electronics" },
                new Category { Name = "Home and Kitchen" },
                new Category { Name = "Sports" },
                new Category { Name = "Mobile and Accessories", ParentCategoryId = 2 },
                new Category { Name = "Cameras", ParentCategoryId = 2 },
                new Category { Name = "Portable Audio and Video", ParentCategoryId = 2 },
                new Category { Name = "Home Entertainment", ParentCategoryId = 2 },
                new Category { Name = "Computer Hardware", ParentCategoryId = 1 },
                new Category { Name = "Desktops", ParentCategoryId = 1 },
                new Category { Name = "Tablets and Handhelds", ParentCategoryId = 1 },
                new Category { Name = "Laptops", ParentCategoryId = 1 },
                new Category { Name = "Softwares", ParentCategoryId = 1 },
                new Category { Name = "Printers", ParentCategoryId = 1 },
                new Category { Name = "Mixer and Grinder", ParentCategoryId = 3 },
                new Category { Name = "Cooking appliances", ParentCategoryId = 3 },
                new Category { Name = "Refrigerator", ParentCategoryId = 3 },
                new Category { Name = "Iron", ParentCategoryId = 3 },
                new Category { Name = "Vacuum cleaner", ParentCategoryId = 3 },
                new Category { Name = "Washing Machine", ParentCategoryId = 3 },
                new Category { Name = "Water Purifier", ParentCategoryId = 3 },
                new Category { Name = "Individual Sports", ParentCategoryId = 4 },
                new Category { Name = "Team Sports", ParentCategoryId = 4 },
                new Category { Name = "Fitness Equipment", ParentCategoryId = 4 }
            };
            categories.ForEach(c => context.Categories.Add(c));
            context.SaveChanges();

            var brands = new List<Brand>
            {
                new Brand { Name = "Lenovo" },
                new Brand { Name = "Toshiba" },
                new Brand { Name = "HP" },
                new Brand { Name = "Sony" },
                new Brand { Name = "Apple" },
                new Brand { Name = "Nokia" },
                new Brand { Name = "LG" },
                new Brand { Name = "Philips" },
                new Brand { Name = "Panasonic" },
                new Brand { Name = "Samsung" },
                new Brand { Name = "Micromax" },
                new Brand { Name = "Microsoft" }
            };
            brands.ForEach(b => context.Brands.Add(b));
            context.SaveChanges();

            var pictures = new List<Picture>
            {
                new Picture { PictureUrl = "/Content/Images/Retailers/retailer800000.png" },
                new Picture { PictureUrl = "/Content/Images/Retailers/retailer800001.gif" },
                new Picture { PictureUrl = "/Content/Images/Retailers/retailer800002.png" },
                new Picture { PictureUrl = "/Content/Images/Retailers/retailer800003.jpg" },
                new Picture { PictureUrl = "/Content/Images/Retailers/retailer800004.jpg" },
                new Picture { PictureUrl = "/Content/Images/Retailers/retailer800005.gif" },
                new Picture { PictureUrl = "/Content/Images/Retailers/retailer800006.jpg" }
            };
            pictures.ForEach(p => context.Pictures.Add(p));
            context.SaveChanges();

            var retailers = new List<Retailer>
            {
                new Retailer { Name = "Flipkart", PictureId = 1, Url = "http://www.flipkart.com" },
                new Retailer { Name = "Ebay.in", PictureId = 2, Url = "http://www.ebay.in" },
                new Retailer { Name = "HomeShop18", PictureId = 3, Url = "http://www.homeshop18.com" },
                new Retailer { Name = "Future Bazaar", PictureId = 4, Url = "http://www.futurebazaar.com" },
                new Retailer { Name = "SnapDeal", PictureId = 5, Url = "http://www.snapdeal.com" },
                new Retailer { Name = "India Plaza", PictureId = 6, Url = "http://www.indiaplaza.com" },
                new Retailer { Name = "Yebhi.com", PictureId = 7, Url = "http://www.yebhi.com" }
            };
            retailers.ForEach(r => context.Retailers.Add(r));
            context.SaveChanges();
        }
    }
}