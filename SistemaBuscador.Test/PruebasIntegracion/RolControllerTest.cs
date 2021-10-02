using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using Evaluacion.JCabrera.SistemaBuscador.Controllers;
using Evaluacion.JCabrera.SistemaBuscador.Models;
using Evaluacion.JCabrera.SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Evaluacion.JCabrera.SistemaBuscador.Entities;

namespace Evaluacion.JCabrera.SistemaBuscador.Test.PruebasIntegracion
{
    [TestClass]
    public class RolControllerTest : TestBase
    {
        [TestMethod]
        public async Task InsertarRol() 
        { 
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            var rolService = new RolRepository(context);
            var model = new RolCreacionModel() { Nombre = "Rol Test" };

            var controller = new RolesController(rolService);
            
            //ejecucion
            await controller.NuevoRol(model);
            var context2 = BuildContex(nombreBd);
            var lista = await context2.Roles.ToListAsync();
            var resultado = lista.Count;
            //verificacion
            Assert.AreEqual(1, resultado);
        }

        [TestMethod]
        public async Task ActualizarRol()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            var rolService = new RolRepository(context);
            
            var rol = new Rol() { Nombre = "Rol Test" };
            context.Roles.Add(rol);
            await context.SaveChangesAsync();

            var model = new RolActualizarModel() { Nombre = "Rol Test Modificado", Id =1 };
            var controller = new RolesController(rolService);

            //ejecucion
            await controller.ActualizarRol(model);
            var context2 = BuildContex(nombreBd);
            var roldb = await context2.Roles.FirstOrDefaultAsync(x=> x.Id == 1);
            var resultado = roldb.Nombre;
            
            //verificacion
            Assert.AreEqual("Rol Test Modificado", resultado);
        }

    }
}
