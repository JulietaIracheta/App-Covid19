using DAO.Context;
using DAO.Repository;
using Entidades;
using Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAO
{
    public class NecesidadesDAO : BaseRepository<Necesidades>
    {
        TpDBContext context;

        #region Crud
        public NecesidadesDAO(TpDBContext contexto) : base(contexto)
        {
            context = contexto;
        }
        

        public List<Necesidades> TraerNecesidadesActivasDelUsuario(int idSession)
        {
            List<Necesidades> necesidadesActivas = (from c in context.Necesidades
                                                    where c.IdUsuarioCreador == idSession
                                                    where c.Estado == 1
                                                    select c).ToList();
            return necesidadesActivas;
        }

        public List<Necesidades> TraerTodasLasNecesidadesDelUsuario(int idSession)
        {
            List<Necesidades> todasLasNecesidadesDelUsuario = (from c in context.Necesidades
                                                               where c.IdUsuarioCreador.Equals(idSession)
                                                               select c).ToList();

            return todasLasNecesidadesDelUsuario;
        }


        public List<Necesidades> ListarNecesidadesVencidasActivas()
        {
            List<Necesidades> listadoNecesidades = new List<Necesidades>();
            var listaObtenida = (from nec in context.Necesidades
                                 where nec.FechaFin < DateTime.Now && nec.Estado ==(int) TipoEstadoNecesidad.Activa
                                 select nec);

            foreach (var item in listaObtenida)
            {
                listadoNecesidades.Add(item);
            }
            return listadoNecesidades;

        }



        public void ActivarNecesidad(int idNecesidad)
        {
            Necesidades n = context.Necesidades.Find(idNecesidad);
            n.Estado = 1;
            context.SaveChanges();
        }
        #endregion
        #region otros
        /// <summary>
        /// Buscar necesidades en relación al nombre de las necesidades existentes o bien según el nombre del
        /// usuario creador. Ordenado por fecha más cercana de cierre de necesidad y, luego,
        /// por mayor valoración de la necesidad.El resultado de la búsqueda no deberá incluir sus propias
        /// necesidades
        /// </summary>
        /// <param name="input"></param>
        /// <param name="idUser"></param>
        /// <returns></returns>
        public List<Necesidades> Buscar(string input, int idUser)
        {
            List<Necesidades> necesidadesObtenidas =
              (
              from necesidad in context.Necesidades.Include("Usuarios")
              where necesidad.Usuarios.Nombre.Contains(input) || necesidad.Nombre.Contains(input)
              where !necesidad.IdUsuarioCreador.Equals(idUser)
              select necesidad
              ).OrderBy(o => o.FechaFin).ThenByDescending(o => o.Valoracion).ToList();
            return necesidadesObtenidas;
        }

        public List<Necesidades> ListarTodasLasNecesidadesActivas()
        {
            List<Necesidades> listadoNecesidades = new List<Necesidades>();

            var listaObtenida = (from nec in context.Necesidades
                                 where nec.FechaFin > DateTime.Now
                                 where nec.Estado == 1
                                 select nec);

            foreach (var item in listaObtenida)
            {
                listadoNecesidades.Add(item);
            }

            return listadoNecesidades;

        }

        public List<Necesidades> TraerNecesidadesQueNoSonDelUsuario(int idSession)
        {
            List<Necesidades> listaNecesidades = new List<Necesidades>();

            var listaObtenida = (from nec in context.Necesidades
                                 where nec.FechaFin > DateTime.Now
                                 where nec.Estado == 1
                                 where !nec.IdUsuarioCreador.Equals(idSession)
                                 select nec);

            foreach (var item in listaObtenida)
            {
                listaNecesidades.Add(item);
            }

            return listaNecesidades;
        }
        public List<Necesidades> ObtenerNecesidadesDenunciadas()
        {
            List<Necesidades> necesidadesBD = context.Necesidades.Where(o => o.Estado == (int)TipoEstadoNecesidad.Revision).ToList();
            return necesidadesBD;
        }

       
        public List<Necesidades> TraerNecesidadesConDonacionInsumosPorUserLogueado(int idUserLogueado)
        {
            List<Necesidades> listadoNecesidades = (from nec in context.Necesidades

                                                    join necDonacionesInsumos in context.NecesidadesDonacionesInsumos
                                                    on nec.IdNecesidad equals necDonacionesInsumos.IdNecesidad
                                                    join DonInsumos in context.DonacionesInsumos
                                                    on necDonacionesInsumos.IdNecesidadDonacionInsumo equals DonInsumos.IdNecesidadDonacionInsumo

                                                    where DonInsumos.IdUsuario == idUserLogueado
                                                    orderby DonInsumos.FechaCreacion descending
                                                    select nec).ToList();


            return listadoNecesidades;
        }

        public List<Necesidades> TraerNecesidadesConDonacionMonetariasPorUserLogueado(int idUserLogueado)
        {

            List<Necesidades> listadoNecesidades =
                (
                from nec in context.Necesidades
                join necDonacionesMonetarias in context.NecesidadesDonacionesMonetarias
                on nec.IdNecesidad equals necDonacionesMonetarias.IdNecesidad
                join DonMonetarias in context.DonacionesMonetarias
                on necDonacionesMonetarias.IdNecesidadDonacionMonetaria equals DonMonetarias.IdNecesidadDonacionMonetaria


                where DonMonetarias.IdUsuario == idUserLogueado
                orderby DonMonetarias.FechaCreacion descending
                select nec
                         ).ToList();
            return listadoNecesidades;
        }

        #endregion
      
    }
}
