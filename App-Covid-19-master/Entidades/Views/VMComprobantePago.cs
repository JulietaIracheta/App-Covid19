using System.ComponentModel.DataAnnotations;

namespace Entidades.Views
{
    public class VMComprobantePago
    {
        [Required]
        public string ArchivoTransferencia { get; set; }

        public int IdDonacionMonetaria { get; set; }
    }
}
