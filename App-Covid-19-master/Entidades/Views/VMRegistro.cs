using Entidades.ValidationCustom;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entidades.Views
{
    public class VMRegistro
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = " Email es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de Email erroneo")]
        [StringLength(50, ErrorMessage = "Email demaciado largo")]

        public string Email { get; set; }

        [Display(Name = "Clave")]
        [Required(ErrorMessage = " Contraseña es obligatoria")]
        //Puede comenzar con A-Z, o a-z, o numeros de 0 a 9    |  finaliza con a-z - A-Z -ó- 0-9   {desde, hasta}
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[a-zA-Z0-9]{1,}$", ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un número")]
        [MinLength(8, ErrorMessage = "Su contraseña debe tener 8 digitos como minimo")]
        public string Password { get; set; }

        [Display(Name = " Repita su clave")]
        [Required(ErrorMessage = " Repita su contraseña")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public string RepeatPassword { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Required(ErrorMessage = "Fecha de nacimiento obligatoria")]
        [CheckValidDate]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
    }
}