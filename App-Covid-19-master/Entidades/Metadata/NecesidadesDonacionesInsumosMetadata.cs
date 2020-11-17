using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entidades.Metadata
{
    public class NecesidadesDonacionesInsumosMetadata
    {
        public int IdNecesidadDonacionInsumo { get; set; }

        public int IdNecesidad { get; set; }
        [Required(ErrorMessage = "El nombre del insumo es obligatorio")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debe añadir una cantidad")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad minima debe ser 1")]
        public int Cantidad { get; set; }

        public virtual ICollection<DonacionesInsumos> DonacionesInsumos { get; set; }
        public virtual Necesidades Necesidades { get; set; }
    }
}
