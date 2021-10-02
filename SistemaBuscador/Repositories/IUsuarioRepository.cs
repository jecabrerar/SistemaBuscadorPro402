using Evaluacion.JCabrera.SistemaBuscador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Repositories
{
    public interface IUsuarioRepository
    {
        Task ActualizarPassword(UsuarioCambioPasswordModel model);
        Task ActualizarUsuario(UsuarioActualizarModel model);
        Task EliminarUsuario(int id);
        Task InsertarUsuario(UsuarioCreacionModel model);
        Task<List<UsuarioListaModel>> ObtenerListaUsuarios();
        Task<UsuarioActualizarModel> ObtenerUsuarioPorId(int id);
        Task<UsuarioCreacionModel> UsuarioCreacionModel();
    }
}
