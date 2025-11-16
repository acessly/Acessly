using Acessly.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Acessly.UI.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly HttpClient _client;

        public EmpresasController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("AcesslyApi");
        }

        public async Task<IActionResult> Index(string setor, string nivelAcessibilidade)
        {
            string url = "/api/empresas";
            if (!string.IsNullOrEmpty(setor))
                url = $"/api/empresas/setor/{setor}";
            else if (!string.IsNullOrEmpty(nivelAcessibilidade))
                url = $"/api/empresas/acessibilidade/{nivelAcessibilidade}";

            var empresas = await _client.GetFromJsonAsync<List<EmpresaViewModel>>(url);
            return View(empresas);
        }

        public async Task<IActionResult> Details(long id)
        {
            var empresa = await _client.GetFromJsonAsync<EmpresaViewModel>($"/api/empresas/{id}");
            return View(empresa);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(EmpresaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _client.PostAsJsonAsync("/api/empresas", model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var empresa = await _client.GetFromJsonAsync<EmpresaViewModel>($"/api/empresas/{id}");
            return View(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, EmpresaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _client.PutAsJsonAsync($"/api/empresas/{id}", model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            await _client.DeleteAsync($"/api/empresas/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
