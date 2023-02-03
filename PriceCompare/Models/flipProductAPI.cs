using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PriceCompare.Models
{
    public class flipProductAPI
    {
        public List<ProductListNames> ProductList(string url)
        {
            List<ProductListNames> objProducts = new List<ProductListNames>();

            try
            {
                var wc = new WebClient();
                wc.Headers.Add("Fk-Affiliate-Id", ConfigurationManager.AppSettings["FK-AFFID"]);
                wc.Headers.Add("Fk-Affiliate-Token", ConfigurationManager.AppSettings["FK-TKN"]);
                string productFeedXml = wc.DownloadString(url);

                JObject jObject = (JObject)JsonConvert.DeserializeObject(productFeedXml);
                string nextUrlString = (string)jObject["nextUrl"];
                
                var jProductData = jObject["productInfoList"];

                foreach (var item in jProductData)
                {
                    string strproductId, strtitle, strimageUrls, strmaximumRetailPrice, strsellingPrice, strcurrency, strproductBrand, strproductUrl, strinStock;

                    try { strproductId = item["productBaseInfo"]["productIdentifier"]["productId"].ToString(); }
                    catch { strproductId = ""; }
                    try { strtitle = item["productBaseInfo"]["productAttributes"]["title"].ToString(); }
                    catch { strtitle = ""; }
                    try { strimageUrls = item["productBaseInfo"]["productAttributes"]["imageUrls"]["125x125"].ToString(); }
                    catch { strimageUrls = ""; }
                    try { strmaximumRetailPrice = item["productBaseInfo"]["productAttributes"]["maximumRetailPrice"].ToString(); }
                    catch { strmaximumRetailPrice = ""; }
                    try { strsellingPrice = item["productBaseInfo"]["productAttributes"]["sellingPrice"].ToString(); }
                    catch { strsellingPrice = ""; }
                    try { strcurrency = item["productBaseInfo"]["productAttributes"]["currency"].ToString(); }
                    catch { strcurrency = ""; }
                    try { strproductBrand = item["productBaseInfo"]["productAttributes"]["productBrand"].ToString(); }
                    catch { strproductBrand = ""; }
                    try { strproductUrl = item["productBaseInfo"]["productAttributes"]["productUrl"].ToString(); }
                    catch { strproductUrl = ""; }
                    try { strinStock = item["productBaseInfo"]["productAttributes"]["inStock"].ToString(); }
                    catch { strinStock = ""; }

                    objProducts.Add(new ProductListNames
                    {
                        productId = strproductId,
                        title = strtitle,
                        imageUrls = strimageUrls,
                        maximumRetailPrice = strmaximumRetailPrice,
                        sellingPrice = strsellingPrice,
                        currency = strcurrency,
                        productBrand = strproductBrand,
                        productUrl = strproductUrl,
                        inStock = strinStock
                    });
                }

            }
            catch (Exception)
            {
                throw;
            }
            return objProducts;
        }


        public class ProductListNames
        {
            public string productId { get; set; }
            public string title { get; set; }
            public string imageUrls { get; set; }
            public string maximumRetailPrice { get; set; }
            public string sellingPrice { get; set; }
            public string currency { get; set; }
            public string productUrl { get; set; }
            public string productBrand { get; set; }
            public string inStock { get; set; }
            public string size { get; set; }
        }
    }
}