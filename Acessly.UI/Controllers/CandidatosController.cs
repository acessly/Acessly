using Acessly.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Acessly.UI.Controllers
{
    public class CandidatosController : Controller
    {
        private readonly HttpClient _client;

        public CandidatosController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("AcesslyApi");
        }

        public async Task<IActionResult> Index(string tipoDeficiencia = null)
        {
            string url = "/api/candidatos";
            if (!string.IsNullOrEmpty(tipoDeficiencia))
                url = $"/api/candidatos/deficiencia/{tipoDeficiencia}";
            var candidatos = await _client.GetFromJsonAsync<List<CandidatoViewModel>>(url);
            return View(candidatos);
        }

        public async Task<IActionResult> Details(long id)
        {
            var candidato = await _client.GetFromJsonAsync<CandidatoViewModel>($"/api/candidatos/{id}");
            return View(candidato);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CandidatoViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _client.PostAsJsonAsync("/api/candidatos", model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var candidato = await _client.GetFromJsonAsync<CandidatoViewModel>($"/api/candidatos/{id}");
            return View(candidato);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, CandidatoViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _client.PutAsJsonAsync($"/api/candidatos/{id}", model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            await _client.DeleteAsync($"/api/candidatos/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
