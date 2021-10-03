using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Repositories
{
    public interface ILoginRepository
    {
        Task<bool> UserExist(string usuario, string password);

        void SetSessionAndCookie(HttpContext context);
        void CloseSessionAndCookie(HttpContext context);
    }
}
