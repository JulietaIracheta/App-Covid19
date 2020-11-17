using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebCovid19
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Necesidades",
                url: "Necesidades/{action}/{id}",
                defaults: new { controller = "Necesidades", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
              name: "DenunciaMonetaria",
              url: "DonacionMonetaria/{action}/{id}",
              defaults: new { controller = "DonacionMonetaria", action = "DonacionMonetaria", id = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Usuario", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
