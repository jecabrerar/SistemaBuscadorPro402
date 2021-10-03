using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Evaluacion.JCabrera.SistemaBuscador.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Repositories
{
    public class LoginRepositoryEF : ILoginRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ISeguridad _seguridad;
        private readonly string  SESSION_NAME = "sessionId";

        public LoginRepositoryEF(ApplicationDbContext context, ISeguridad seguridad)
        {
            _context = context;
            _seguridad = seguridad;
        }
        public void SetSessionAndCookie(HttpContext context)
        {
            Guid sessionId = Guid.NewGuid();
            context.Session.SetString(SESSION_NAME, sessionId.ToString());
            context.Response.Cookies.Append(SESSION_NAME, sessionId.ToString());
        }
        

        public async Task<bool> UserExist(string usuario, string password)
        {
            var resultado = false;
            var usuarioDB = await _context.Usuarios.FirstOrDefaultAsync(x=> x.NombreUsuario == usuario && x.Password == _seguridad.Encriptar(password));
            

            if (usuarioDB != null)
            {   
                resultado = true;
            }

            return resultado;
        }

        void ILoginRepository.CloseSessionAndCookie(HttpContext context)
        {
            context.Session.Remove(SESSION_NAME);
            context.Response.Cookies.Delete(SESSION_NAME);
        }
    }
}
