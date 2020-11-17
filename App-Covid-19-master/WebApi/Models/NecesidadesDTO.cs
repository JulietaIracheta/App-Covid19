using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Models
{
    public class NecesidadesDTO
    {
        public int IdNecesidad { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public System.DateTime FechaFin { get; set; }
        public string TelefonoContacto { get; set; }
        public int TipoDonacion { get; set; }
        public string Foto { get; set; }
        public int IdUsuarioCreador { get; set; }
        public int Estado { get; set; }
        public Nullable<decimal> Valoracion { get; set; }

        public List<NecesidadesDonacionesInsumosDTO> NecesidadesDonacionesInsumos { get; set; }
        public virtual List<NecesidadesDonacionesMonetariasDTO> NecesidadesDonacionesMonetarias { get; set; }

        public NecesidadesDTO()
        {
        }

        public NecesidadesDTO(Necesidades necesidadesEntidad)
        {
            this.IdNecesidad = necesidadesEntidad.IdNecesidad;
            this.Nombre = necesidadesEntidad.Nombre;
            this.Descripcion = necesidadesEntidad.Descripcion;
            this.TelefonoContacto = necesidadesEntidad.TelefonoContacto;
            this.TipoDonacion = necesidadesEntidad.TipoDonacion;
            this.Foto = necesidadesEntidad.Foto;
            this.IdUsuarioCreador = necesidadesEntidad.IdUsuarioCreador;
            this.Estado = necesidadesEntidad.Estado;
            this.Valoracion = necesidadesEntidad.Valoracion;

            if (necesidadesEntidad.NecesidadesDonacionesInsumos != null)
            {
                this.NecesidadesDonacionesInsumos = NecesidadesDonacionesInsumosDTO.MapearListaEF(necesidadesEntidad.NecesidadesDonacionesInsumos.ToList(), true);
            }

            if (necesidadesEntidad.NecesidadesDonacionesMonetarias != null)
            {
                this.NecesidadesDonacionesMonetarias = NecesidadesDonacionesMonetariasDTO.MapearListaEF(necesidadesEntidad.NecesidadesDonacionesMonetarias.ToList(), true);
            }
        }

        public static List<NecesidadesDTO> MapearListaEF(List<Necesidades> listaNecesidadesEF)
        {
            List<NecesidadesDTO> listaNecesidadesDTO = new List<NecesidadesDTO>();

            //mapeamos las necesidades a DTO y las agregamos a la lista que quiero retornar
            foreach (Necesidades necesidadEF in listaNecesidadesEF)
            {
                listaNecesidadesDTO.Add(new NecesidadesDTO(necesidadEF));
            }
            return listaNecesidadesDTO;
        }
    }
}