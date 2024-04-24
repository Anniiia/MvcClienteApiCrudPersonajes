using ApiCrudPersonajes.Models;
using Microsoft.AspNetCore.Mvc;
using MvcClienteApiCrudPersonajes.Services;
using System.Numerics;

namespace MvcClienteApiCrudPersonajes.Controllers
{
    public class PersonajesController : Controller
    {

        private ServiceApiPersonajes service;

        public PersonajesController(ServiceApiPersonajes service)
        {
            this.service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Personaje> personajes = await this.service.GetPersonajesAsync();
            return View(personajes);

        }
        public async Task<IActionResult> Details(int id)
        {
            Personaje personaje = await this.service.FindPersonajeAsync(id);

            return View(personaje);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(Personaje per)
        {
            await this.service.InsertPersonajesAsync(per.IdPersonaje, per.Nombre, per.Imagen, per.Serie);
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Edit(int id)
        {
            Personaje personaje = await this.service.FindPersonajeAsync(id);

            return View(personaje);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Personaje per)
        {
            await this.service.UpdatePersonajeAsync(per.IdPersonaje, per.Nombre, per.Imagen, per.Serie);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await this.service.DeletePersonajesAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Buscador()
        {
            ViewData["SERIES"] = await this.service.GetSeries();

            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Buscador(string serie)
        {
            ViewData["SERIES"] = await this.service.GetSeries();

            List<Personaje> personaje = await this.service.GetPersonajesSerieAsync(serie);
            return View(personaje);

        }
    }
}
