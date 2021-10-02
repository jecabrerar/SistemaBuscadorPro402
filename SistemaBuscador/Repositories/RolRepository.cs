using Microsoft.EntityFrameworkCore;
using Evaluacion.JCabrera.SistemaBuscador.Entities;
using Evaluacion.JCabrera.SistemaBuscador.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Repositories
{
    public class RolRepository : IRolRepository
    {
        private readonly ApplicationDbContext _context;

        public RolRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<RolListaModel>> ObtenerListaRoles()
        {
            var listaRolesDb = await _context.Roles.ToListAsync();
            var listaRolesModel = new List<RolListaModel>();

            foreach (var roldb in listaRolesDb)
            {
                var rol = new RolListaModel()
                {
                    Id = roldb.Id,
                    Nombre = roldb.Nombre
                };

                listaRolesModel.Add(rol);
            }
            

            return listaRolesModel;
        }

        public async Task InsertarRol(RolCreacionModel model)
        {
            var nuevoRol = new Rol()
            {
                Nombre = model.Nombre
            };

            _context.Roles.Add(nuevoRol);
            await _context.SaveChangesAsync();

        }

        public async Task ActualizarRol(RolActualizarModel model)
        {
            var rolDb = await _context.Roles.FirstOrDefaultAsync(x => x.Id == model.Id);
            rolDb.Nombre = model.Nombre;            
            await _context.SaveChangesAsync();
        }

        public async Task<RolActualizarModel> ObtenerRolPorId(int id)
        {
            var respuesta = new RolActualizarModel() { };
            var roldb = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id); //Linq
            if (roldb != null)
            {
                respuesta.Id = roldb.Id;
                respuesta.Nombre = roldb.Nombre;                
            }

            return respuesta;
        }
        
        public async Task EliminarRol(int id)
        {
            var rol = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();

        }
    }
}
