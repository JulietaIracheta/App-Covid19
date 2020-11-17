using DAO.Context;
using DAO.Repository;
using Entidades;
using System.Collections.Generic;
using System.Linq;

namespace DAO
{
    public class NecesidadesDonacionesMonetariasDAO : BaseRepository<NecesidadesDonacionesMonetarias>
    {
        TpDBContext context;
        public NecesidadesDonacionesMonetariasDAO(TpDBContext contexto) : base(contexto)
        {
            context = contexto;
        }

        public List<NecesidadesDonacionesMonetarias> BuscarMonetariasPorIdNecesidad(int id)
        {
            return (List<NecesidadesDonacionesMonetarias>)context.NecesidadesDonacionesMonetarias.Where(o => o.IdNecesidad == id).ToList();
        }
    }
}
