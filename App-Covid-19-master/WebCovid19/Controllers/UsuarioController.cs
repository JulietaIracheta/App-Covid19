using DAO.Context;
using Entidades;
using Entidades.Enum;
using Entidades.Views;
using Newtonsoft.Json;
using Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Mvc;
using WebCovid19.Filters;
using WebCovid19.Utilities;

namespace WebCovid19.Controllers
{
    public class UsuarioController : Controller
    {
        ServicioUsuario servicioUsuario;
        ServicioNecesidadValoraciones servNecesidadValoraciones;
        ServicioNecesidad servicioNecesidad;
        ServicioDenuncia servicioDenuncia;
        ServicioNecesidadValoraciones servicioValoraciones;
        public UsuarioController()
        {
            TpDBContext context = new TpDBContext();
            servNecesidadValoraciones = new ServicioNecesidadValoraciones(context);
            servicioNecesidad = new ServicioNecesidad(context);
            servicioDenuncia = new ServicioDenuncia(context);
            servicioUsuario = new ServicioUsuario(context);
            servicioValoraciones = new ServicioNecesidadValoraciones(context);
        }

        public ActionResult Index()
        {
            List<Necesidades> listaNecesidades = servicioNecesidad.obtener5NecesidadesMasValoradas();
            return View(listaNecesidades);
        }



        public ActionResult Salir()
        {
            servicioUsuario.CerrarSession();
            return RedirectToAction("Index");
        }

        public ActionResult Registro()
        {
            VMRegistro registro = new VMRegistro();
            return View(registro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registro(VMRegistro registro)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                Usuarios usuario = new Usuarios();

                //Asigno datos obtenidos del formulario a usuario
                usuario = servicioUsuario.asignoDatosAUsuarioDelRegistro(registro);

                //Validar si el email es un email nuevo o si ya fue registrado
                TipoEmail emailIngresado = servicioUsuario.ValidoEstadoEmail(usuario);

                //Esta condicion es por si se le envie la activacion, elimina el mensaje, y quiere recuperar su activacion.
                if (emailIngresado == TipoEmail.EmailNuevo)
                {
                    if (servicioUsuario.registrarUsuario(usuario) >= 0)
                    {
                        ViewData.Add("mensajeAdvertencia", "Te hemos enviado un email por Gmail con su clave de activación");

                        string mensajeEnviado = servicioUsuario.EnviarCodigoPorEmail(usuario);

                        if (mensajeEnviado != "Ok")
                        {
                            ViewData.Add("mensajeError", mensajeEnviado);
                        }
                    }
                    else
                    {
                        ViewData.Add("mensajeError", "Ha ocurrido un error al registrarse, por favor intentelo nuevamente");
                    }
                }
                else
                {
                    ViewData.Add("mensajeError", "Ya existe una cuenta con ese email");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            return View();
        }

        public ActionResult ReenvioDeCodigo()
        {
            VMReenvioDeEmail email = new VMReenvioDeEmail();
            return View(email);
        }

        [HttpPost]
        public ActionResult ReenvioDeCodigo(VMReenvioDeEmail emailRecibido)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {

                Usuarios usuarioObtenido = new Usuarios();
                usuarioObtenido.Email = emailRecibido.Email;

                //Validar si el email es un email nuevo o si ya fue registrado
                TipoEmail emailIngresado = servicioUsuario.ValidoEstadoEmail(usuarioObtenido);

                if (emailIngresado == TipoEmail.EmailSolicitado)
                {
                    //Se le envia nuevamente su token al usuario ya registrado
                    string mensajeEnviado = servicioUsuario.ReenviarEmail(usuarioObtenido);

                    if (servicioUsuario.ReenviarEmail(usuarioObtenido) != "Ok")
                    {
                        ViewData.Add("mensajeError", mensajeEnviado);
                    }
                    else
                    {
                        ViewData.Add("mensajeAdvertencia", "Te hemos enviado nuevamente un email por Gmail con su clave de activación");
                    }
                }
                else if (emailIngresado == TipoEmail.EmailNuevo)
                {
                    //Aun no se registro
                    ViewData.Add("mensajeAdvertencia", "Todavia no se ha registrado un usuario con ese email");
                }
                else
                {   //Usuario ya activo
                    ViewData.Add("mensajeError", "Ya existe una cuenta activa con ese email");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            VMLogin login = new VMLogin();
            return View(login);
        }


        [HttpPost]
        public ActionResult Login(VMLogin login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                Usuarios usuario = new Usuarios();

                //Asigno datos obtenidos del formulario a usuario
                usuario = servicioUsuario.asignoDatosAUsuarioDelLogin(login);

                //Validar si existe este usuario         
                string usuarioExistente = servicioUsuario.validoQueExistaEsteUsuario(usuario);
                if (usuarioExistente == null)
                {
                    ViewData.Add("mensajeError", "No existe ese email, debera registrarse primero");
                    return View();
                }
                else if (usuarioExistente == "incorrecto")
                {
                    ViewData.Add("mensajeError", "La contraseña ha sido incorrecta");
                    return View();
                }
                else if (usuarioExistente == "ok")
                {
                    //Validar si esta activo o no
                    TipoEmail estadoEmail = servicioUsuario.ValidoEstadoEmail(usuario);
                    if (estadoEmail != TipoEmail.EmailActivo)
                    {
                        ViewData.Add("mensajeAdvertencia", "Su usuario está inactivo. Actívelo desde el email recibido");
                        return View();
                    }

                    //seteo de session
                    servicioUsuario.SetearSession(usuario);
                    //Lo lleva a la vista a donde queria ir
                    return RedirectToAction("AsignarRuta", usuario);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }
            return View();
        }


        [LoginFilter]
        [ValidarPeticionFilter]
        public ActionResult AsignarRuta(Usuarios u)
        {

            //Validar si es un Usuario o un Administrador
            TipoUsuario tipoUsuario = servicioUsuario.tipoDeUsuario(u);
            if (tipoUsuario == TipoUsuario.Usuario)
            {
                return RedirectToAction("Home", "Necesidades");
            }
            else
            {
                Session["Admin"] = u.IdUsuario;
                return RedirectToAction("Administrador");
            }
        }


        [LoginFilter]
        public ActionResult Perfil()
        {

            int idSession = int.Parse(Session["UserId"].ToString());
            Usuarios usuarioSession = servicioUsuario.obtenerUsuarioPorID(idSession);
            VMPerfil vMPerfil = servicioUsuario.asignoDatosAVMPerfil(usuarioSession);
            bool validoPerfil = servicioUsuario.validarSiExisteFaltanteDeDatos(vMPerfil);
            if (!validoPerfil)
            {
                ViewData.Add("mensajeInfo", "Debe completar sus datos para poder Crear Necesidades");
            }
            return View(vMPerfil);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [LoginFilter]
        public ActionResult Perfil(VMPerfil perfil)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    int idSessionUsuario = int.Parse(Session["UserId"].ToString());
                    Usuarios usuarioSession = servicioUsuario.obtenerUsuarioPorID(idSessionUsuario);
                    VMPerfil vmPerfil = servicioUsuario.asignoDatosAVMPerfil(usuarioSession);
                    bool validoPerfil = servicioUsuario.validarSiExisteFaltanteDeDatos(vmPerfil);
                    if (!validoPerfil)
                    {
                        ViewData.Add("mensajeError", "Debe completar todos los campos");
                    }
                    return View(vmPerfil);
                }

                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    //TODO: Agregar validacion para confirmar que el archivo es una imagen - UsuarioController
                    //creo un nombre significativo en este caso apellidonombre pero solo un caracter del nombre, ejemplo BatistutaG
                    int idUsuario = int.Parse(Session["UserId"].ToString());
                    string nombreSignificativo = idUsuario + " " + Session["Email"];
                    //Guardar Imagen
                    string pathRelativoImagen = ImagenesUtil.Guardar(Request.Files[0], nombreSignificativo);
                    perfil.Foto = pathRelativoImagen;
                }
                int idSession = int.Parse(Session["UserId"].ToString());
                //Asigno datos obtenidos del formulario a usuario
                Usuarios usuarioPerfil = servicioUsuario.asignoDatosAUsuarioDelPerfil(perfil, idSession);

                bool actualizado = servicioUsuario.completoDatosDeMiPerfil(usuarioPerfil);

                if (!actualizado)
                {
                    ViewData.Add("mensajeError", "Error: No se ha podido guardar los datos, intentelo nuevamente");
                }
                else
                {
                    ViewData.Add("mensajeCorrecto", "¡Datos guardados correctamente!");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error: ", ex.Message);
            }

            //Pasar a la vista los datos del usuario
            Usuarios usuario = servicioUsuario.obtenerUsuarioLogueado(int.Parse(Session["UserId"].ToString()));
            VMPerfil vMPerfil = servicioUsuario.asignoDatosAVMPerfil(usuario);
            return View("Perfil", vMPerfil);
        }

        public ActionResult ActivarMiCuenta(string token)
        {
            VMDatosDeVerificacionDeUsuario vmDatosDeVerificacion = new VMDatosDeVerificacionDeUsuario();

            if (!ModelState.IsValid)
            {
                return View();
            }

            //Validar que el email coincida con el codigo
            bool estado = servicioUsuario.validacionDelCodigoDeVerificacionJuntoAlEmail(token);
            if (!estado)
            {
                ViewData.Add("mensajeError", "No encontramos un email con esa clave de verificacion");
                return View("Login");
            }
            else
            {
                ViewData.Add("mensajeCorrecto", "¡Has activado tu cuenta exitosamente! Logueate asi podés ingresar");
            }
            return View("Login");
        }


        [LoginFilter]

        public ActionResult LikeOrDislike(int idNecesidad)
        {
            int idSession = int.Parse(Session["UserId"].ToString());
            string boton = (Request.Form["Like"] != null) ? "Like" : (Request.Form["Dislike"] != null) ? "Dislike" : null;
            LikeOrDislike likeOrDislike = new LikeOrDislike();
            bool estado = likeOrDislike.AgregaLikeOrDislike(idSession, boton, idNecesidad, servicioValoraciones);
            return RedirectToAction("Home", "Necesidades");
        }


        [AdminFilter]
        public ActionResult Administrador()
        {
            VMAdministrador vMAdministrador = servicioUsuario.ObtenerDenunciasParaElAdministrador();
            return View("Administrador", vMAdministrador);
        }

        [HttpPost]
        public ActionResult DenunciaEvaluada(Denuncias denuncia)
        {

            //Si es Desestimar obtengo un false, si es Bloquear obtengo un true
            bool estado = (Request.Form["Desestimar"] != null) ? false : (Request.Form["Bloquear"] != null) ? true : false;
            bool evaluada = servicioDenuncia.NecesidadEvaluada(denuncia.IdNecesidad, estado, denuncia);
            if (evaluada)
            {
                ViewData["mensajeCorrecto"] = "La Denuncia que evaluaste fue guardada con exito";
            }
            else
            {
                ViewData["mensajeError"] = "Ha ocurrido un error al evaluar la necesidad, volverá a aparecerte en el listado";
            }

            return RedirectToAction("Administrador");
        }



        [ActionName("acerca-de")]
        public ActionResult AcercaDe()
        {
            return View();
        }

        [LoginFilter]
        public ActionResult HistorialDonaciones()
        {
            int idSession = int.Parse(Session["UserId"].ToString());
            List<VMNecesidades> listadoNecesidades = ListadoNecesidadesDesdeApiRest(idSession);

            return View(listadoNecesidades);
        }



        private List<VMNecesidades> ListadoNecesidadesDesdeApiRest(int idSession)
        {

            var url = $"https://localhost:44319/api/Necesidades?id=" + idSession + "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";


            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null) return null;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        //Lo deserealiza a tipo List<Necesidades>
                        var result = JsonConvert.DeserializeObject<List<VMNecesidades>>(responseBody);
                        return result;

                    }

                }

            }

        }




    }
}