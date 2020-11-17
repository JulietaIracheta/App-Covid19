using Entidades.Enum;
using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    [MetadataType(typeof(DenunciasMetadata))]
    public partial class Denuncias
    {

    }

    public class DenunciasMetadata
    {
        [Required]
        public int IdMotivo { get; set; }

        [Required]
        public string Comentarios { get; set; }
        public TipoEstadoDenuncia Estado;
    }
}
