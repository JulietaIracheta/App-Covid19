using DAO;
using DAO.Context;
using Entidades;
using System.Collections.Generic;

namespace Servicios
{
    public class ServicioNecesidadesDonacionesInsumos
    {
        NecesidadesDonacionesInsumosDAO necesidadesDonacionesInsumosDAO;
        public ServicioNecesidadesDonacionesInsumos(TpDBContext context)
        {
            necesidadesDonacionesInsumosDAO = new NecesidadesDonacionesInsumosDAO(context);
        }

        public List<NecesidadesDonacionesInsumos> ListaNombre(NecesidadesDonacionesInsumos idNecesidad)
        {
            return necesidadesDonacionesInsumosDAO.BuscarPorId(idNecesidad.IdNecesidad);
        }


        public NecesidadesDonacionesInsumos ObtenerNecesidadDonacionInsumosPorId(int idNecesidadDonacionInsumo)
        {
            return necesidadesDonacionesInsumosDAO.ObtenerPorID(idNecesidadDonacionInsumo);
        }

    }
}
