using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Models
{
    public class UsuarioActualizarModel
    {
        public int Id { get; set; }
        [Display(Name = "Nombre de usuario")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Apellidos { get; set; }
        [Display(Name = "Rol")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int? RolId { get; set; }

        public RolActualizarModel Rol { get; set; }
        public SelectList Roles { get; set; }
    }
}
