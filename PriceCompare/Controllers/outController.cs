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
    public class outController : Controller
    {
        PriceCompareEntity pe = new PriceCompareEntity();
        //
        // GET: /out/

        public ActionResult Index(int id)
        {
            var retailer = pe.Retailer_Product_Prices.Find(id);
            if (retailer.RetailerId == 3 || retailer.RetailerId == 9 || retailer.RetailerId == 16 || retailer.RetailerId == 17)
            {
                return Redirect(retailer.FetchUrl);
            }
            else
            {
                return Redirect(retailer.Url);
            }
        }
    }
}
