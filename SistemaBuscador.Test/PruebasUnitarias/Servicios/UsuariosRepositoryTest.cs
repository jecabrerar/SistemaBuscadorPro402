using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Evaluacion.JCabrera.SistemaBuscador.Entities;
using Evaluacion.JCabrera.SistemaBuscador.Models;
using Evaluacion.JCabrera.SistemaBuscador.Repositories;
using Evaluacion.JCabrera.SistemaBuscador.Utilidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Test.PruebasUnitarias.Servicios
{
    [TestClass]
    public class UsuariosRepositoryTest : TestBase
    {
        [TestMethod]
        public async Task InsertarUsuario()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            var seguridadService = new Mock<ISeguridad>();
            var rolService = new RolRepository(context);

            context.Roles.Add(new Rol() { Id = 1, Nombre = "Rol 1" });
            await context.SaveChangesAsync();

            var repo = new UsuarioRepository(context, seguridadService.Object, rolService);
            var modelo = new UsuarioCreacionModel() { Nombres="Nombre Test", Apellidos="Apellido test", NombreUsuario="test1", Password= seguridadService.Object.Encriptar("test"), RePassword= seguridadService.Object.Encriptar("test"), RolId=1 };

            //ejecucion
            await repo.InsertarUsuario(modelo);
            var context2 = BuildContex(nombreBd);

            var lista = await context2.Usuarios.ToListAsync();
            var resultado = lista.Count;
            //verificacion
            Assert.AreEqual(1, resultado);
        }

        [TestMethod]
        public async Task ActualizaUsuario()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            var seguridadService = new Mock<ISeguridad>();
            

            context.Roles.Add(new Rol() { Id = 1, Nombre = "Rol 1" });
            context.Roles.Add(new Rol() { Id = 2, Nombre = "Rol 2" });

            context.Usuarios.Add(new Usuario() { Id = 1, RolId=1, Nombres = "Nombre test", Apellidos= "Apellidos test", NombreUsuario="UsuarioTest", Password= seguridadService.Object.Encriptar("ClaveTest") });
            await context.SaveChangesAsync();

            var context2 = BuildContex(nombreBd);
            var rolService = new RolRepository(context2);

            var repo = new UsuarioRepository(context2, seguridadService.Object, rolService);
            var model = new UsuarioActualizarModel() { Id = 1, Nombres = "Nombre test modificado", Apellidos = "Apellidos test modificado", RolId=2 };

            //ejecucion
            await repo.ActualizarUsuario(model);
            var context3 = BuildContex(nombreBd);
            var usuarioModificado = await context3.Usuarios.FirstOrDefaultAsync(x => x.Id == 1);
            var resultado1 = usuarioModificado.Nombres;
            var resultado2 = usuarioModificado.Apellidos;            
            var resultado3 = usuarioModificado.RolId;
            
            //verificacion
            Assert.AreEqual("Nombre test modificado", resultado1);
            Assert.AreEqual("Apellidos test modificado", resultado2);            
            Assert.AreEqual(2, resultado3);
        }

        [TestMethod]
        public async Task ObtenerListaUsuarios()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            
            context.Roles.Add(new Rol() { Id = 1, Nombre = "Rol 1" });
            context.Usuarios.Add(new Usuario() { Id = 1, RolId = 1, Nombres = "Nombre test 1", Apellidos = "Apellidos test 1", NombreUsuario = "UsuarioTest1", Password = "ClaveTest1" });
            context.Usuarios.Add(new Usuario() { Id = 2, RolId = 1, Nombres = "Nombre test 2", Apellidos = "Apellidos test 2", NombreUsuario = "UsuarioTest2", Password = "ClaveTest2" });
            context.Usuarios.Add(new Usuario() { Id = 3, RolId = 1, Nombres = "Nombre test 3", Apellidos = "Apellidos test 3", NombreUsuario = "UsuarioTest2", Password = "ClaveTest3" });
            await context.SaveChangesAsync();

            var context2 = BuildContex(nombreBd);

            var seguridadService = new Mock<ISeguridad>();
            var rolService = new RolRepository(context2);

            var repo = new UsuarioRepository(context2, seguridadService.Object, rolService);

            //ejecucion
            var usuListaModels = await repo.ObtenerListaUsuarios();
            var resultado = usuListaModels.Count;

            //verificacion
            Assert.IsNotNull(usuListaModels);
            Assert.AreEqual(3, resultado);
        }

        [TestMethod]
        public async Task ObtenerUsuarioPorId()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);

            context.Roles.Add(new Rol() { Id = 1, Nombre = "Rol 1" });
            context.Roles.Add(new Rol() { Id = 2, Nombre = "Rol 2" });
            context.Roles.Add(new Rol() { Id = 3, Nombre = "Rol 3" });
            context.Roles.Add(new Rol() { Id = 4, Nombre = "Rol 4" });
            context.Usuarios.Add(new Usuario() { Id = 1, RolId = 1, Nombres = "Nombre test 1", Apellidos = "Apellidos test 1", NombreUsuario = "UsuarioTest1", Password = "ClaveTest1" });
            await context.SaveChangesAsync();
            
            var context2 = BuildContex(nombreBd);

            var seguridadService = new Mock<ISeguridad>();
            var rolService = new RolRepository(context2);
            
            var repo = new UsuarioRepository(context2, seguridadService.Object, rolService);

            //ejecucion
            var usuDeLaBd = await repo.ObtenerUsuarioPorId(1);

            //verificacion
            Assert.IsNotNull(usuDeLaBd);
        }

        [TestMethod]
        public async Task ActulizarPassword()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            var seguridadService = new Mock<ISeguridad>();

            context.Usuarios.Add(new Usuario(){
                Id = 1, 
                RolId = 1, 
                Nombres = "Nombre test", 
                Apellidos = "Apellidos test", 
                NombreUsuario = "UsuarioTest", 
                Password = seguridadService.Object.Encriptar("ClaveTest") 
            });
            await context.SaveChangesAsync();

            var rolService = new Mock<IRolRepository>();

            var context2 = BuildContex(nombreBd);
            var repo = new UsuarioRepository(context2, seguridadService.Object, rolService.Object);
            var model = new UsuarioCambioPasswordModel() { 
                Id = 1, Password = seguridadService.Object.Encriptar("ClaveTest Editada"), 
                RePassword= seguridadService.Object.Encriptar("ClaveTest Editada"
            )};

            //ejecucion
            await repo.ActualizarPassword(model);
            var context3 = BuildContex(nombreBd);
            var usuarioModificado = await context3.Usuarios.FirstOrDefaultAsync(x => x.Id == 1);
            var resultado = usuarioModificado.Password;
            

            //verificacion
            Assert.AreEqual(seguridadService.Object.Encriptar("ClaveTest Editada"), resultado);
        }

        [TestMethod]
        public async Task EliminarUsuario()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            var seguridadService = new Mock<ISeguridad>();
            var rolService = new Mock<IRolRepository>();

            context.Usuarios.Add(new Usuario()
            {
                Id = 1,
                RolId = 1,
                Nombres = "Nombre test",
                Apellidos = "Apellidos test",
                NombreUsuario = "UsuarioTest",
                Password = seguridadService.Object.Encriptar("ClaveTest")
            });
            await context.SaveChangesAsync();

            var context2 = BuildContex(nombreBd);
            var repo = new UsuarioRepository(context2, seguridadService.Object, rolService.Object);

            //ejecucion
            await repo.EliminarUsuario(1);
            var context3 = BuildContex(nombreBd);
            var resultado = await context3.Usuarios.FirstOrDefaultAsync(x => x.Id == 1);

            //verificacion
            Assert.IsNull(resultado);
        }
    }
}
