using DAO.Context;
using Entidades;
using Entidades.Views;
using Servicios;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebCovid19.Filters;

namespace WebCovid19.Controllers
{
    public class DonacionInsumosController : Controller
    {

        ServicioDonacionInsumo servicioDonacionInsumo;
        ServicioNecesidadesDonacionesInsumos servicioNecesidadesDonacionesInsumos;
        public DonacionInsumosController()
        {
            TpDBContext context = new TpDBContext();
            servicioDonacionInsumo = new ServicioDonacionInsumo(context);
            servicioNecesidadesDonacionesInsumos = new ServicioNecesidadesDonacionesInsumos(context);
        }

        [LoginFilter]
        public ActionResult DonacionInsumos(int idNecesidad)
        {
            NecesidadesDonacionesInsumos NdonacionesI = new NecesidadesDonacionesInsumos();
            NdonacionesI.IdNecesidad = idNecesidad;
            List<NecesidadesDonacionesInsumos> listaNombreInsumos = servicioNecesidadesDonacionesInsumos.ListaNombre(NdonacionesI);
            return View("DonacionInsumos", listaNombreInsumos);
        }

        [LoginFilter]
        [HttpGet]
        public ActionResult Donar(int idNecesidadDonacionInsumo)
        {
            DonacionesInsumos donacionesInsumos = new DonacionesInsumos();
            donacionesInsumos.IdNecesidadDonacionInsumo = idNecesidadDonacionInsumo;
            return View(donacionesInsumos);
        }

        [LoginFilter]
        [HttpPost]
        public ActionResult Donar(DonacionesInsumos donacionInsumos)
        {
            DonacionesInsumos donacionI = new DonacionesInsumos();
            NecesidadesDonacionesInsumos nec = new NecesidadesDonacionesInsumos();

            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    int idUsuario = int.Parse(Session["UserId"].ToString());
                    donacionI = servicioDonacionInsumo.GuardarCantidadDonada(donacionInsumos, idUsuario);
                    nec = servicioNecesidadesDonacionesInsumos.ObtenerNecesidadDonacionInsumosPorId(donacionInsumos.IdNecesidadDonacionInsumo);
                    TempData["Mensaje"] = "Gracias por su donación"; //Creo el TempData son el mensaje. Este TempData lo uso en la vista.
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }
            return RedirectToAction("DonacionInsumos", new { nec.IdNecesidad }); /*Aca le paso nec.IdNecesidad porque DonacionInsumos espera un Id. Si no se 
                                                                                                   lo paso, me va a tirar error 404*/
        }
    }
}