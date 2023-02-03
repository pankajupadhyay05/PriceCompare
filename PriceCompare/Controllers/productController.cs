using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Mvc;
using PriceCompare.Models;
using PriceCompare.ViewModel;

namespace PriceCompare.Controllers
{
    public class productController : Controller
    {
        PriceCompareEntity pe = new PriceCompareEntity();
        //
        // GET: /Product/

        public ActionResult Index(int id, string title)
        {
            Product product = pe.Products.Where(p => p.Id == id).First();
            if (product.Name.Replace(", ", " ").Replace(" ", "-").ToLower() != title.Replace(", ", " ").Replace(" ", "-").ToLower())
            {
                return Redirect("/product/" + product.Id + "/" + product.Name.Replace(", ", " ").Replace(" ", "-").ToLower());
            }
            product.BasePrice = product.Retailer_Product_Prices.Min(p => p.Price);
            if (product.BasePrice == 0)
            {
                var prices = product.Retailer_Product_Prices.OrderBy(p=>p.Price).ToList();
                var next = prices.ElementAt(1);
                product.BasePrice = next.Price;
            }
            Rating rating = pe.Ratings.Where(r => r.ProductId == id).FirstOrDefault();
            if (rating != null)
            {
                ViewBag.RatingCount = rating.RatingCount;
                ViewBag.RatingValue = rating.RatingValue;
            }
            ViewBag.CategoryName = char.ToUpper(product.Category.Name[0]) + product.Category.Name.Substring(1);
            ViewBag.Title = product.Name + " Price in India as on " + DateTime.Today.ToLongDateString() + " along with reviews and ratings";
            ViewBag.Description = product.Retailer_Product_Prices.Count() + " Indian online Retailers offer " + product.Name + " with the Lowest Price @ " + product.BasePrice + " - Compare them on Pricepan now";
            ViewBag.LikeType = "og_pricepan:product";
            ViewBag.LikeTitle = product.Name;
            ViewBag.LikeUrl = "http://www.pricepan.com/Product/" + product.Id + "/" + product.Name.Replace(" ", "-");
            ViewBag.LikeImage = "http://www.pricepan.com" + product.Picture.PictureUrl;
            DateTime nowTime = DateTime.Now;
            int catInfoForInfi = product.CategoryId;
            if (product.lastFetch == null)
            {
                product = populatePricing(product, catInfoForInfi, rating);
                ViewBag.HighestPrice = product.Retailer_Product_Prices.Max(p => p.Price);
                return View(product);
            }
            else
            {
                DateTime lastFetch = product.lastFetch.GetValueOrDefault();
                TimeSpan t1 = nowTime.Subtract(lastFetch);
                int minimumCrawlPeriod = 2;
                if (catInfoForInfi == 12 || catInfoForInfi == 13)
                {
                    minimumCrawlPeriod = 10;
                }
                if (t1.Days > minimumCrawlPeriod)
                {
                    product = populatePricing(product, catInfoForInfi, rating);
                    ViewBag.HighestPrice = product.Retailer_Product_Prices.Max(p => p.Price);
                    return View(product);
                }
                else
                {
                    ViewBag.HighestPrice = product.Retailer_Product_Prices.Max(p => p.Price);
                    return View(product);
                }
            }
        }

        public Product populatePricing(Product product, int catInfoForInfi, Rating rating)
        {
            foreach (var pricing in product.Retailer_Product_Prices)
            {
                switch (pricing.RetailerId)
                {
                    case 2: // Flipkart //
                        flipkartWithRating flipWithRating = new flipkartWithRating();
                        flipWithRating = PriceCompareStatic.GetFlipKart(pricing.FetchUrl, "//span[@class='selling-price omniture-field']");
                        if (rating != null)
                        {
                            rating.RatingCount = flipWithRating.rating.RatingCount;
                            rating.RatingValue = flipWithRating.rating.RatingValue;
                            pe.Entry(rating).State = EntityState.Modified;
                        }
                        else
                        {
                            Rating ratingToAdd = new Rating();
                            ratingToAdd.ProductId = product.Id;
                            ratingToAdd.RatingCount = flipWithRating.rating.RatingCount;
                            ratingToAdd.RatingValue = flipWithRating.rating.RatingValue;
                            pe.Ratings.Add(ratingToAdd);
                        }
                        if (flipWithRating.pricing != null)
                        {
                            pricing.Price = flipWithRating.pricing;
                            pricing.Stock = "yes";
                        }
                        else
                        {
                            pricing.Stock = "no";
                        }
                        rating = flipWithRating.rating;
                        ViewBag.RatingCount = flipWithRating.rating.RatingCount;
                        ViewBag.RatingValue = flipWithRating.rating.RatingValue;
                        pe.Entry(pricing).State = EntityState.Modified;
                        break;
                    case 3: // HomeShop //
                        decimal? hmPrice = PriceCompareStatic.GetHomeShop(pricing.FetchUrl, "//span[@id='hs18Price']");
                        if (hmPrice != null)
                        {
                            pricing.Price = hmPrice;
                            pricing.Stock = "yes";
                        }
                        else
                        {
                            pricing.Stock = "no";
                        }
                        pe.Entry(pricing).State = EntityState.Modified;
                        break;
                    case 6: // SnapDeal //
                        decimal? sdPrice = PriceCompareStatic.GetSpanFromWebSite(pricing.FetchUrl, "//span[@itemprop='price']");
                        if (sdPrice != null)
                        {
                            pricing.Price = sdPrice;
                            pricing.Stock = "yes";
                        }
                        else
                        {
                            pricing.Stock = "no";
                        }
                        pe.Entry(pricing).State = EntityState.Modified;
                        break;
                    case 4: // InfiPrice //
                        decimal? inPrice = PriceCompareStatic.GetSpanFromInfi(pricing.FetchUrl, "//meta[@itemprop='price']");
                        if (inPrice != null)
                        {
                            pricing.Price = inPrice;
                            pricing.Stock = "yes";
                        }
                        else
                        {
                            pricing.Stock = "no";
                        }
                        pe.Entry(pricing).State = EntityState.Modified;
                        break;
                    case 9: // IndiaTimes //
                        decimal? ispPrice = PriceCompareStatic.GetIndiaTimes(pricing.FetchUrl);
                        if (ispPrice != null)
                        {
                            pricing.Price = ispPrice;
                            pricing.Stock = "yes";
                        }
                        else
                        {
                            pricing.Stock = "no";
                        }
                        pe.Entry(pricing).State = EntityState.Modified;
                        break;
                    case 10: // BookAdda //
                        decimal? bkPrice = PriceCompareStatic.GetBookAdda(pricing.FetchUrl, "//span[@itemprop='price']");
                        if (bkPrice != null)
                        {
                            pricing.Price = bkPrice;
                            pricing.Stock = "yes";
                        }
                        else
                        {
                            pricing.Stock = "no";
                        }
                        pe.Entry(pricing).State = EntityState.Modified;
                        break;
                    case 11: // URead //
                        pricing.Price = PriceCompareStatic.GetMashedUpCode(pricing.FetchUrl, "//label[@id='ctl00_phBody_ProductDetail_lblourPrice']");
                        pe.Entry(pricing).State = EntityState.Modified;
                        break;
                    case 12: // CrossWord //
                        decimal? cwPrice = PriceCompareStatic.GetMashedUpCode(pricing.FetchUrl, "//div[@class='final-price']");
                        if (cwPrice != null)
                        {
                            pricing.Price = cwPrice;
                            pricing.Stock = "yes";
                        }
                        else
                        {
                            pricing.Stock = "no";
                        }
                        pe.Entry(pricing).State = EntityState.Modified;
                        break;
                    case 13: // Landmarkonthenet //
                        decimal? lnPrice = PriceCompareStatic.GetMashedUpCode(pricing.FetchUrl, "//span[@class='price-current']");
                        if (lnPrice != null)
                        {
                            pricing.Price = lnPrice;
                            pricing.Stock = "yes";
                        }
                        else
                        {
                            pricing.Stock = "no";
                        }
                        pe.Entry(pricing).State = EntityState.Modified;
                        break;
                    case 14: // Amazon //
                        decimal? azPrice;
                        if (product.CategoryId == 12 || product.CategoryId == 13)
                        {
                            azPrice = PriceCompareStatic.GetMashedUpCode(pricing.FetchUrl, "//span[@class='a-size-medium a-color-price inlineBlock-display offer-price a-text-normal price3P']");
                        }
                        else
                        {
                            azPrice = PriceCompareStatic.GetMashedUpCode(pricing.FetchUrl, "//span[@id='priceblock_saleprice']");
                            if (azPrice == null)
                            {
                                azPrice = PriceCompareStatic.GetMashedUpCode(pricing.FetchUrl, "//span[@class='a-color-price']");
                                if (azPrice == null)
                                {
                                    azPrice = PriceCompareStatic.GetMashedUpCode(pricing.FetchUrl, "//span[@class='a-size-medium a-color-price']");
                                }
                            }
                        }
                        if (azPrice != null)
                        {
                            pricing.Price = azPrice;
                            pricing.Stock = "yes";
                        }
                        else
                        {
                            pricing.Stock = "no";
                        }
                        pe.Entry(pricing).State = EntityState.Modified;
                        break;
                    case 15: // Saholic //
                        decimal? shPrice = PriceCompareStatic.GetSpanFromWebSite(pricing.FetchUrl, "//span[@id='sp']");
                        if (shPrice != null)
                        {
                            pricing.Price = shPrice;
                            pricing.Stock = "yes";
                        }
                        else
                        {
                            pricing.Stock = "no";
                        }
                        pe.Entry(pricing).State = EntityState.Modified;
                        break;
                    case 16: // Croma //
                        decimal? crPrice = PriceCompareStatic.GetMashedUpCroma(pricing.FetchUrl, "//div[@class='cta']");
                        if (crPrice != null)
                        {
                            pricing.Price = crPrice;
                            pricing.Stock = "yes";
                        }
                        else
                        {
                            pricing.Stock = "no";
                        }
                        pe.Entry(pricing).State = EntityState.Modified;
                        break;
                    case 17: // The Mobile Store //
                        decimal? msPrice = PriceCompareStatic.GetSpanFromMS(pricing.FetchUrl, "//span[@itemprop='price']");
                        if (msPrice != null)
                        {
                            pricing.Price = msPrice;
                            pricing.Stock = "yes";
                        }
                        else
                        {
                            pricing.Stock = "no";
                        }
                        pe.Entry(pricing).State = EntityState.Modified;
                        break;
                    default:
                        break;
                }
                product.BasePrice = product.Retailer_Product_Prices.Min(p => p.Price);
                product.lastFetch = DateTime.Now;
                pe.Entry(product).State = EntityState.Modified;
                pe.SaveChanges();
            }
            return product;
        }

        [HttpPost]
        public void ClickCount(int retailerId)
        {
            Retailer retailer = pe.Retailers.Find(retailerId);
            if (retailer.ClickCount == null)
                retailer.ClickCount = 0;
            retailer.ClickCount++;
            pe.Entry(retailer).State = EntityState.Modified;
            pe.SaveChanges();
        }
    }
}