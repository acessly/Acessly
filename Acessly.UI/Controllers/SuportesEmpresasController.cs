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
        public async Task<IActionResult> Create(SuporteEmpresaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _client.PostAsJsonAsync("/api/suportesempresa", model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            await _client.DeleteAsync($"/api/suportesempresa/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
