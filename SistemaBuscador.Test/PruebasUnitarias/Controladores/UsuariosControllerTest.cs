using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Evaluacion.JCabrera.SistemaBuscador.Controllers;
using Evaluacion.JCabrera.SistemaBuscador.Entities;
using Evaluacion.JCabrera.SistemaBuscador.Models;
using Evaluacion.JCabrera.SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Test.PruebasUnitarias.Controladores
{
    [TestClass]
    public class UsuariosControllerTest : TestBase
    {
        [TestMethod]
        public async Task NuevoUsuarioModeloInvalido()
        {
            //Preparacion            
            var usuarioService = new Mock<IUsuarioRepository>();
            var model = new UsuarioCreacionModel();
            var controller = new UsuariosController(usuarioService.Object);

            //Ejecucion
            controller.ModelState.AddModelError(string.Empty, "Datos Invalidos");
            
            var respuesta = await controller.NuevoUsuario(model);
            var resultado = respuesta as ViewResult;

            //validacion            
            Assert.AreEqual(resultado.ViewName, "NuevoUsuario");
        }

        [TestMethod]
        public async Task NuevoUsuarioModeloValido()
        {
            //Preparacion
            var usuarioService = new Mock<IUsuarioRepository>();
            var model = new UsuarioCreacionModel() { NombreUsuario = "usuario 1", Nombres = "Nombre 1", Apellidos = "Apellido 1", Password = "12345", RePassword = "12345", RolId=1 };
            var controller = new UsuariosController(usuarioService.Object);

            //Ejecucion
            var respuesta = await controller.NuevoUsuario(model);
            var resultado = respuesta as RedirectToActionResult;

            //validacion            
            Assert.AreEqual(resultado.ControllerName, "Usuarios");
            Assert.AreEqual(resultado.ActionName, "Index");
        }

        [TestMethod]
        public async Task ActualizarUsuarioModeloValido()
        {
            //Preparacion                        
            var usuarioService = new Mock<IUsuarioRepository>();      
            var model = new UsuarioActualizarModel() { NombreUsuario = "usuario 1", Nombres = "Nombre 1", Apellidos = "Apellido 1", Id=1, RolId = 1};
            var controller = new UsuariosController(usuarioService.Object);

            //Ejecucion
            var respuesta = await controller.ActualizarUsuario(model);
            var resultado = respuesta as RedirectToActionResult;

            //validacion
            Assert.AreEqual(resultado.ControllerName, "Usuarios");
            Assert.AreEqual(resultado.ActionName, "Index");
        }

        [TestMethod]
        public async Task ActualizarUsuarioModeloInvalido()
        {
            //Preparacion                        
            var usuarioService = new Mock<IUsuarioRepository>();
            var model = new UsuarioActualizarModel();
            var controller = new UsuariosController(usuarioService.Object);

            //Ejecucion
            controller.ModelState.AddModelError(string.Empty, "Datos Invalidos");
            var respuesta = await controller.ActualizarUsuario(model);
            var resultado = respuesta as ViewResult;

            //validacion
            Assert.AreEqual(resultado.ViewName, "ActualizarUsuario");
        }

        [TestMethod]
        public async Task EliminarUsuario()
        {
            //Preparacion                        
            var usuarioService = new Mock<IUsuarioRepository>();
            var model = new UsuarioActualizarModel() { NombreUsuario = "usuario 1", Nombres = "Nombre 1", Apellidos = "Apellido 1", Id = 1, RolId = 1 };
            var controller = new UsuariosController(usuarioService.Object);

            //Ejecucion
            var respuesta = await controller.EliminarUsuario(model);
            var resultado = respuesta as RedirectToActionResult;

            //validacion
            Assert.AreEqual(resultado.ControllerName, "Usuarios");
            Assert.AreEqual(resultado.ActionName, "Index");
        }

        [TestMethod]
        public async Task CambiarPasswordModeloValido()
        {
            //Preparacion                        
            var usuarioService = new Mock<IUsuarioRepository>();
            var model = new UsuarioCambioPasswordModel() { Password="Password 1", RePassword="Password 1", Id=1};
            var controller = new UsuariosController(usuarioService.Object);

            //Ejecucion
            var respuesta = await controller.CambiarPassword(model);
            var resultado = respuesta as RedirectToActionResult;

            //validacion
            Assert.AreEqual(resultado.ControllerName, "Usuarios");
            Assert.AreEqual(resultado.ActionName, "Index");
        }

        [TestMethod]
        public async Task CambiarPasswordModeloInvalido()
        {
            //Preparacion                        
            var usuarioService = new Mock<IUsuarioRepository>();
            var model = new UsuarioCambioPasswordModel();
            var controller = new UsuariosController(usuarioService.Object);

            //Ejecucion
            controller.ModelState.AddModelError(string.Empty, "Datos Invalidos");
            var respuesta = await controller.CambiarPassword(model);
            var resultado = respuesta as ViewResult;

            //validacion            
            Assert.AreEqual(resultado.ViewName, "CambiarPassword");
        }
    }
}
