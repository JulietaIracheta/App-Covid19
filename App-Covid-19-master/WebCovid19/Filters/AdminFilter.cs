using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebCovid19.Filters
{
    public class AdminFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Admin"].ToString() == string.Empty)
            {

                string action = filterContext.RouteData.Values["action"].ToString();
                filterContext.Result = new RedirectToRouteResult
                    (
                        new RouteValueDictionary
                        {
                            {"Controller", "Usuario" },
                            {"Action", "Index" },
                            {"mensaje", "Solo el usuario Administrador puede acceder a esta sección"}
                        }
                    );
            }

        }
    }
}