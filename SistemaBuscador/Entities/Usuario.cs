using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string  Nombres { get; set; }
        public string Apellidos { get; set; }
        public int RolId { get; set; }
        public string NombreUsuario { get; set; }

        public string Password { get; set; }

        public Rol Rol { get; set; }
    }
}
