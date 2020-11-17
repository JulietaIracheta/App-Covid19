using System.ComponentModel.DataAnnotations;

namespace Entidades.Views
{
    public class VMReferencias
    {
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Nombre del primer contacto")]
        public string Nombre1 { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Telefono del primer contacto")]
        [RegularExpression("([0-9]{2,4})([0-9]{6,10})", ErrorMessage = "El número de teléfono no es válido")]
        public string Telefono1 { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Nombre del segundo contacto")]
        public string Nombre2 { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Telefono del segundo contacto")]
        [RegularExpression("([0-9]{2,4})([0-9]{6,10})", ErrorMessage = "El número de teléfono no es válido")]
        public string Telefono2 { get; set; }

        public int IdNecesidad { get; set; }

        public Necesidades Necesidades { get; set; }

    }
}
