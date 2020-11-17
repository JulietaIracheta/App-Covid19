using DAO;
using DAO.Context;
using Entidades;
using Entidades.Enum;
using System.Collections.Generic;

namespace Servicios
{
    public class ServicioNecesidadValoraciones
    {
        UsuarioDao usuarioDao;
        NecesidadesDAO necesidadesDAO;
        NecesidadValoracionesDao necesidadValoracionesDao;
        ServicioNecesidad servicioNecesidad;

        public ServicioNecesidadValoraciones(TpDBContext context)
        {
            usuarioDao = new UsuarioDao(context);
            necesidadesDAO = new NecesidadesDAO(context);
            necesidadValoracionesDao = new NecesidadValoracionesDao(context);
            servicioNecesidad = new ServicioNecesidad(context);

        }
        public bool guardarValoracion(int idUsuario, int idNecesidad, string botonRecibido)
        {
            //Obtengo Usuario y Necesidad
            Usuarios usuarioObtenido = usuarioDao.ObtenerPorID(idUsuario);
            Necesidades necesidadObtenida = necesidadesDAO.ObtenerPorID(idNecesidad);
            if (necesidadObtenida.Estado == (int)TipoEstadoNecesidad.Bloqueada || necesidadObtenida.Estado == (int)TipoEstadoNecesidad.Finalizada)
            {
                return false;
            }

            //Valido si es que antes le dio Like or Dislike
            NecesidadesValoraciones necesidadRegistrada = necesidadValoracionesDao.obtenerNecesidadValoracionPor_IDUsuario_e_IdNecesidad(idUsuario, idNecesidad);

            if (necesidadRegistrada != null)
            {
                NecesidadesValoraciones valoracionObtenidaBD = new NecesidadesValoraciones();
                if (botonRecibido == "Like")
                {
                    if (necesidadRegistrada.IdNecesidad == idNecesidad)
                    {
                        if (necesidadRegistrada.Valoracion == "Like") //Si el estado en la BD tenia su MG, se lo remueve para que no quede el boton seleccionado
                        {
                            necesidadRegistrada.Valoracion = "Undefined";
                            valoracionObtenidaBD = necesidadValoracionesDao.Actualizar(necesidadRegistrada);

                            necesidadObtenida.NecesidadesValoraciones.Add(valoracionObtenidaBD);
                            Necesidades necesidadNueva = servicioNecesidad.calcularValoracion(necesidadObtenida);


                            if (valoracionObtenidaBD == null)
                            {
                                return false;
                            }
                        }
                        else if (necesidadRegistrada.Valoracion != "Like") //Si el estado en la BD tenia su MG removido, se lo vuelve a poner en MG, para que quede el boton seleccionado
                        {
                            necesidadRegistrada.Valoracion = "Like";
                            valoracionObtenidaBD = necesidadValoracionesDao.Actualizar(necesidadRegistrada);

                            necesidadObtenida.NecesidadesValoraciones.Add(valoracionObtenidaBD);
                            Necesidades necesidadNueva = servicioNecesidad.calcularValoracion(necesidadObtenida);

                            if (valoracionObtenidaBD == null)
                            {
                                return false;
                            }
                        }

                    }

                }

                if (botonRecibido == "Dislike")
                {

                    if (necesidadRegistrada.IdNecesidad == idNecesidad)
                    {
                        if (necesidadRegistrada.Valoracion == "Dislike") //Si el estado en la BD tenia su Dislike, se lo remueve para que no quede el boton seleccionado
                        {
                            necesidadRegistrada.Valoracion = "Undefined";
                            valoracionObtenidaBD = necesidadValoracionesDao.Actualizar(necesidadRegistrada);


                            necesidadObtenida.NecesidadesValoraciones.Add(valoracionObtenidaBD);
                            Necesidades necesidadNueva = servicioNecesidad.calcularValoracion(necesidadObtenida);

                            if (valoracionObtenidaBD == null)
                            {
                                return false;
                            }
                        }
                        else if (necesidadRegistrada.Valoracion != "Dislike") //Si el estado en la BD tenia su MG removido, se lo vuelve a poner en MG, para que quede el boton seleccionado
                        {
                            necesidadRegistrada.Valoracion = "Dislike";
                            valoracionObtenidaBD = necesidadValoracionesDao.Actualizar(necesidadRegistrada);


                            necesidadObtenida.NecesidadesValoraciones.Add(valoracionObtenidaBD);
                            Necesidades necesidadNueva = servicioNecesidad.calcularValoracion(necesidadObtenida);

                            if (valoracionObtenidaBD == null)
                            {
                                return false;
                            }
                        }
                    }

                }
            }
            else //Es decir, nunca le habia dado MG a esa publicacion
            {

                //Asigno datos al objeto Necesidad Valoraciones
                NecesidadesValoraciones necesidadesValoraciones = new NecesidadesValoraciones();
                necesidadesValoraciones.IdUsuario = usuarioObtenido.IdUsuario;
                necesidadesValoraciones.IdNecesidad = necesidadObtenida.IdNecesidad;
                necesidadesValoraciones.Valoracion = (botonRecibido == "Like") ? "Like" : (botonRecibido == "Dislike") ? "Dislike" : null;

                NecesidadesValoraciones valoracionObtenida = necesidadValoracionesDao.Guardar(necesidadesValoraciones);
                if (valoracionObtenida == null)
                {
                    return false;
                }

                necesidadObtenida.NecesidadesValoraciones.Add(valoracionObtenida);
                Necesidades necesidadNueva = servicioNecesidad.calcularValoracion(necesidadObtenida);
            }


            return true;
        }

        public List<NecesidadesValoraciones> obtenerValoracionesPorIDNecesidad(int idNecesidad)
        {

            List<NecesidadesValoraciones> valoracionesDelaNecesidad = necesidadValoracionesDao.obtenerValoracionesPorIDNecesidad(idNecesidad);
            return valoracionesDelaNecesidad;
        }

    }
}
