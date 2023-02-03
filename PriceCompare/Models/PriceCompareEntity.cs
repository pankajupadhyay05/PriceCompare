using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PriceCompare.Models
{
    public class PriceCompareEntity : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Retailer> Retailers { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Retailer_Product_Price> Retailer_Product_Prices { get; set; }
        public DbSet<NewsAndReview> NewsAndReviews { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<mobile> mobiles { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<men> mens { get; set; }
        public DbSet<women> womens { get; set; }
        public DbSet<ClothingBrand> ClothingBrands { get; set; }
    }
}