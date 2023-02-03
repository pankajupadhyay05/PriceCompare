using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Text.RegularExpressions;

namespace PriceCompare
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "ProductRoute", // Route name
                "product/{id}/{title}", // URL with parameters
                new { controller = "product", action = "Index", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "OutRoute", // Route name
                "out/{title}/{id}", // URL with parameters
                new { controller = "out", action = "Index", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "RefRoute", // Route name
                "refer/{title}/{id}", // URL with parameters
                new { controller = "refer", action = "Index", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "GoRoute", // Route name
                "go/{title}/{id}", // URL with parameters
                new { controller = "go", action = "Index", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "ClothRoute", // Cloth Route name
                "wardrobe/item/{id}/{title}", // URL with parameters
                new { controller = "wardrobe", action = "item", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "ClothProRoute", // Cloth Route name
                "wardrobe/product/{id}/{title}", // URL with parameters
                new { controller = "wardrobe", action = "product", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "StoryRoute", // Route name
                "news/{id}/{title}", // URL with parameters
                new { controller = "news", action = "story", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "AppRoute", // Route name
                "apps/{id}/{title}", // URL with parameters
                new { controller = "apps", action = "article", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "MobilePriceRoute", // Route name
                "mobiles/price", // URL with parameters
                new { controller = "mobiles", action = "price", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "MobileRoute", // Route name
                "mobiles/{title}", // URL with parameters
                new { controller = "mobiles", action = "brand", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "LaptopPriceRoute", // Route name
                "laptops/price", // URL with parameters
                new { controller = "laptops", action = "price", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "LaptopRoute", // Route name
                "laptops/{title}", // URL with parameters
                new { controller = "laptops", action = "brand", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "TabletPriceRoute", // Route name
                "tablets/price", // URL with parameters
                new { controller = "tablets", action = "price", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "TabletRoute", // Route name
                "tablets/{title}", // URL with parameters
                new { controller = "tablets", action = "brand", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "TelevisionPriceRoute", // Route name
                "televisions/price", // URL with parameters
                new { controller = "televisions", action = "price", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "TelevisionRoute", // Route name
                "televisions/{title}", // URL with parameters
                new { controller = "televisions", action = "brand", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "BooksRoute", // Route name
                "books/{title}", // URL with parameters
                new { controller = "books", action = "genre", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "AccessoryRoute", // Route name
                "accessories/{title}", // URL with parameters
                new { controller = "accessories", action = "brand", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "SearchRoute", // Route name
                "search", // URL with parameters
                new { controller = "home", action = "search", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "home", action = "Index", id = UrlParameter.Optional }, // Parameters defaults
                new[] { "PriceCompare.Controllers" } 
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_PreSendRequestHeaders()
        {
            /*Response.Headers.Remove("Server");
            Response.Headers.Remove("X-AspNet-Version");
            Response.Headers.Remove("X-AspNetMvc-Version");*/
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            /*Mobile request detection and Redirection*/ 
 
            string u = Request.ServerVariables["HTTP_USER_AGENT"];
            string path = Request.Url.PathAndQuery;
            Regex b = new Regex(@"android.+mobile|avantgo|bada\\/|blackberry|BB10|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\\/|plucker|pocket|psp|symbian|treo|up\\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            Regex v = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\\-(n|u)|c55\\/|capi|ccwa|cdm\\-|cell|chtm|cldc|cmd\\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\\-s|devi|dica|dmob|do(c|p)o|ds(12|\\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\\-|_)|g1 u|g560|gene|gf\\-5|g\\-mo|go(\\.w|od)|gr(ad|un)|haie|hcit|hd\\-(m|p|t)|hei\\-|hi(pt|ta)|hp( i|ip)|hs\\-c|ht(c(\\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\\-(20|go|ma)|i230|iac( |\\-|\\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\\/)|klon|kpt |kwc\\-|kyo(c|k)|le(no|xi)|lg( g|\\/(k|l|u)|50|54|e\\-|e\\/|\\-[a-w])|libw|lynx|m1\\-w|m3ga|m50\\/|ma(te|ui|xo)|mc(01|21|ca)|m\\-cr|me(di|rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\\-2|po(ck|rt|se)|prox|psio|pt\\-g|qa\\-a|qc(07|12|21|32|60|\\-[2-7]|i\\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\\-|oo|p\\-)|sdk\\/|se(c(\\-|0|1)|47|mc|nd|ri)|sgh\\-|shar|sie(\\-|m)|sk\\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\\-|v\\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\\-|tdg\\-|tel(i|m)|tim\\-|t\\-mo|to(pl|sh)|ts(70|m\\-|m3|m5)|tx\\-9|up(\\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|xda(\\-|2|g)|yas\\-|your|zeto|zte\\-", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if ((b.IsMatch(u) || v.IsMatch(u.Substring(0, 4))))
            {
                if(!path.Contains("Images") && !path.Contains("news-images"))
                {
                    string redirectTo = "http://m.pricepan.com"+path;
                    Response.Redirect(redirectTo);
                }
            }

            /* we guess at this point session is not already retrieved by application so we recreate cookie with the session id... */
            try
            {
                string session_param_name = "ASPSESSID";
                string session_cookie_name = "ASP.NET_SessionId";

                if (HttpContext.Current.Request.Form[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
                }
            }
            catch
            {
            }

            try
            {
                string auth_param_name = "AUTHID";
                string auth_cookie_name = FormsAuthentication.FormsCookieName;

                if (HttpContext.Current.Request.Form[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
                }

            }
            catch
            {
            }
        }

        private void UpdateCookie(string cookie_name, string cookie_value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
            if (null == cookie)
            {
                cookie = new HttpCookie(cookie_name);
            }
            cookie.Value = cookie_value;
            HttpContext.Current.Request.Cookies.Set(cookie);
        }
    }
}