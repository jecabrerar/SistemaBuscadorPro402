using SistemaBuscador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
    public interface IRolRepository
    {
        Task ActualizarRol(RolActualizarModel model);
        Task EliminarRol(int id);
        Task InsertarRol(RolCreacionModel model);
        Task<List<RolListaModel>> ObtenerListaRoles();
        Task<RolActualizarModel> ObtenerRolPorId(int id);
    }
}
