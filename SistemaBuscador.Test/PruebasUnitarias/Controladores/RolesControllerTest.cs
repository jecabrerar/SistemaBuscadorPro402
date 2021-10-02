using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Evaluacion.JCabrera.SistemaBuscador.Controllers;
using Evaluacion.JCabrera.SistemaBuscador.Models;
using Evaluacion.JCabrera.SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Test.PruebasUnitarias.Controladores
{
    [TestClass]
    public class RolesControllerTest
    {

        [TestMethod]
        public async Task NuevoRolModeloInvalido()
        {
            //Preparacion                        
            var rolService = new Mock<IRolRepository>();
            var model = new RolCreacionModel();
            var controller = new RolesController(rolService.Object);

            //Ejecucion
            controller.ModelState.AddModelError(string.Empty, "Datos Invalidos");
            var respuesta = await controller.NuevoRol(model);
            var resultado = respuesta as ViewResult;

            //validacion
            Assert.AreEqual(resultado.ViewName, "NuevoRol");
        }

        [TestMethod]
        public async Task NuevoRolModeloValido()
        {
            //Preparacion                        
            var rolService = new Mock<IRolRepository>();
            var model = new RolCreacionModel() { Nombre= "Rol 1"};
            var controller = new RolesController(rolService.Object);

            //Ejecucion
            var respuesta = await controller.NuevoRol(model);
            var resultado = respuesta as RedirectToActionResult;

            //validacion            
            Assert.AreEqual(resultado.ControllerName, "Roles");
            Assert.AreEqual(resultado.ActionName, "Index");
        }

        [TestMethod]
        public async Task ActualizarRolModeloValido()
        {
            //Preparacion                        
            var rolService = new Mock<IRolRepository>();
            var model = new RolActualizarModel() { Nombre = "Rol 2", Id = 1 };
            var controller = new RolesController(rolService.Object);

            //Ejecucion
            var respuesta = await controller.ActualizarRol(model);
            var resultado = respuesta as RedirectToActionResult;

            //validacion            
            Assert.AreEqual(resultado.ControllerName, "Roles");
            Assert.AreEqual(resultado.ActionName, "Index");
        }


        [TestMethod]
        public async Task ActualizarRolModeloInvalido()
        {
            //Preparacion                        
            var rolService = new Mock<IRolRepository>();
            var model = new RolActualizarModel();
            var controller = new RolesController(rolService.Object);

            //Ejecucion
            controller.ModelState.AddModelError(string.Empty, "Datos Invalidos");
            var respuesta = await controller.ActualizarRol(model);
            var resultado = respuesta as ViewResult;

            //validacion            
            Assert.AreEqual(resultado.ViewName, "ActualizarRol");
        }

        [TestMethod]
        public async Task EliminarRol()
        {
            //Preparacion                        
            var rolService = new Mock<IRolRepository>();
            var model = new RolActualizarModel() { Nombre = "Rol 1", Id = 1 };
            var controller = new RolesController(rolService.Object);

            //Ejecucion
            var respuesta = await controller.EliminarRol(model);
            var resultado = respuesta as RedirectToActionResult;

            //validacion            
            Assert.AreEqual(resultado.ControllerName, "Roles");
            Assert.AreEqual(resultado.ActionName, "Index");
        }
    }
}
