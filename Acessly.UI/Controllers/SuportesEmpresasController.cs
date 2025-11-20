using Acessly.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Acessly.UI.Controllers
{
    public class SuportesEmpresasController : Controller
    {
        private readonly HttpClient _client;

        public SuportesEmpresasController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("AcesslyApi");
        }

        public async Task<IActionResult> Index(long? idEmpresa = null)
        {
            string url = "/api/suportesempresa";
            if (idEmpresa.HasValue)
                url = $"/api/suportesempresa/empresa/{idEmpresa.Value}";

            var suportes = await _client.GetFromJsonAsync<List<SuporteEmpresaViewModel>>(url);
            return View(suportes);
        }

        public async Task<IActionResult> Details(long id)
        {
            var suporte = await _client.GetFromJsonAsync<SuporteEmpresaViewModel>($"/api/suportesempresa/{id}");
            return View(suporte);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SuporteEmpresaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var response = await _client.PostAsJsonAsync("/api/suportesempresa", model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Erro ao criar suporte: {error}");
            return View(model);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(long id)
        {
            var suporte = await _client.GetFromJsonAsync<SuporteEmpresaViewModel>($"/api/suportesempresa/{id}");
            if (suporte == null)
                return NotFound();

            return View(suporte);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SuporteEmpresaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var response = await _client.PutAsJsonAsync($"/api/suportesempresa/{model.IdSuporte}", model);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));

            var error = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, $"Erro ao atualizar suporte: {error}");
            return View(model);
        }

        public async Task<IActionResult> Delete(long id)
        {
            await _client.DeleteAsync($"/api/suportesempresa/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
