using Evaluacion.JCabrera.SistemaBuscador.Controllers;
using Evaluacion.JCabrera.SistemaBuscador.Entities;
using Evaluacion.JCabrera.SistemaBuscador.Models;
using Evaluacion.JCabrera.SistemaBuscador.Repositories;
using Evaluacion.JCabrera.SistemaBuscador.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Test.PruebasIntegracion
{
    [TestClass]
    public class UsuariosControllerTest : TestBase
    {
        [TestMethod]
        public async Task InsertarUsuario()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            var seguridadService = new Seguridad();
            var rolService = new RolRepository(context);

            context.Roles.Add(new Rol() { Id = 1, Nombre = "Rol 1" });
            await context.SaveChangesAsync();

            var usuarioService = new UsuarioRepository(context, seguridadService, rolService);
            var model = new UsuarioCreacionModel() { NombreUsuario = "user1", Apellidos = "apellido 1", Nombres="Usuario 1 Test", Password= "password1", RePassword= "password1", RolId = 1 };

            var controller = new UsuariosController(usuarioService);

            //ejecucion
            await controller.NuevoUsuario(model);
            var context2 = BuildContex(nombreBd);
            var lista = await context2.Usuarios.ToListAsync();
            var resultado = lista.Count;
            //verificacion
            Assert.AreEqual(1, resultado);
        }

        [TestMethod]
        public async Task ActualizarUsuario()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            var seguridadService = new Seguridad();
            var rolService = new RolRepository(context);
            var usuarioService = new UsuarioRepository(context, seguridadService, rolService);

            context.Roles.Add(new Rol() { Id = 1, Nombre = "Rol 1" });
            var usuario = new Usuario() { NombreUsuario = "user1", Apellidos = "apellido 1", Nombres = "Usuario 1 Test", Password = "password1", RolId = 1};
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            var model = new UsuarioActualizarModel() { Id = 1, NombreUsuario = "user1", Apellidos = "apellido 1 edit", Nombres = "Usuario 1 Test edit", RolId = 1};
            var controller = new UsuariosController(usuarioService);

            //ejecucion
            await controller.ActualizarUsuario(model);
            var context2 = BuildContex(nombreBd);
            var usudb = await context2.Usuarios.FirstOrDefaultAsync(x => x.Id == 1);
            var resultado = usudb.NombreUsuario;
            var resultado1 = usudb.Nombres;
            var resultado2 = usudb.Apellidos;
            
            //verificacion            
            Assert.AreEqual("Usuario 1 Test edit", resultado1);
            Assert.AreEqual("apellido 1 edit", resultado2);
        }

        [TestMethod]
        public async Task EliminarUsuario()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            var seguridadService = new Seguridad();
            var rolService = new RolRepository(context);
            var usuarioService = new UsuarioRepository(context, seguridadService, rolService);

            context.Roles.Add(new Rol() { Id = 1, Nombre = "Rol 1" });
            var usuario = new Usuario() { NombreUsuario = "user1", Apellidos = "apellido 1", Nombres = "Usuario 1 Test", Password = "password1", RolId = 1 };
            context.Usuarios.Add(usuario);


            await context.SaveChangesAsync();

            var model = new UsuarioActualizarModel() { Id = 1, NombreUsuario = "user1", Apellidos = "apellido 1", Nombres = "Usuario 1 Test" };
            var controller = new UsuariosController(usuarioService);

            //ejecucion
            var respuesta = await controller.EliminarUsuario(model);
            var resultado1 = respuesta as RedirectToActionResult;

            var context2 = BuildContex(nombreBd);
            var resultado = await context2.Usuarios.FirstOrDefaultAsync(x => x.Id == 1);

            //verificacion
            Assert.IsNull(resultado);
            Assert.AreEqual(resultado1.ControllerName, "Usuarios");
            Assert.AreEqual(resultado1.ActionName, "Index");
        }

        [TestMethod]
        public async Task ActualizarUsuario_Id()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            var seguridadService = new Seguridad();
            var rolService = new RolRepository(context);
            var usuarioService = new UsuarioRepository(context, seguridadService, rolService);

            var rol = new Rol() { Nombre = "Rol Test" };
            context.Roles.Add(rol);
            var usuario = new Usuario() { NombreUsuario = "user1", Apellidos = "apellido 1", Nombres = "Usuario 1 Test", Password = "password1", RolId = 1 };
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            var controller = new UsuariosController(usuarioService);

            //ejecucion
            var respuesta = await controller.ActualizarUsuario(1);
            var resultado = respuesta as ViewResult;

            //verificacion
            Assert.AreEqual(resultado.ViewName, "ActualizarUsuario");
        }


        [TestMethod]
        public async Task EliminarUsuario_Id()
        {
            //preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            var seguridadService = new Seguridad();
            var rolService = new RolRepository(context);
            var usuarioService = new UsuarioRepository(context, seguridadService, rolService);

            var rol = new Rol() { Nombre = "Rol Test" };
            context.Roles.Add(rol);
            var usuario = new Usuario() { NombreUsuario = "user1", Apellidos = "apellido 1", Nombres = "Usuario 1 Test", Password = "password1", RolId = 1 };
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();

            var controller = new UsuariosController(usuarioService);

            //ejecucion
            var respuesta = await controller.EliminarUsuario(1);
            var resultado = respuesta as ViewResult;

            //verificacion
            Assert.AreEqual(resultado.ViewName, "EliminarUsuario");
        }

    }
}
