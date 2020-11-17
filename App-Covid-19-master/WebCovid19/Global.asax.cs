using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebCovid19.App_Start;

namespace WebCovid19
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ValidacionesGenerales.VerificarFechaFinDeNecesidades();
        }
        
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();

            HttpException httpException = exception as HttpException;

            int error = httpException != null ? httpException.GetHttpCode() : 0;

            Server.ClearError();
            Response.Redirect(String.Format("~/Error/?error={0}", error, exception.Message));
        }

         protected void Session_Start(Object sender, EventArgs e)
        { 
            Session["UserId"] = String.Empty;
            Session["Admin"] = String.Empty;
            Session["action"] = String.Empty;
            Session["controller"] = String.Empty;
            Session["parametro"] = String.Empty;
        }
    }
}
