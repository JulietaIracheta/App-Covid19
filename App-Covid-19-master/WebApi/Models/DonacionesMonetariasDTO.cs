using Entidades;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class DonacionesMonetariasDTO
    {
        public int IdDonacionMonetaria { get; set; }
        public int IdNecesidadDonacionMonetaria { get; set; }
        public int IdUsuario { get; set; }
        public decimal Dinero { get; set; }
        public string ArchivoTransferencia { get; set; }
        public System.DateTime FechaCreacion { get; set; }

        public DonacionesMonetariasDTO()
        {
        }

        public DonacionesMonetariasDTO(DonacionesMonetarias donacionMonetaria)
        {
            this.IdDonacionMonetaria = donacionMonetaria.IdDonacionMonetaria;
            this.IdNecesidadDonacionMonetaria = donacionMonetaria.IdNecesidadDonacionMonetaria;
            this.IdUsuario = donacionMonetaria.IdUsuario;
            this.Dinero = donacionMonetaria.Dinero;
            this.ArchivoTransferencia = donacionMonetaria.ArchivoTransferencia;
            this.FechaCreacion = donacionMonetaria.FechaCreacion;
        }

        public List<DonacionesMonetariasDTO> MapearListaDTO(ICollection<DonacionesMonetarias> donacionesMonetarias)
        {
            List<DonacionesMonetariasDTO> listaDto = new List<DonacionesMonetariasDTO>();
            foreach (var donMonetarias in donacionesMonetarias)
            {
                listaDto.Add(new DonacionesMonetariasDTO(donMonetarias));
            }

            return listaDto;
        }
    }
}