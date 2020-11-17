using DAO;
using DAO.Context;
using Entidades;

namespace Servicios
{
    public class ServicioNecesidadesDonacionesMonetarias
    {
        NecesidadesDonacionesMonetariasDAO NecesidadesDonacionesMonetariasDAO;

        public ServicioNecesidadesDonacionesMonetarias(TpDBContext context)
        {
            NecesidadesDonacionesMonetariasDAO = new NecesidadesDonacionesMonetariasDAO(context);
        }

    }
}
