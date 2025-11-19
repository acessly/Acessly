    using Acessly.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Acessly.UI.Controllers
{
    public class CandidaturasController : Controller
    {
        private readonly HttpClient _client;

        public CandidaturasController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("AcesslyApi");
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var candidaturas = await _client.GetFromJsonAsync<List<CandidaturaViewModel>>("api/candidaturas");
                return View(candidaturas ?? new List<CandidaturaViewModel>());
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = $"Erro ao conectar com a API: {ex.Message}";
                return View(new List<CandidaturaViewModel>());
            }
        }

        public IActionResult Create()
        {
            return View(new CandidaturaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CandidaturaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var response = await _client.PostAsJsonAsync("api/candidaturas", model);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Erro ao criar candidatura: {errorContent}");
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
                var candidatura = await _client.GetFromJsonAsync<CandidaturaViewModel>($"api/candidaturas/{id}");
                return View(candidatura);
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
                var candidatura = await _client.GetFromJsonAsync<CandidaturaViewModel>($"api/candidaturas/{id}");
                return View(candidatura);
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CandidaturaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var response = await _client.PutAsJsonAsync($"api/candidaturas/{model.IdCandidatura}", model);
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Erro ao atualizar candidatura: {errorContent}");
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
                var candidatura = await _client.GetFromJsonAsync<CandidaturaViewModel>($"api/candidaturas/{id}");
                return View(candidatura);
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
                var response = await _client.DeleteAsync($"api/candidaturas/{id}");
                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                ViewBag.Error = "Erro ao excluir candidatura.";
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
