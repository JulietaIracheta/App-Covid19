using Entidades.Enum;
using Entidades.ValidationCustom;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entidades.Metadata
{
    public class NecesidadesMetadata
    {
        [Required(ErrorMessage = "Obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        [RegularExpression("([0-9]{2,4})([0-9]{6,10})", ErrorMessage ="El número de teléfono no es válido")]
        public string TelefonoContacto { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [CheckDateGTNow]
        public DateTime FechaFin { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        [StringLength(100)]
        public string Foto { get; set; }

        [Required]
        public int IdUsuarioCreador { get; set; }

        [Required(ErrorMessage = "Obligatorio")]
        public TipoDonacion TipoDonacion { get; set; }



        public NecesidadesMetadata()
        {
            this.Denuncias = new HashSet<Denuncias>();
            this.NecesidadesDonacionesInsumos = new HashSet<NecesidadesDonacionesInsumos>();
            this.NecesidadesDonacionesMonetarias = new HashSet<NecesidadesDonacionesMonetarias>();
            this.NecesidadesReferencias = new HashSet<NecesidadesReferencias>();
            this.NecesidadesValoraciones = new HashSet<NecesidadesValoraciones>();
        }

        public int IdNecesidad { get; set; }
        public System.DateTime FechaCreacion { get; set; }
        public int Estado { get; set; }
        public Nullable<decimal> Valoracion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Denuncias> Denuncias { get; set; }
        public virtual Usuarios Usuarios { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NecesidadesDonacionesInsumos> NecesidadesDonacionesInsumos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NecesidadesDonacionesMonetarias> NecesidadesDonacionesMonetarias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NecesidadesReferencias> NecesidadesReferencias { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NecesidadesValoraciones> NecesidadesValoraciones { get; set; }
    }
}
