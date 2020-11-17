using DAO;
using DAO.Context;
using Entidades;
using Entidades.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Servicios
{
    public class ServicioDenuncia
    {
        DenunciasDao denunciasDao;
        ServicioNecesidad servicioNecesidad;
        NecesidadesDAO necesidadesDAO;
        public ServicioDenuncia(TpDBContext context)
        {
            denunciasDao = new DenunciasDao(context);
            servicioNecesidad = new ServicioNecesidad(context);
            necesidadesDAO = new NecesidadesDAO(context);
        }


        /// <summary>
        /// Guardar la denuncia 
        /// </summary>
        /// <param name="denuncia"></param>
        /// <returns>True o False</returns>
        public Denuncias GuardarDenuncia(Denuncias denuncia, int idUsuario)
        {
            denuncia.FechaCreacion = DateTime.Now;
            denuncia.IdUsuario = idUsuario;
           Denuncias denunciaSave= denunciasDao.Guardar(denuncia);

            return denunciaSave;
        }

        public List<MotivoDenuncia> ObtenerMotivosDenuncia()
        {
            return denunciasDao.ObtenerMotivosDenuncia();
        }

        public List<Denuncias> ObtenerDenunciasEnRevision()
        {
            List<Denuncias> listaDenuncias = denunciasDao.ObtenerDenunciasEnRevision();
            return listaDenuncias;
        }

        public void EvaluarCantidadDenunciasDeNecesidad(int idNecesidad)
        {
            Necesidades necesidad = necesidadesDAO.ObtenerPorID(idNecesidad);
            List<int> idsUsuarioDenuncia = new List<int>();
            foreach (var denuncia in necesidad.Denuncias)
            {
                if (!denuncia.Estado.Equals((int)TipoEstadoDenuncia.Revisada))
                {
                    idsUsuarioDenuncia.Add(denuncia.IdUsuario);
                }
            }

            if (idsUsuarioDenuncia.Count != 0)
            {
                IEnumerable<int> idsUsuarioDenunciaSinDuplicados = idsUsuarioDenuncia.Distinct();
                if (idsUsuarioDenunciaSinDuplicados.Count() == 5) 
                {
                    necesidad.Estado = (int)TipoEstadoNecesidad.Revision; // 3 revision
                                                                          //Actualizo el estado
                    necesidadesDAO.Actualizar(necesidad);
                }
            }

        }

        public bool NecesidadEvaluada(int idNecesidad, bool estado, Denuncias denuncia)
        {
            Denuncias denunciaObtenida = denunciasDao.ObtenerPorID(denuncia.IdDenuncia);
            Necesidades necesidad = necesidadesDAO.ObtenerPorID(idNecesidad);
            if (estado) //True es para dejarla bloqueada/Inactiva a la Necesidad
            {

                if (necesidad == null)
                {
                    return false;
                }

                //Pongo la necesidad en estado bloqueada
                denunciaObtenida.Necesidades.Estado = (int)TipoEstadoNecesidad.Bloqueada;

            }
            else //Al ser false, esta necesidad no le deberia volver a aparecer al Administrador
            {
                if (denunciaObtenida == null)
                {
                    return false;
                }
                denunciaObtenida.Necesidades.Estado = (int)TipoEstadoNecesidad.Activa; // activa 1
            }
            foreach (var d in necesidad.Denuncias)
            {
                d.Estado = (int)TipoEstadoDenuncia.Revisada; // 1 revisada
                                                             //Actualizo el estado
                 denunciasDao.Actualizar(d);

            }

            return true;
        }
    }
}
