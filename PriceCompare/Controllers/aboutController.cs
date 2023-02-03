using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PriceCompare.Controllers
{
    public class aboutController : Controller
    {
        //
        // GET: /About/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult terms()
        {
            return View();
        }
    }
}
