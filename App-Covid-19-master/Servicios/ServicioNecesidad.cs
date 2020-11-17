using DAO;
using DAO.Context;
using Entidades;
using Entidades.Enum;
using Entidades.Metadata;
using Entidades.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Servicios
{
    public class ServicioNecesidad
    {
        NecesidadesDAO necesidadesDAO;
        TpDBContext contexto;
        NecesidadesReferenciasDao necesidadesReferenciasDao;
        NecesidadesDonacionesInsumosDAO necesidadesDonacionesInsumosDao;

        public void VerificarFechaFinDeNecesidades()
        {
            List<Necesidades> necesidadesVencidas = necesidadesDAO.ListarNecesidadesVencidasActivas();
            foreach (var item in necesidadesVencidas)
            {
                item.Estado = (int)TipoEstadoNecesidad.Finalizada;
                necesidadesDAO.Actualizar(item);
            }
            
        }

        NecesidadesDonacionesMonetariasDAO necesidadesDonacionesMonetariasDAO;
        public ServicioNecesidad(TpDBContext context)
        {
            necesidadesDAO = new NecesidadesDAO(context);
            contexto = context;
            necesidadesReferenciasDao = new NecesidadesReferenciasDao(context);
            necesidadesDonacionesInsumosDao = new NecesidadesDonacionesInsumosDAO(context);
            necesidadesDonacionesMonetariasDAO = new NecesidadesDonacionesMonetariasDAO(context);
        }
        #region necesidad
        public Necesidades obtenerNecesidadPorId(int id)
        {
            return necesidadesDAO.ObtenerPorID(id);
        }

        public Necesidades buildNecesidad(NecesidadesMetadata necesidadmd, int idUser)

        {
            Necesidades necesidades = new Necesidades()
            {
                Nombre = necesidadmd.Nombre,
                Descripcion = necesidadmd.Descripcion,
                TelefonoContacto = necesidadmd.TelefonoContacto,
                FechaCreacion = DateTime.Now,
                FechaFin = necesidadmd.FechaFin,
                Foto = necesidadmd.Foto,
                TipoDonacion = (necesidadmd.TipoDonacion == TipoDonacion.Monetaria) ? 1 : 2,
                IdUsuarioCreador = idUser,
                Estado = 0,
                Valoracion = 0
            };

            return necesidadesDAO.Guardar(necesidades);
        }



        /// <summary>
        /// Trae todas las necesidades del usuario en base al estado de las mismas
        /// </summary>
        /// <param name="idSession"></param>
        /// <param name="estadoNecesidad"></param>
        /// <returns></returns>
        public List<Necesidades> TraerNecesidadesDelUsuario(int idSession, string estadoNecesidad)
        {
            if (estadoNecesidad == "on")
            {
                List<Necesidades> necesidadesBD = necesidadesDAO.TraerNecesidadesActivasDelUsuario(idSession);
                return necesidadesBD;
            }
            else
            {
                List<Necesidades> necesidadesBD = necesidadesDAO.TraerTodasLasNecesidadesDelUsuario(idSession);
                return necesidadesBD;

            }

        }

        /// <summary>
        /// Algoritmo que calcula la valoracion de la necesidad
        /// </summary>
        /// <param name="necesidad"></param>
        /// <returns>Necesidades</returns>
        public Necesidades calcularValoracion(Necesidades necesidad)
        {
            ServicioNecesidadValoraciones servicioNecesidadValoraciones = new ServicioNecesidadValoraciones(contexto);

            List<NecesidadesValoraciones> valoracionesObtenidas = servicioNecesidadValoraciones.obtenerValoracionesPorIDNecesidad(necesidad.IdNecesidad);
            decimal cantidadLikes = 0;
            decimal cantidadDeVotaciones = valoracionesObtenidas.Count;
            decimal resultado = 0;

            //   foreach (var item in valoracionesObtenidas)
            foreach (var item in necesidad.NecesidadesValoraciones)
            {
                if (item.Valoracion == "Like")
                {
                    cantidadLikes++;
                }
                resultado = (cantidadLikes / cantidadDeVotaciones * 100);
            }

            necesidad.Valoracion = Math.Round(resultado, 2);

            // necesidad.Valoracion = valoracion;


            necesidadesDAO.Actualizar(necesidad);
           
            return necesidad;
        }

        public List<NecesidadesDonacionesInsumos> ObtenerInsumosPorIdNecesidad(int idN)
        {
            return necesidadesDonacionesInsumosDao.BuscarInsumosPorIdNecesidad(idN);
        }

        public List<NecesidadesDonacionesMonetarias> ObtenerMonetariasPorIdNecesidad(int idN)
        {
            return necesidadesDonacionesMonetariasDAO.BuscarMonetariasPorIdNecesidad(idN);
        }


        /// <summary>
        /// Divide en cada espacio con Split, el string ingresado en el buscador
        /// para luego buscar por cada palabra ingresada
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<Necesidades> Buscar(string input)
        {
            int idUser = int.Parse(HttpContext.Current.Session["UserId"].ToString());
            List<Necesidades> necesidades = new List<Necesidades>();
            char delimitador = ' ';
            string[] valores = input.Split(delimitador);

            foreach (var item in valores)
            {
                var necesidadesObtenidas = necesidadesDAO.Buscar(item, idUser);
                foreach (var itemObtenida in necesidadesObtenidas)
                {
                    if (!necesidades.Contains(itemObtenida))
                    {
                        necesidades.Add(itemObtenida);
                    }
                }
            }
            return necesidades;
        }


        public void EditarNecesidad(NecesidadesMetadata nm)
        {
            Necesidades n = this.obtenerNecesidadPorId(nm.IdNecesidad);
            n.Nombre = nm.Nombre;
            n.Descripcion = nm.Descripcion;
            n.TelefonoContacto = nm.TelefonoContacto;
            n.FechaFin = nm.FechaFin;
            n.Foto = nm.Foto;
            necesidadesDAO.Actualizar(n);
        }

        public NecesidadesMetadata ConvertirNecesidadAMetadata(Necesidades n)
        {
            NecesidadesMetadata meta = new NecesidadesMetadata()
            {
                Nombre = n.Nombre,
                Descripcion = n.Descripcion,
                TelefonoContacto = n.TelefonoContacto,
                FechaFin = n.FechaFin,
                Foto = n.Foto,
                NecesidadesReferencias = n.NecesidadesReferencias,
                IdUsuarioCreador = n.IdUsuarioCreador,
                IdNecesidad = n.IdNecesidad,
                TipoDonacion = n.TipoDonacion == 1 ? TipoDonacion.Monetaria : TipoDonacion.Insumos
            };
            return meta;
        }

        public List<Necesidades> obtener5NecesidadesMasValoradas()
        {
            List<Necesidades> listadoNecesidades = necesidadesDAO.ListarTodasLasNecesidadesActivas();
            List<Necesidades> necesidadesMasValoradas = new List<Necesidades>();
            int cantidad = (listadoNecesidades.Count >= 5) ? 5 : listadoNecesidades.Count;

            foreach (var item in listadoNecesidades.OrderByDescending(n => n.Valoracion).ToList())

            {
                necesidadesMasValoradas.Add(item);

                if (necesidadesMasValoradas.Count == cantidad)
                {
                    break;
                }
            }

            return necesidadesMasValoradas;

        }

        public List<NecesidadesReferencias> ObtenerReferenciasPorIdNecesidad(int id)
        {
            return (List<NecesidadesReferencias>)necesidadesReferenciasDao.ObtenerReferenciasPorIdNecesidad(id);
        }

        public bool ModificarReferencia(NecesidadesReferencias r)
        {
            if(string.IsNullOrEmpty(r.Nombre) || string.IsNullOrEmpty(r.Telefono))
            {
                return false;
            }
            necesidadesReferenciasDao.ModificarReferencia(r);
            return true;
        }

        public List<NecesidadesDonacionesInsumosMetadata> ObtenerInsumosMetadataPorIdNecesidad(int id)
        {
            List<NecesidadesDonacionesInsumos> listaInsumos = this.ObtenerInsumosPorIdNecesidad(id);
            List<NecesidadesDonacionesInsumosMetadata> listaMeta = new List<NecesidadesDonacionesInsumosMetadata>();
            foreach(var i in listaInsumos)
            {
                NecesidadesDonacionesInsumosMetadata meta = new NecesidadesDonacionesInsumosMetadata()
                {
                    Nombre = i.Nombre,
                    Cantidad = i.Cantidad,
                    IdNecesidadDonacionInsumo = i.IdNecesidadDonacionInsumo,
                    IdNecesidad = i.IdNecesidad
                };
                listaMeta.Add(meta);
            };
            return listaMeta;
        }

        public void EditarInsumo(NecesidadesDonacionesInsumosMetadata metaI)
        {
            NecesidadesDonacionesInsumos insumo = necesidadesDonacionesInsumosDao.ObtenerPorID(metaI.IdNecesidadDonacionInsumo);
            insumo.Nombre = metaI.Nombre;
            insumo.Cantidad = metaI.Cantidad;
            necesidadesDonacionesInsumosDao.Actualizar(insumo);
        }
        public void EditarMonetaria(NecesidadesDonacionesMonetariasMetadata metaI)
        {
            NecesidadesDonacionesMonetarias Monetaria = necesidadesDonacionesMonetariasDAO.ObtenerPorID(metaI.IdNecesidadDonacionMonetaria);
            Monetaria.Dinero = metaI.Dinero;
            Monetaria.CBU= metaI.CBU;
            necesidadesDonacionesMonetariasDAO.Actualizar(Monetaria);
        }

        public List<NecesidadesDonacionesMonetariasMetadata> ObtenerMonetariasMetadataPorIdNecesidad(int id)
        {
            List<NecesidadesDonacionesMonetarias> listaMonetarias = this.ObtenerMonetariasPorIdNecesidad(id);
            List<NecesidadesDonacionesMonetariasMetadata> listaMeta = new List<NecesidadesDonacionesMonetariasMetadata>();
            foreach (var m in listaMonetarias)
            {
                NecesidadesDonacionesMonetariasMetadata meta = new NecesidadesDonacionesMonetariasMetadata()
                {
                    Dinero = m.Dinero,
                    CBU = m.CBU,
                    IdNecesidadDonacionMonetaria = m.IdNecesidadDonacionMonetaria,
                    IdNecesidad = m.IdNecesidad
                };
                listaMeta.Add(meta);
            };
            return listaMeta;
        }

        public void ActivarNecesidad(int idNecesidad)
        {
            necesidadesDAO.ActivarNecesidad(idNecesidad);
        }
        public List<Necesidades> TraerNecesidadesQueNoSonDelUsuario(int idSession)
        {
            List<Necesidades> necesidadesBD = necesidadesDAO.TraerNecesidadesQueNoSonDelUsuario(idSession);
            return necesidadesBD;
        }
       
        #endregion
        #region InsumosMonetarias
        public NecesidadesDonacionesInsumos AgregarInsumos(NecesidadesDonacionesInsumosMetadata insumometa)
        {
            NecesidadesDonacionesInsumos insumo = new NecesidadesDonacionesInsumos()
            {
                Cantidad = insumometa.Cantidad,
                Nombre = insumometa.Nombre,
                IdNecesidad = insumometa.IdNecesidad,
                Necesidades = insumometa.Necesidades
            };
            return necesidadesDonacionesInsumosDao.Guardar(insumo);
        }
        public NecesidadesDonacionesMonetarias AgregarMonetarias(NecesidadesDonacionesMonetariasMetadata monetariameta)
        {
            NecesidadesDonacionesMonetarias monetaria = new NecesidadesDonacionesMonetarias()
            {
                CBU = monetariameta.CBU,
                Dinero = monetariameta.Dinero,
                IdNecesidad = monetariameta.IdNecesidad,
                Necesidades = monetariameta.Necesidades
            };
            return necesidadesDonacionesMonetariasDAO.Guardar(monetaria);
        }
        
        #endregion
        public void AgregarReferencias(VMReferencias vmref)
        {
            NecesidadesReferencias nr = new NecesidadesReferencias()
            {
                IdNecesidad = vmref.IdNecesidad,
                Nombre = vmref.Nombre1,
                Telefono = vmref.Telefono1,
                Necesidades = vmref.Necesidades
            };
            necesidadesReferenciasDao.Guardar(nr);
            NecesidadesReferencias nr2 = new NecesidadesReferencias()
            {
                IdNecesidad = vmref.IdNecesidad,
                Nombre = vmref.Nombre2,
                Telefono = vmref.Telefono1,
                Necesidades = vmref.Necesidades
            };
            necesidadesReferenciasDao.Guardar(nr2);
        }

        public List<Necesidades> TraerNecesidadesConDonacionesDelUserLogueado(int idUserLogueado)
        {
            List<Necesidades> necesidadesConDonacionesMonetarias = necesidadesDAO.TraerNecesidadesConDonacionMonetariasPorUserLogueado(idUserLogueado);
            List<Necesidades> necesidadesConDonacionesInsumos = necesidadesDAO.TraerNecesidadesConDonacionInsumosPorUserLogueado(idUserLogueado);
            List<Necesidades> necesidades = new List<Necesidades>();

            if (necesidadesConDonacionesMonetarias.Count != 0)
            {
                foreach (var item in necesidadesConDonacionesMonetarias)
                {
                    if (!necesidades.Contains(item))
                    {
                        necesidades.Add(item);
                    }

                }

            }
            if (necesidadesConDonacionesInsumos.Count != 0)
            {
                foreach (var item in necesidadesConDonacionesInsumos)
                {
                    if (!necesidades.Contains(item))
                    {
                        necesidades.Add(item);
                    }
                }

            }
            return necesidades;
        }

    }
}

