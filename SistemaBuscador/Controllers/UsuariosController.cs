using Microsoft.AspNetCore.Mvc;
using Evaluacion.JCabrera.SistemaBuscador.Models;
using Evaluacion.JCabrera.SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evaluacion.JCabrera.SistemaBuscador.Filters;

namespace Evaluacion.JCabrera.SistemaBuscador.Controllers
{
    [ServiceFilter(typeof(SessionFilter))]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;        

        public UsuariosController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;            
        }
        public async Task<IActionResult> Index()
        {
            var listaUsuario = await _usuarioRepository.ObtenerListaUsuarios();
            return View(listaUsuario);
        }

        public async Task<IActionResult> NuevoUsuario()
        {
            var usuarioCreacion = await _usuarioRepository.UsuarioCreacionModel();
            return View(usuarioCreacion);
        }

        [HttpPost]
        public async Task<IActionResult> NuevoUsuario(UsuarioCreacionModel model)
        {
            if (ModelState.IsValid)
            {
                //guardar usuario en bd
               await  _usuarioRepository.InsertarUsuario(model);
                return  RedirectToAction("Index","Usuarios");
            }
            return View("NuevoUsuario", model);
        }

        public async Task<IActionResult> ActualizarUsuario([FromRoute] int id)
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioPorId(id);
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarUsuario(UsuarioActualizarModel model)
        {
            if (ModelState.IsValid)
            {
                //guardar usuario en bd
                await _usuarioRepository.ActualizarUsuario(model);
                return RedirectToAction("Index", "Usuarios");
            }
            return View("ActualizarUsuario", model);
        }

        public IActionResult CambiarPassword(int id)
        {
            ViewBag.idUsuario = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CambiarPassword(UsuarioCambioPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                await _usuarioRepository.ActualizarPassword(model);
                return RedirectToAction("Index","Usuarios");
            }

            ViewBag.idUsuario = model.Id;

            return View("CambiarPassword", model);

        }

        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioPorId(id);

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarUsuario(UsuarioActualizarModel model)
        {
            await _usuarioRepository.EliminarUsuario(model.Id);

            return RedirectToAction("Index", "Usuarios");
        }



    }
}
