using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Models
{
    public class UsuarioCambioPasswordModel
    {
        public int Id { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MinLength(8, ErrorMessage = "El campo {0} debe tener como minimo {1} caracteres")]
        [RegularExpression(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,16}$", ErrorMessage = "La contraseña debe tener mayusculas y minisculas y digitos")]
        public string Password { get; set; }

        [Display(Name = "Repetir Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [MinLength(8, ErrorMessage = "El campo {0} debe tener como minimo {1} caracteres")]
        [Compare("Password", ErrorMessage = "Las contraseñas no son iguales")]
        public string RePassword { get; set; }
    }
}
