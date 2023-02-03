using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using PriceCompare.Models;
using PriceCompare.ViewModel;

namespace PriceCompare.Models
{
    public static class PriceCompareStatic
    {
        public static IEnumerable<GroupedSelectListItem> groupList(PriceCompareEntity db)
        {
            List<Category> orderedList = new List<Category>();
            var rootList = db.Categories.Where(c => c.ParentCategoryId == null).ToList();
            foreach (var item in rootList)
            {
                orderedList.Add(item);
                if (item.SubCategories.Count != 0)
                {
                    foreach (var subcat in item.SubCategories)
                    {
                        orderedList.Add(subcat);
                        if (subcat.SubCategories.Count != 0)
                        {
                            foreach (var subsubcat in subcat.SubCategories)
                            {
                                orderedList.Add(subsubcat);
                            }
                        }
                    }
                }
            }
            IEnumerable<GroupedSelectListItem> groupList = orderedList.Select(p => new GroupedSelectListItem()
            {
                GroupKey = p.ParentCategory != null ? p.ParentCategory.Name : null,
                GroupName = p.ParentCategory != null ? p.ParentCategory.Name : null,
                Text = p.Name,
                Value = p.Id.ToString()
            });
            return groupList;
        }

        public static flipkartWithRating GetFlipKart(string url, string identification)
        {
            var baseUrl = new Uri(url);
            flipkartWithRating flipWithRate = new flipkartWithRating();
            Rating ratingNew = new Rating();
            flipWithRate.rating = ratingNew;
            HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/5.0");
                document.Load(client.OpenRead(baseUrl));
                HtmlNode pricing = document.DocumentNode.SelectNodes(identification).FirstOrDefault();
                string attPricing = pricing.InnerHtml.Replace(",", "");
                string modPrice = attPricing.Replace("Rs. ", "");
                flipWithRate.pricing = Convert.ToDecimal(modPrice);
                string identification1 = "//div[@class='bigStar']";
                string identification2 = "//ul[@class='fk-give-star']";
                HtmlNode ratingCount = document.DocumentNode.SelectNodes(identification2).FirstOrDefault();
                HtmlAttribute attRatingCount = ratingCount.Attributes["data-rating-count"];
                flipWithRate.rating.RatingCount = attRatingCount.Value;
                HtmlNode ratingValue = document.DocumentNode.SelectNodes(identification1).FirstOrDefault();
                flipWithRate.rating.RatingValue = ratingValue.InnerHtml;
                
                return flipWithRate;
            }
            catch (Exception)
            {
                flipWithRate.rating.RatingCount = "0";
                flipWithRate.rating.RatingValue = "0";
                return flipWithRate;
            }
        }

        public static flipkartWithRating GetFlipKartSpan(string url, string identification)
        {
            var baseUrl = new Uri(url);
            flipkartWithRating flipWithRate = new flipkartWithRating();
            Rating ratingNew = new Rating();
            flipWithRate.rating = ratingNew;
            HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/5.0");
                document.Load(client.OpenRead(baseUrl));
                HtmlNode pricing = document.DocumentNode.SelectNodes(identification).FirstOrDefault();
                string attPricing = pricing.InnerHtml.Replace(",","");
                string modPrice = attPricing.Replace("Rs. ", "");
                flipWithRate.pricing = Convert.ToDecimal(modPrice);
                flipWithRate.rating.RatingValue = "0";
                flipWithRate.rating.RatingCount = "0";

                return flipWithRate;
            }
            catch (Exception)
            {
                flipWithRate.pricing = null;
                flipWithRate.rating.RatingCount = "0";
                flipWithRate.rating.RatingValue = "0";
                return flipWithRate;
            }
        }

        public static decimal? GetIndiaTimes(string url)
        {
            var baseUrl = new Uri(url);
            HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
            try
            {
                WebClient client = new WebClient();
                document.Load(client.OpenRead(baseUrl));
                var div = document.DocumentNode.SelectNodes("//meta[@property='og:price:amount']").FirstOrDefault();
                HtmlAttribute att = div.Attributes["content"];
                return Convert.ToDecimal(att.Value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static decimal? GetSpanFromWebSite(string url, string identification)
        {
            var baseUrl = new Uri(url);
            HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/5.0");
                document.Load(client.OpenRead(baseUrl));
                var div = document.DocumentNode.SelectNodes(identification).FirstOrDefault();
                return Convert.ToDecimal(div.InnerHtml.Replace(",",""));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static decimal? GetSpanFromMS(string url, string identification)
        {
            var baseUrl = new Uri(url);
            HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/5.0");
                document.Load(client.OpenRead(baseUrl));
                var div = document.DocumentNode.SelectNodes(identification).FirstOrDefault();
                string try22 = div.InnerHtml.Replace("Rs.", "");
                return Convert.ToDecimal(try22.Replace(",",""));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static decimal? GetSpanFromInfi(string url, string identification)
        {
            var baseUrl = new Uri(url);
            HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
            try
            {
                WebClient client = new WebClient();
                document.Load(client.OpenRead(baseUrl));
                var div = document.DocumentNode.SelectNodes(identification).FirstOrDefault();
                HtmlAttribute divPricing = div.Attributes["content"];
                string modPrice = divPricing.Value.Replace(",", "");
                return Convert.ToDecimal(modPrice);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static decimal? GetHomeShop(string url, string identification)
        {
            var baseUrl = new Uri(url);
            HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
            try
            {
                WebClient client = new WebClient();
                document.Load(client.OpenRead(baseUrl));
                var div = document.DocumentNode.SelectNodes(identification).FirstOrDefault();
                string try21 = div.InnerHtml.Replace("&#x20B9", "nil");
                var result = Regex.Match(try21, @"\d+").Value;
                //string modPrice = result.Replace(",", "");
                return Convert.ToDecimal(result);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static decimal? GetBookAdda(string url, string identification)
        {
            var baseUrl = new Uri(url);
            HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
            try
            {
                WebClient client = new WebClient();
                document.Load(client.OpenRead(baseUrl));
                var div = document.DocumentNode.SelectNodes(identification).FirstOrDefault();
                string modPrice = div.InnerHtml.Replace("Rs.", "");
                return Convert.ToDecimal(modPrice);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static decimal? GetMashedUpCode(string url, string identification)
        {
            var baseUrl = new Uri(url);
            HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/5.0");
                document.Load(client.OpenRead(baseUrl));
                var div = document.DocumentNode.SelectNodes(identification).FirstOrDefault();
                string try21 = div.InnerHtml.Replace("&#8377;", "nil");
                string try22 = try21.Replace(",", "");
                var result = Regex.Match(try22, @"\d+").Value;
                //string modPrice = result.Replace(",", "");
                return Convert.ToDecimal(result);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static decimal? GetMashedUpCroma(string url, string identification)
        {
            var baseUrl = new Uri(url);
            HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
            try
            {
                WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Mozilla/5.0");
                document.Load(client.OpenRead(baseUrl));
                var div = document.DocumentNode.SelectNodes(identification).FirstOrDefault();
                var divH = div.SelectNodes("//h2").FirstOrDefault();
                string try22 = divH.InnerHtml.Replace(",", "");
                var result = try22.Replace("Rs. ", "");
                //string modPrice = result.Replace(",", "");
                return Convert.ToDecimal(result.Replace(" ", ""));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}