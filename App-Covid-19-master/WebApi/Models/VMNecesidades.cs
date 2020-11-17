using Entidades.Enum;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class VMNecesidades
    {
        public int IdNecesidad { get; set; }
        public string Nombre { get; set; }
        public TipoDonacion TipoDonacion { get; set; }
        public TipoEstadoNecesidad Estado { get; set; }
        public decimal TotalDineroRecaudado { get; set; }
        public List<DonacionesInsumosVM> DonacionesInsumos { get; set; }
        public List<DonacionInsumosDTO> MisDonacionesInsumos { get; set; }
        public List<DonacionesMonetariasDTO> MisDonacionesMonetarias { get; set; }
        public decimal TotalDineroDonado { get; set; }
        public VMNecesidades()
        {
            this.DonacionesInsumos = new List<DonacionesInsumosVM>();
            this.MisDonacionesMonetarias = new List<DonacionesMonetariasDTO>();
            this.MisDonacionesInsumos = new List<DonacionInsumosDTO>();
            this.TotalDineroRecaudado = 0M;
        }
    }
}