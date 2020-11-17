using Microsoft.Ajax.Utilities;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebCovid19.Filters
{
    public class ValidarPeticionFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string action = HttpContext.Current.Session["action"].ToString();
            string controller = HttpContext.Current.Session["controller"].ToString();
            string parametro = HttpContext.Current.Session["parametro"].ToString();
            var intParametro = 0;
            if (! string.IsNullOrEmpty(parametro))
            {
                 intParametro = int.Parse(parametro);
            }
            

            
            if (action != "")
            {

                if (action == "DetalleNecesidad")
                {
                    filterContext.Result = new RedirectToRouteResult
                        (
                            new RouteValueDictionary
                            {
                                {"Controller", controller },
                                {"Action", action},
                                { "idNecesidad", intParametro },
                            }
                        );
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult
                      (
                          new RouteValueDictionary
                          {
                                {"Controller", controller },
                                {"Action", action},
                          }
                      );

                }
            }


        }
    }
}



