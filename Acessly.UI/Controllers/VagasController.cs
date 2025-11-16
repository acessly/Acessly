using Acessly.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Acessly.UI.Controllers
{
    public class VagasController : Controller
    {
        private readonly HttpClient _client;

        public VagasController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("AcesslyApi");
        }

        public async Task<IActionResult> Index(string titulo, string tipo, string cidade, int page = 1)
        {
            string url = $"/api/vagas/search?titulo={titulo}&tipo={tipo}&cidade={cidade}&page={page}&pageSize=10";
            var vagas = await _client.GetFromJsonAsync<List<VagaViewModel>>(url);
            return View(vagas);
        }

        public async Task<IActionResult> Details(long id)
        {
            var vaga = await _client.GetFromJsonAsync<VagaViewModel>($"/api/vagas/{id}");
            return View(vaga);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(VagaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _client.PostAsJsonAsync("/api/vagas", model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var vaga = await _client.GetFromJsonAsync<VagaViewModel>($"/api/vagas/{id}");
            return View(vaga);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, VagaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _client.PutAsJsonAsync($"/api/vagas/{id}", model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            await _client.DeleteAsync($"/api/vagas/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
