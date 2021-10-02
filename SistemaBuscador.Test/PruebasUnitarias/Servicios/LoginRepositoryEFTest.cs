using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Evaluacion.JCabrera.SistemaBuscador.Entities;
using Evaluacion.JCabrera.SistemaBuscador.Repositories;
using Evaluacion.JCabrera.SistemaBuscador.Utilidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Evaluacion.JCabrera.SistemaBuscador.Test.PruebasUnitarias.Servicios
{
    [TestClass]
    public class LoginRepositoryEFTest :TestBase
    {
        [TestMethod]        
        public async Task UsuarioNoExiste()
        {
            //Preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            
            context.Usuarios.Add(new Usuario() { NombreUsuario = "Usuario1", Password="Password1" });
            await context.SaveChangesAsync();

            var context2 = BuildContex(nombreBd);
            var securidad = new Mock<ISeguridad>();
            securidad.Setup(x => x.Encriptar(It.IsAny<string>())).Returns("aaabbgggffjjyyuoosaddsddsasashjkjk");

            //Ejecucion

            var nombreUsuario = "Usuario2";
            var password = "Password2";
            var repo = new LoginRepositoryEF(context2, securidad.Object);
            var respuesta = await repo.UserExist(nombreUsuario, password);

            //Verificacion

            Assert.IsFalse(respuesta);

        }

        [TestMethod]
        public async Task UsuarioExiste()
        {
            //Preparacion
            var nombreBd = Guid.NewGuid().ToString();
            var context = BuildContex(nombreBd);
            context.Usuarios.Add(new Usuario() { NombreUsuario = "Usuario1", Password = "aaabbgggffjjyyuoosaddsddsasashjkjk" });
            await context.SaveChangesAsync();

            var context2 = BuildContex(nombreBd);
            var securidad = new Mock<ISeguridad>();
            securidad.Setup(x => x.Encriptar(It.IsAny<string>())).Returns("aaabbgggffjjyyuoosaddsddsasashjkjk");
            //Ejecucion

            var nombreUsuario = "Usuario1";
            var password = "Password1";
            var repo = new LoginRepositoryEF(context2, securidad.Object);
            var respuesta = await repo.UserExist(nombreUsuario, password);

            //Verificacion

            Assert.IsTrue(respuesta);

        }
    }
}
