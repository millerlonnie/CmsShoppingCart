using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CmsShoppingCart
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {



            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            

            routes.MapRoute("Default", "", new { Controller = "Pages", action = "Index" }, new[] { "CmsShoppingCart.Controllers" });
            routes.MapRoute("Cart", "Cart/{action}/{id}", new { controller = "Cart", action = "Index", id = UrlParameter.Optional }, new[] { "CmsShoppingCart.Controllers" });

            routes.MapRoute("Shop", "Shop/{action}/{name}", new { controller = "Shop", action = "Index", name = UrlParameter.Optional }, new[] { "CmsShoppingCart.Controllers" });

            routes.MapRoute("_SidebarPartial", "Pages/_SidebarPartial", new { controller = "Pages", action = "_SidebarPartial" }, new[] { "CmsShoppingCart.Controllers" });
            routes.MapRoute("_PagesMenuPartial", "Pages/_PagesMenuPartial", new { controller = "Pages", action = "_PagesMenuPartial" }, new[] { "CmsShoppingCart.Controllers" });
            routes.MapRoute("Pages", "{page}", new { controller = "Pages", action = "Index" }, new[] { "CmsShoppingCart.Controllers" });
            //routes.MapRoute("Default", "", new { controller = "Pages", action = "Index" }, new[] { "CmsShoppingCart.Controllers" });
            //                            someaction  some category
           // someaction some category

            ////////////


            //above we "" -> means home , register Pages contoller, new[]{CmsShoppingCart.Controllers"} -->used if we have areas

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
