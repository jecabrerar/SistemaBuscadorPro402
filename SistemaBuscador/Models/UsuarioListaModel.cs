using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Models
{
    public class UsuarioListaModel
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int RolId { get; set; }
    }
}
