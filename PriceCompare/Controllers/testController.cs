using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using FileHelpers;
using PriceCompare.Models;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace PriceCompare.Controllers
{
    public class testController : Controller
    {
        PriceCompareEntity pe = new PriceCompareEntity();
        //
        // GET: /test/

        public ActionResult Index()
        {
            FileHelperEngine engine = new FileHelperEngine(typeof(tablets));

            // To Read Use:
            tablets[] res = engine.ReadFile(Server.MapPath("~/trial.csv")) as tablets[];

            return View(res);
        }

        public ActionResult Specs()
        {
            /*string url;
            string[] keys = new string[100];
            string[] values = new string[100];
            int i = 0;
            string identificationKeys = "//td[@class='specs-key']";
            string identificationValues = "//td[@class='specs-value fk-data']";
            url = "http://www.flipkart.com/xolo-a500s/p/itmdv6f4xmfrevzy?pid=MOBDNGR9GZXRGDB7&icmpid=reco_hp_personalBR_1&otracker=hp_mod__reco_prd_img";
            var baseUrl = new Uri(url);
            HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
            WebClient client = new WebClient();
            document.Load(client.OpenRead(baseUrl));
            var divKeys = document.DocumentNode.SelectNodes(identificationKeys);
            var divValues = document.DocumentNode.SelectNodes(identificationValues);
            foreach (var node in divKeys)
            {
                var keysVal = node.InnerHtml;
                keys[i] = keysVal;
                i++;
            }
            i = 0;
            foreach (var node in divValues)
            {
                var valuesVal = node.InnerHtml;
                values[i] = valuesVal;
                i++;
            }
            /*trial trialArray = new trial();
            trialArray.array = values;
            trialArray.key = keys;*/

            /*newMobile.Name = "Nokia Lumia 720";
            i = 0;
            foreach (var key in keys)
            {
                switch (key)
                {
                    case "OS":
                        newMobile.OS = values[i];
                        i++;
                        break;
                    case "Processor":
                        newMobile.Processor = values[i];
                        i++;
                        break;
                    case "Resolution":
                        newMobile.Resolution = values[i];
                        i++;
                        break;
                    default:
                        i++;
                        break;
                }
            }*/
            var mobilesAll = pe.Products.Where(p => p.CategoryId == 9).OrderByDescending(p => p.Id).ToList();
            foreach (var product in mobilesAll)
            {
                mobile mobile = new mobile();
                mobile.Name = product.Name;
                mobile.Brand = product.Brand.Name;
                mobile.Price = product.BasePrice;
                mobile.PictureUrl = product.Picture.PictureUrl;
                mobile.Description = product.FullDescription;
                mobile.ProductId = product.Id;
                string flipUrl = pe.Retailer_Product_Prices.Where(p => p.ProductId == product.Id & p.RetailerId == 2).Select(p => p.Url).FirstOrDefault();
                string url = flipUrl;
                if (url != null)
                {
                    string[] keys = new string[100];
                    string[] values = new string[100];
                    int i = 0;
                    string identificationKeys = "//td[@class='specsKey']";
                    string identificationValues = "//td[@class='specsValue']";
                    var baseUrl = new Uri(url);
                    HtmlAgilityPack.HtmlDocument document = new HtmlDocument();
                    WebClient client = new WebClient();
                    client.Headers.Add("user-agent", "Mozilla/5.0");
                    document.Load(client.OpenRead(baseUrl));
                    var divKeys = document.DocumentNode.SelectNodes(identificationKeys);
                    var divValues = document.DocumentNode.SelectNodes(identificationValues);
                    foreach (var node in divKeys)
                    {
                        var keysVal = node.InnerHtml;
                        keys[i] = keysVal;
                        i++;
                    }
                    i = 0;
                    foreach (var node in divValues)
                    {
                        var valuesVal = node.InnerHtml;
                        string repVal = valuesVal.Replace("\t", "");
                        values[i] = repVal.Replace("\n", "");
                        i++;
                    }
                    i = 0;
                    string[] keys21 = keys;
                    string[] val21 = values;
                    foreach (var key in keys21)
                    {
                        switch (key)
                        {
                            case "OS":
                                mobile.OS = val21[i];
                                i++;
                                break;
                            case "Processor":
                                mobile.Processor = val21[i];
                                i++;
                                break;
                            case "Size":
                                if (mobile.Display == null)
                                {
                                    mobile.Display = val21[i];
                                    if (mobile.Display.Contains("mm"))
                                    {
                                        mobile.Display = null;
                                    }
                                }
                                i++;
                                break;
                            case "Resolution":
                                mobile.Resolution = val21[i];
                                i++;
                                break;
                            case "Primary Camera":
                                mobile.PrimaryCam = val21[i];
                                i++;
                                break;
                            case "Secondary Camera":
                                mobile.SecondaryCam = val21[i];
                                i++;
                                break;
                            case "Video Recording":
                                mobile.Video = val21[i];
                                i++;
                                break;
                            case "Internal":
                                mobile.Internal = val21[i];
                                i++;
                                break;
                            case "Expandable Memory":
                                mobile.Expandable = val21[i];
                                i++;
                                break;
                            case "Memory":
                                mobile.RAM = val21[i];
                                i++;
                                break;
                            default:
                                i++;
                                break;
                        }
                    }
                    return View(mobile);
                }
                pe.mobiles.Add(mobile);
                pe.SaveChanges();
            }
            return View();
        }
    }
}
