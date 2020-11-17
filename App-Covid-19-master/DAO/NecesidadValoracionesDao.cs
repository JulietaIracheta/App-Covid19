using DAO.Context;
using DAO.Repository;
using Entidades;
using System.Collections.Generic;
using System.Linq;

namespace DAO
{
    public class NecesidadValoracionesDao : BaseRepository<NecesidadesValoraciones>
    {
        TpDBContext context;
        public NecesidadValoracionesDao(TpDBContext contexto) : base(contexto)
        {
            context = contexto;
        }

        public List<NecesidadesValoraciones> obtenerValoracionesDelUsuario(int idSession)
        {
            List<NecesidadesValoraciones> valoracionesObtenidas = context.NecesidadesValoraciones.Where(o => o.IdUsuario == idSession).ToList();
            return valoracionesObtenidas;
        }

        public NecesidadesValoraciones obtenerNecesidadValoracionPor_IDUsuario_e_IdNecesidad(int idUsuario, int idNecesidad)
        {
            NecesidadesValoraciones valoracionObtenida = context.NecesidadesValoraciones.Where(o => o.IdUsuario == idUsuario).Where(n => n.IdNecesidad == idNecesidad).FirstOrDefault();
            return valoracionObtenida;
        }


        public List<NecesidadesValoraciones> obtenerValoracionesPorIDNecesidad(int idNecesidad)
        {
            List<NecesidadesValoraciones> listadoObtenido = context.NecesidadesValoraciones.Where(o => o.IdNecesidad == idNecesidad).ToList();
            return listadoObtenido;
        }


        public List<NecesidadesValoraciones> obtenerValoracionPorIdNecesidad(int idNecesidad)
        {
            List<NecesidadesValoraciones> valoracionDB = context.NecesidadesValoraciones.Where(o => o.IdNecesidad == idNecesidad).ToList();
            return valoracionDB;
        }
    }
}
