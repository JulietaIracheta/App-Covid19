using System.ComponentModel.DataAnnotations;

namespace Entidades.Views
{
    public class VMReenvioDeEmail
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = " Email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de Email erroneo")]
        [StringLength(50, ErrorMessage = "Email demasiado largo")]
        public string Email { get; set; }
    }
}
