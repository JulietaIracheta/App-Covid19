using DAO.Context;
using Entidades;
using Servicios;
using System;
using System.Web.Mvc;
using WebCovid19.Filters;

namespace WebCovid19.Controllers
{
    [LoginFilter]
    public class DenunciaController : Controller
    {
        ServicioDenuncia servicioDenuncia;
        ServicioNecesidad servicioNecesidad;

        public DenunciaController()
        {
            TpDBContext context = new TpDBContext();
            servicioDenuncia = new ServicioDenuncia(context);
            servicioNecesidad = new ServicioNecesidad(context);
        }

        public ActionResult Denunciar(int id)
        {
            ViewBag.motivosDenuncia = servicioDenuncia.ObtenerMotivosDenuncia();
            ViewBag.idNecesidad = id;
            return View();
        }


        [HttpPost]
        public ActionResult Denunciar(Denuncias denuncia)
        {
            int idUsuario = int.Parse(Session["UserId"].ToString());
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.motivosDenuncia = servicioDenuncia.ObtenerMotivosDenuncia();
                    ViewBag.idNecesidad = denuncia.IdNecesidad;
                    return View();
                }

                Denuncias denunciaRegistrada = servicioDenuncia.GuardarDenuncia(denuncia, idUsuario);
                servicioDenuncia.EvaluarCantidadDenunciasDeNecesidad(denuncia.IdNecesidad);
                if (denunciaRegistrada == null)
                {
                    ViewBag.mensajeError = "Ha ocurrido un error. Intente nuevamente por favor";
                    return View();
                }
                else
                {
                    ViewBag.mensajeCorrecto = "La denuncia se registró con éxito";
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }
            ViewBag.motivosDenuncia = servicioDenuncia.ObtenerMotivosDenuncia();
            ViewBag.idNecesidad = denuncia.IdNecesidad;
            return View(denuncia);
        }

    }
}