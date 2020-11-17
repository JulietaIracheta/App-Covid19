using System;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebCovid19.Filters
{
    public class LoginFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (HttpContext.Current.Session["UserId"] as string == String.Empty)
            {
                string action = filterContext.RouteData.Values["action"].ToString();
                string controller = filterContext.RouteData.Values["controller"].ToString();
                string parametro = HttpContext.Current.Request.Params.Get("idNecesidad");

            

                HttpContext.Current.Session["action"] = action;
                HttpContext.Current.Session["controller"] = controller;

                if(parametro != null)
                {
                    HttpContext.Current.Session["parametro"] = parametro;
                }
               

              
                
                filterContext.Result = new RedirectToRouteResult

                    (
                        new RouteValueDictionary
                        {
                            {"Controller", "Usuario" },
                            {"Action", "Login" },
                            {"mensaje", $"Para acceder a {action} debe loguearse"}
                        }
                    );
            }
        }


    }
}
