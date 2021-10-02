using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Evaluacion.JCabrera.SistemaBuscador.Entities;
using Evaluacion.JCabrera.SistemaBuscador.Models;
using Evaluacion.JCabrera.SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Test.PruebasUnitarias.Servicios
{
    [TestClass]
    public class RolRepositorioTest : TestBase
    {
        [TestMethod]
        public async Task InsertarRol()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            var repo = new RolRepository(context);

            var modelo = new RolCreacionModel() { Nombre="Rol test"};


            //ejecucion
            await repo.InsertarRol(modelo);
            var context2 = BuildContex(nombreBd);
            var lista = await context2.Roles.ToListAsync();
            var resultado = lista.Count;
            //verificacion
            Assert.AreEqual(1, resultado);

        }

        [TestMethod]
        public async Task ObtenerRolPorId()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            
            context.Roles.Add(new Rol() { Nombre = "Rol 1" });
            await context.SaveChangesAsync();

            var context2 = BuildContex(nombreBd);
            var repo = new RolRepository(context2);            

            //ejecucion
            var rolDeLaBd = await repo.ObtenerRolPorId(1);
            
            //verificacion
            Assert.IsNotNull(rolDeLaBd);
        }

        [TestMethod]
        public async Task ObtenerListaRoles()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);

            context.Roles.Add(new Rol() { Nombre = "Rol 1" });
            context.Roles.Add(new Rol() { Nombre = "Rol 2" });
            context.Roles.Add(new Rol() { Nombre = "Rol 3" });
            await context.SaveChangesAsync();

            var context2 = BuildContex(nombreBd);
            var repo = new RolRepository(context2);

            //ejecucion
            var rolListaModels = await repo.ObtenerListaRoles();
            var resultado = rolListaModels.Count;

            //verificacion
            Assert.IsNotNull(rolListaModels);
            Assert.AreEqual(3, resultado);
        }

        [TestMethod]
        public async Task ActualizaRol()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);

            context.Roles.Add(new Rol() { Nombre = "Rol 1" });
            await context.SaveChangesAsync();

            var context2 = BuildContex(nombreBd);
            var repo = new RolRepository(context2);
            var model = new RolActualizarModel() { Id = 1, Nombre = "Rol Modificado" };

            //ejecucion
            await repo.ActualizarRol(model);
            var context3 = BuildContex(nombreBd);
            var rolModificado = await context3.Roles.FirstOrDefaultAsync(x => x.Id == 1);
            var resultado = rolModificado.Nombre;
            //verificacion
            Assert.AreEqual("Rol Modificado", resultado);
        }

        [TestMethod]
        public async Task EliminarRol()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);

            context.Roles.Add(new Rol() { Nombre = "Rol 1" });
            await context.SaveChangesAsync();

            var context2 = BuildContex(nombreBd);
            var repo = new RolRepository(context2);            

            //ejecucion
            await repo.EliminarRol(1);            
            var context3 = BuildContex(nombreBd);
            var resultado = await context3.Roles.FirstOrDefaultAsync(x => x.Id == 1);
            
            //verificacion
            Assert.IsNull(resultado);
        }
    }
}
