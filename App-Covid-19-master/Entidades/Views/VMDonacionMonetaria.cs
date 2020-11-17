using System.ComponentModel.DataAnnotations;

namespace Entidades.Views
{
    public class VMDonacionMonetaria
    {
        [Required(ErrorMessage = "Debe ingresar un monto a donar valido")]
        [Range(100, 9999999, ErrorMessage = "La donación minima es de $100")]
        public decimal Dinero { get; set; }

        public int IdNecesidadDonacionMonetaria { get; set; }
    }
}