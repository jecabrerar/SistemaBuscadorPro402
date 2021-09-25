using Microsoft.AspNetCore.Mvc;
using SistemaBuscador.Models;
using SistemaBuscador.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaBuscador.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolRepository _rolRepository;

        public UsuariosController(IUsuarioRepository usuarioRepository, IRolRepository rolRepository)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
        }
        public async Task<IActionResult> Index()
        {
            var listaUsuario = await _usuarioRepository.ObtenerListaUsuarios();
            return View(listaUsuario);
        }

        public IActionResult NuevoUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NuevoUsuario(UsuarioCreacionModel model)
        {
            if (ModelState.IsValid)
            {
                //guardar usuario en bd
               await  _usuarioRepository.InsertarUsuario(model);
                return  RedirectToAction("Index");
            }
            return View(model);
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
                return RedirectToAction("Index");
            }
            return View(model);
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
                return RedirectToAction("Index");
            }

            ViewBag.idUsuario = model.Id;

            return View(model);

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

            return RedirectToAction("Index");
        }



    }
}
