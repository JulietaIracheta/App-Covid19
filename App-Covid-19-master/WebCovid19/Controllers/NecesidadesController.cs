using DAO.Context;
using Entidades;
using Entidades.Enum;
using Entidades.Metadata;
using Entidades.Views;
using Servicios;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebCovid19.Filters;
using WebCovid19.Utilities;

namespace WebCovid19.Controllers
{

    [LoginFilter]
    public class NecesidadesController : Controller
    {
        ServicioNecesidad servicioNecesidad;
        ServicioNecesidadValoraciones servicioNecesidadValoraciones;
        ServicioUsuario servicioUsuario;
        public NecesidadesController()
        {
            TpDBContext context = new TpDBContext();
            servicioNecesidad = new ServicioNecesidad(context);
            servicioNecesidadValoraciones = new ServicioNecesidadValoraciones(context);
            servicioUsuario = new ServicioUsuario(context);
        }

        // GET: Necesidades
        public ActionResult Index()
        {
            return View();
        }

        #region Creacion de Necesidad
        public ActionResult Crear()
        {
            int idUsuario = int.Parse(Session["UserId"].ToString());
            if (servicioUsuario.VerificarPerfilCompleto(idUsuario))
            {
                TempData["Mensaje"] = "Debe Completar su perfil para crear una necesidad.";
                return View("AvisosNecesidad");
            }else if (servicioNecesidad.TraerNecesidadesDelUsuario(idUsuario, "on").Count >= 3)
            {
                TempData["Mensaje"] = "Usted ya alcanzó el límite (3) de necesidades activas.";
                return View("AvisosNecesidad");
            }
            NecesidadesMetadata necesidadesMetadata = new NecesidadesMetadata();
            return View(necesidadesMetadata);
        }

        [HttpPost]
        public ActionResult Crear(NecesidadesMetadata necesidadMeta)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    string nombreSignificativo = necesidadMeta.Nombre + " " + Session["Email"];
                    //Guardar Imagen
                    string pathRelativoImagen = ImagenesUtil.Guardar(Request.Files[0], nombreSignificativo);
                    necesidadMeta.Foto = pathRelativoImagen;
                }
                int idUsuario = int.Parse(Session["UserId"].ToString());
                Necesidades necesidad = servicioNecesidad.buildNecesidad(necesidadMeta, idUsuario);
                Session["idNecesidad"] = necesidad.IdNecesidad;
                if (Enum.GetName(typeof(TipoDonacion), necesidadMeta.TipoDonacion) == "Insumos")
                {
                    return View("Insumos");
                }
                else
                {
                    return View("Monetaria");
                }
            }

        }

        [HttpGet]
        public ActionResult Insumos()
        {
            NecesidadesDonacionesInsumosMetadata insumos = new NecesidadesDonacionesInsumosMetadata();
            return View(insumos);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Insumos(NecesidadesDonacionesInsumosMetadata insumos)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            int idN = int.Parse(Session["idNecesidad"].ToString());
            insumos.Necesidades = servicioNecesidad.obtenerNecesidadPorId(idN);
            insumos.IdNecesidad = idN;
            servicioNecesidad.AgregarInsumos(insumos);
            TempData["Creada"] = "La necesidad se creó exitosamente.";
            TempData["Insumo"] = "SI";
            if (servicioNecesidad.ObtenerInsumosPorIdNecesidad(idN).Count<=1){
                return View("Referencias");
            }
            else
            {
                return View("AvisosNecesidad");
            }
                  
        }

        public ActionResult Monetaria()
        {
            NecesidadesDonacionesMonetariasMetadata monetaria = new NecesidadesDonacionesMonetariasMetadata();
            return View(monetaria);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Monetaria(NecesidadesDonacionesMonetariasMetadata monetarias)
        {
            monetarias.Dinero = decimal.Parse(monetarias.Dinero.ToString());
            if (!ModelState.IsValid)
            {
                return View();
            }
            int idN = int.Parse(Session["idNecesidad"].ToString());
            monetarias.Necesidades = servicioNecesidad.obtenerNecesidadPorId(idN);
            monetarias.IdNecesidad = idN;
            servicioNecesidad.AgregarMonetarias(monetarias);
            TempData["Creada"] = "La necesidad se creó exitosamente.";
            TempData["Monetaria"] = "SI";
            if (servicioNecesidad.ObtenerMonetariasPorIdNecesidad(idN).Count <= 1)
            {
                return View("Referencias");
            }
            else 
            {
                if(monetarias.Necesidades.NecesidadesReferencias.Count>0)
                {
                    servicioNecesidad.ActivarNecesidad(idN);
                }
                return View("AvisosNecesidad");
            }
        }

        public ActionResult Referencias()
        {
            VMReferencias vmReferencia = new VMReferencias();
            return View(vmReferencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Referencias(VMReferencias vmref)
        {
            if (!ModelState.IsValid)
            {
                return View();
            };
            if (vmref.Telefono1.Equals(vmref.Telefono2))
            {
                ViewBag.TelIguales = "Los números de teléfonos no pueden ser los mismos";
            };
            int idN = int.Parse(Session["idNecesidad"].ToString());
            vmref.Necesidades = servicioNecesidad.obtenerNecesidadPorId(idN);
            vmref.IdNecesidad = idN;
            servicioNecesidad.AgregarReferencias(vmref);
            servicioNecesidad.ActivarNecesidad(idN);
            TempData["Creada"] = "La necesidad se creó exitosamente.";
            return View("AvisosNecesidad");
        }
        #endregion

        #region Modificacion y Detalle
        [HttpGet]
        public ActionResult Modificar(int id)
        {
            Necesidades n = servicioNecesidad.obtenerNecesidadPorId(id);
            NecesidadesMetadata nm = servicioNecesidad.ConvertirNecesidadAMetadata(n);
            if (n == null || n.FechaFin <= DateTime.Now)
            {
                return RedirectToAction("Index","Usuario");
            }
            Session["idNecesidad"] = n.IdNecesidad;
            return View(nm);
        }
        [HttpPost]
        public ActionResult Modificar(NecesidadesMetadata nm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                string nombreSignificativo = nm.Nombre + " " + Session["Email"];
                //Guardar Imagen
                string pathRelativoImagen = ImagenesUtil.Guardar(Request.Files[0], nombreSignificativo);
                nm.Foto = pathRelativoImagen;
            }
            servicioNecesidad.EditarNecesidad(nm);
            return RedirectToAction("DetalleNecesidad", new { idNecesidad = nm.IdNecesidad });
        }
        //TODO: AGREGAR MODIFICACION DE REFERENCIAS
        [HttpGet]
        public ActionResult ModificarReferencias(int id)
        {
            List<NecesidadesReferencias> lista = servicioNecesidad.ObtenerReferenciasPorIdNecesidad(id);
            return View(lista);
        }
        [HttpPost]
        public ActionResult ModificarReferencias(NecesidadesReferencias r)
        {
            if (!servicioNecesidad.ModificarReferencia(r)) 
            {
                TempData["Error"] = "Los datos no son válidos";
            }
            return RedirectToAction("ModificarReferencias", r.IdNecesidad);
        }
        [HttpGet]
        public ActionResult EditarInsumos(int id)
        {
           List<NecesidadesDonacionesInsumosMetadata> lista = servicioNecesidad.ObtenerInsumosMetadataPorIdNecesidad(id);
            return View("ListadoInsumos", lista);
        }
        [HttpGet]
        public ActionResult EditarMonetarias(int id)
        {
            List<NecesidadesDonacionesMonetariasMetadata> lista = servicioNecesidad.ObtenerMonetariasMetadataPorIdNecesidad(id);
            return View("ListadoMonetarias", lista);
        }
        [HttpPost]
        public ActionResult EditarInsumos(NecesidadesDonacionesInsumosMetadata metaI)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Los Datos no son válidos";
                return View("EditarInsumos", metaI.IdNecesidad);
            }
            servicioNecesidad.EditarInsumo(metaI);
            return RedirectToAction("EditarInsumos", metaI.IdNecesidad);
        }
        [HttpPost]
        public ActionResult EditarMonetarias(NecesidadesDonacionesMonetariasMetadata metaM)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Los Datos no son válidos";
                return View("EditarMonetarias",metaM.IdNecesidad);
            }
            servicioNecesidad.EditarMonetaria(metaM);
            return RedirectToAction("EditarMonetarias", metaM.IdNecesidad);
        }

        #endregion
        [LoginFilter]//toDo: Probar que funcione bien del todo este action.
        public ActionResult DetalleNecesidad(int idNecesidad)
        {
            int idSession = int.Parse(Session["UserId"].ToString());
            /***************************** Like or Dislike *************************/
            /*Si recibe un Like or dislike desde la vista DetalleNecesidad viene para acá*/
            if (Request.Form["Like"] != null | (Request.Form["Dislike"] != null))
            {
                string boton = (Request.Form["Like"] != null) ? "Like" : (Request.Form["Dislike"] != null) ? "Dislike" : null;
                LikeOrDislike likeOrDislike = new LikeOrDislike();
                bool estado = likeOrDislike.AgregaLikeOrDislike(idSession, boton, idNecesidad, servicioNecesidadValoraciones);
                return RedirectToAction("DetalleNecesidad", new { idNecesidad });
            }
            /**********************************************************************/
            Necesidades necesidadObtenida = servicioNecesidad.obtenerNecesidadPorId(idNecesidad);
            return View(necesidadObtenida);
        }

        [LoginFilter]
        public ActionResult Home(string necesidad)
        {
            List<Necesidades> todasLasNecesidades;
            int idSession = int.Parse(Session["UserId"].ToString());
            if (!string.IsNullOrEmpty(Request["buscar"]))
            {
                ViewBag.ResultadoBusqueda = true;
                todasLasNecesidades = servicioNecesidad.Buscar(Request["buscar"]);
                if (todasLasNecesidades.Count == 0)
                {
                    ViewBag.ResultadoBusqueda = false;
                }
            }
            else
            {
                todasLasNecesidades = servicioNecesidad.TraerNecesidadesQueNoSonDelUsuario(idSession);
            }
            List<Necesidades> necesidadesDelUser = servicioNecesidad.TraerNecesidadesDelUsuario(idSession, necesidad);
            //Mantener el checkbox seleccionado o no, dependiendo lo que haya elegido
            TempData["estadoCheckbox"] = necesidad;

            ViewBag.necesidadesDelUser = necesidadesDelUser;
            return View(todasLasNecesidades);
        }

    }
}