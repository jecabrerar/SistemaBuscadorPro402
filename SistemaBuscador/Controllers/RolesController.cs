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
    public class RolesController : Controller
    {
        private readonly IRolRepository _repository;

        public RolesController(IRolRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var lista = await _repository.ObtenerListaRoles();

            return View(lista);
        }

        public IActionResult NuevoRol()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NuevoRol(RolCreacionModel model)
        {
            if (ModelState.IsValid)
            {
                //guardar usuario en bd
                await _repository.InsertarRol(model);
                return RedirectToAction("Index","Roles");
            }
            return View("NuevoRol", model);
        }

        public async Task<IActionResult> ActualizarRol([FromRoute] int id)
        {
            var usuario = await _repository.ObtenerRolPorId(id);
            return View("ActualizarRol", usuario);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarRol(RolActualizarModel model)
        {
            if (ModelState.IsValid)
            {
                //guardar usuario en bd
                await _repository.ActualizarRol(model);
                return RedirectToAction("Index", "Roles");
            }
            return View("ActualizarRol", model);
        }


        public async Task<IActionResult> EliminarRol(int id)
        {
            var usuario = await _repository.ObtenerRolPorId(id);

            return View("EliminarRol", usuario);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarRol(RolActualizarModel model)
        {
            await _repository.EliminarRol(model.Id);

            return RedirectToAction("Index", "Roles");
        }

    }
}
