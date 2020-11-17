using Entidades;
using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class DonacionInsumosDTO
    {
        public int IdDonacionInsumo { get; set; }
        public int IdNecesidadDonacionInsumo { get; set; }
        public int IdUsuario { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaCreacion { get; set; }
        public virtual NecesidadesDonacionesInsumosDTO NecesidadesDonacionesInsumos { get; set; }

        public DonacionInsumosDTO()
        {
        }

        public DonacionInsumosDTO(DonacionesInsumos donacionesInsumos, bool mapearRelacionadas = true)
        {
            this.IdDonacionInsumo = donacionesInsumos.IdDonacionInsumo;
            this.Cantidad = donacionesInsumos.Cantidad;
            this.IdNecesidadDonacionInsumo = donacionesInsumos.IdNecesidadDonacionInsumo;
            this.IdUsuario = donacionesInsumos.IdUsuario;
            this.FechaCreacion = donacionesInsumos.FechaCreacion;

            if (mapearRelacionadas && donacionesInsumos != null)
            {
                this.NecesidadesDonacionesInsumos = new NecesidadesDonacionesInsumosDTO(donacionesInsumos.NecesidadesDonacionesInsumos, false);
            }
        }

        public List<DonacionInsumosDTO> MapearDTO(ICollection<DonacionesInsumos> donacionesInsumos, bool mapearNecesidadDonacionInsumos = true)
        {
            List<DonacionInsumosDTO> listaDto = new List<DonacionInsumosDTO>();
            foreach (var donInsumos in donacionesInsumos)
            {

                listaDto.Add(new DonacionInsumosDTO(donInsumos, mapearNecesidadDonacionInsumos));
            }
            return listaDto;
        }
    }
}