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
    public class goController : Controller
    {
        PriceCompareEntity pe = new PriceCompareEntity();
        //
        // GET: /out/

        public ActionResult Index(int id)
        {
            var retailer = pe.womens.Find(id);
            if (retailer.RetailerId == 18 || retailer.RetailerId == 20)
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
