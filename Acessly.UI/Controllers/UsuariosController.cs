using Acessly.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Acessly.UI.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly HttpClient _client;

        public UsuariosController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("AcesslyApi");
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _client.GetFromJsonAsync<List<UsuarioViewModel>>("/api/usuarios");
            return View(usuarios);
        }

        public async Task<IActionResult> Details(long id)
        {
            var usuario = await _client.GetFromJsonAsync<UsuarioViewModel>($"/api/usuarios/{id}");
            return View(usuario);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _client.PostAsJsonAsync("/api/usuarios", model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var usuario = await _client.GetFromJsonAsync<UsuarioViewModel>($"/api/usuarios/{id}");
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, UsuarioViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _client.PutAsJsonAsync($"/api/usuarios/{id}", model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            await _client.DeleteAsync($"/api/usuarios/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
