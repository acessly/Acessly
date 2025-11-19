using Acessly.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Acessly.UI.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly HttpClient _client;

        public UsuariosController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("AcesslyApi");
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var usuarios = await _client.GetFromJsonAsync<List<UsuarioViewModel>>("api/usuarios");
                return View(usuarios ?? new List<UsuarioViewModel>());
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = $"Erro ao conectar com a API: {ex.Message}";
                return View(new List<UsuarioViewModel>());
            }
        }

        public IActionResult Create()
        {
            return View(new UsuarioViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var response = await _client.PostAsJsonAsync("api/usuarios", model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Erro ao criar usuário: {errorContent}");
                return View(model);
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Erro de conexão: {ex.Message}");
                return View(model);
            }
        }

        public async Task<IActionResult> Details(long id)
        {
            try
            {
                var usuario = await _client.GetFromJsonAsync<UsuarioViewModel>($"api/usuarios/{id}");
                return View(usuario);
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Edit(long id)
        {
            try
            {
                var usuario = await _client.GetFromJsonAsync<UsuarioViewModel>($"api/usuarios/{id}");
                return View(usuario);
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UsuarioViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var response = await _client.PutAsJsonAsync($"api/usuarios/{model.IdUsuario}", model);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Erro ao atualizar: {errorContent}");
                return View(model);
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Erro de conexão: {ex.Message}");
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var usuario = await _client.GetFromJsonAsync<UsuarioViewModel>($"api/usuarios/{id}");
                return View(usuario);
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                var response = await _client.DeleteAsync($"api/usuarios/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Erro ao excluir usuário";
                return View();
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = $"Erro de conexão: {ex.Message}";
                return View();
            }
        }
    }
}
