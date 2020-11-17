using DAO.Context;
using DAO.Repository;
using Entidades;

namespace DAO
{
    public class DonacionInsumosDao : BaseRepository<DonacionesInsumos>
    {
        public DonacionInsumosDao(TpDBContext contexto) : base(contexto)
        {
        }    
    }
}
