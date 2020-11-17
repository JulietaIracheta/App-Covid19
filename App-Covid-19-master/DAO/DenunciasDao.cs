using DAO.Context;
using DAO.Repository;
using Entidades;
using Entidades.Enum;
using System.Collections.Generic;
using System.Linq;

namespace DAO
{
    public class DenunciasDao : BaseRepository<Denuncias>//Uso de Generics
    {
        TpDBContext context;
        public DenunciasDao(TpDBContext contexto) : base(contexto)
        {
            context = contexto;

        }
        public List<Denuncias> ObtenerDenunciasEnRevision()
        {
            List<Denuncias> listaObtenida = context.Denuncias.Where(o => o.Necesidades.Estado == (int)TipoEstadoNecesidad.Revision).ToList();
            return listaObtenida;
        }

        public List<MotivoDenuncia> ObtenerMotivosDenuncia()
        {
            return context.MotivoDenuncia.ToList();
        }
        
    }
}
