using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entidades.Metadata
{
    public class NecesidadesDonacionesMonetariasMetadata
    {
        public int IdNecesidadDonacionMonetaria { get; set; }
        public int IdNecesidad { get; set; }
        [Required(ErrorMessage = "Debe ingresar la cantidad de dinero necesitada")]
        public decimal Dinero { get; set; }
        [Required(ErrorMessage = "Es obligatorio el ingreso de un CBU")]
        [StringLength(22, ErrorMessage ="El CBU no es válido. Debe tener 22 caracteres.")]
        public string CBU { get; set; }
        public virtual ICollection<DonacionesMonetarias> DonacionesMonetarias { get; set; }
        public virtual Necesidades Necesidades { get; set; }
    }
}
