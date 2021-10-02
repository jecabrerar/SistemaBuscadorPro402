using Microsoft.EntityFrameworkCore;
using Evaluacion.JCabrera.SistemaBuscador;
using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluacion.JCabrera.SistemaBuscador.Test
{
    public class TestBase
    {
        protected ApplicationDbContext BuildContex(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(dbName).Options;

            var dbContext = new ApplicationDbContext(options);

            return dbContext;
        }
    }
}
