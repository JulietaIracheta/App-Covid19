using DAO.Context;
using DAO.Repository;
using Entidades;
using Entidades.Views;

namespace DAO
{
    public class DonacionMonetariaDao : BaseRepository<DonacionesMonetarias> //Uso de Generics
    {
        TpDBContext context;
        public DonacionMonetariaDao(TpDBContext contexto) : base(contexto)
        {
            context = contexto;
        }

        public DonacionesMonetarias ActualizarComprobante(VMComprobantePago donaM)
        {
            DonacionesMonetarias DonacionesMonetariasBd = ObtenerPorID(donaM.IdDonacionMonetaria);
            DonacionesMonetariasBd.ArchivoTransferencia = donaM.ArchivoTransferencia;
            context.SaveChanges();
            return DonacionesMonetariasBd;
        }
    }
}
