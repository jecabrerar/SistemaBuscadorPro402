using Microsoft.EntityFrameworkCore;
using SistemaBuscador.Entities;
using SistemaBuscador.Models;
using SistemaBuscador.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ISeguridad _seguridad;

        public UsuarioRepository(ApplicationDbContext context, ISeguridad seguridad)
        {
            _context = context;
            _seguridad = seguridad;
        }
        public async Task InsertarUsuario(UsuarioCreacionModel model)
        {
            var nuevoUsuario = new Usuario() { 
                Nombres = model.Nombres,
                NombreUsuario = model.NombreUsuario,
                Apellidos = model.Apellidos,
                RolId = (int)model.RolId,
                Password = _seguridad.Encriptar(model.Password)
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync();

        }

        public async Task ActualizarUsuario(UsuarioActualizarModel model)
        {
            var usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == model.Id);
            usuarioDb.Nombres = model.Nombres;
            usuarioDb.Apellidos = model.Apellidos;
            usuarioDb.RolId = (int)model.RolId;
            await _context.SaveChangesAsync();
        }

        public async Task<List<UsuarioListaModel>> ObtenerListaUsuarios()
        {
            var respuesta = new List<UsuarioListaModel>();
            var listaBd = await _context.Usuarios.ToListAsync();

            foreach (var usuariobd in listaBd)
            {
                var newUsuarioLista = new UsuarioListaModel() { 
                    Id =  usuariobd.Id,
                    Nombres = usuariobd.Nombres,
                    Apellidos = usuariobd.Apellidos,
                    NombreUsuario = usuariobd.NombreUsuario,
                    RolId = usuariobd.RolId
                };
                respuesta.Add(newUsuarioLista);
            }

            return respuesta;
        }

        public async Task<UsuarioActualizarModel> ObtenerUsuarioPorId(int id)
        {
            var respuesta = new UsuarioActualizarModel() { };
            var usuariodb = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id); //Linq
            if (usuariodb != null)
            {
                respuesta.Id = usuariodb.Id;
                respuesta.Nombres = usuariodb.Nombres;
                respuesta.Apellidos = usuariodb.Apellidos;
                respuesta.NombreUsuario = usuariodb.NombreUsuario;
                respuesta.RolId = usuariodb.RolId;
            }

            return respuesta;
        }
        public async Task ActualizarPassword(UsuarioCambioPasswordModel model)
        {
         
            
            var usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == model.Id);
            usuarioDb.Password = _seguridad.Encriptar(model.Password);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            
        }
    }
}
