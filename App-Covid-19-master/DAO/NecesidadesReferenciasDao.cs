using DAO.Context;
using DAO.Repository;
using Entidades;
using System.Collections.Generic;
using System.Linq;

namespace DAO
{
    public class NecesidadesReferenciasDao : BaseRepository<NecesidadesReferencias>
    {
        TpDBContext context;
        public NecesidadesReferenciasDao(TpDBContext contexto) : base(contexto)
        {
            context = contexto;
        }

        public List<NecesidadesReferencias> ObtenerReferenciasPorIdNecesidad(int id)
        {
            return (List<NecesidadesReferencias>)context.NecesidadesReferencias.Where(o => o.IdNecesidad == id).ToList();
        }
         
        public void ModificarReferencia(NecesidadesReferencias referencia)
        {
            NecesidadesReferencias r = context.NecesidadesReferencias.Find(referencia.IdReferencia);
            r.Nombre = referencia.Nombre;
            r.Telefono = referencia.Telefono;
            context.SaveChanges();
        }

    }
}
