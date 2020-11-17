using System.ComponentModel.DataAnnotations;

namespace Entidades.Views
{
    public class VMPerfil
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Nombre obligatorio")]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Apellido obligatorio")]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Formato de Email erroneo")]
        [StringLength(50, ErrorMessage = "Email demaciado largo")]
        public string Email { get; set; }

        [Display(Name = "Foto de perfil")]
        [Required(ErrorMessage = "Foto de perfil obligatoria")]
        [StringLength(100)]
        public string Foto { get; set; }

        public string Username { get; set; }
    }
}