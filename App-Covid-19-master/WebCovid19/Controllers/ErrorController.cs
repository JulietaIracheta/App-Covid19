using System.Web.Mvc;

namespace WebCovid19.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(int error = 0)
        {

            switch (error)
            {
                case 505:
                    ViewBag.Title = "Ocurrio un error inesperado";
                    ViewBag.Description = "Disculpe las molestias, esperamos esto no vuelva a suceder :( ...";
                    break;

                case 404:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "La URL que está intentando ingresar no existe";
                    break;

                default:
                    ViewBag.Title = "Página no encontrada";
                    ViewBag.Description = "Algo salio muy mal :( ...";
                    break;
            }

            return View();
        }
    }
}