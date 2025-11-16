using Acessly.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Acessly.UI.Controllers
{
    public class CandidaturasController : Controller
    {
        private readonly HttpClient _client;

        public CandidaturasController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("AcesslyApi");
        }

        public async Task<IActionResult> Index()
        {
            var candidaturas = await _client.GetFromJsonAsync<List<CandidaturaViewModel>>("/api/candidaturas");
            return View(candidaturas);
        }

        public async Task<IActionResult> Details(long id)
        {
            var candidatura = await _client.GetFromJsonAsync<CandidaturaViewModel>($"/api/candidaturas/{id}");
            return View(candidatura);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CandidaturaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            await _client.PostAsJsonAsync("/api/candidaturas", model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(long id, CandidaturaStatusViewModel status)
        {
            if (!ModelState.IsValid) return View(status);
            await _client.PutAsJsonAsync($"/api/candidaturas/{id}/status", status);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            await _client.DeleteAsync($"/api/candidaturas/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}
