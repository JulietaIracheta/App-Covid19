using DAO.Context;
using DAO.Repository;
using Entidades;
using System.Collections.Generic;
using System.Linq;

namespace DAO
{
    public class NecesidadesDonacionesInsumosDAO : BaseRepository<NecesidadesDonacionesInsumos>
    {
        TpDBContext context;
        public NecesidadesDonacionesInsumosDAO(TpDBContext contexto) : base(contexto)
        {
            context = contexto;
        }

        public List<NecesidadesDonacionesInsumos> BuscarPorId(int IdNecesidad)
        {
            List<NecesidadesDonacionesInsumos> listaObtenida = context.NecesidadesDonacionesInsumos.Where(o => o.IdNecesidad == IdNecesidad).ToList();
            return listaObtenida;
        }

        public List<NecesidadesDonacionesInsumos> BuscarInsumosPorIdNecesidad(int id)
        {
            return context.NecesidadesDonacionesInsumos.Where(o => o.IdNecesidad == id).ToList();
        }

    }
}
