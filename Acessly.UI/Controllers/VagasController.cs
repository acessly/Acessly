using Acessly.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Acessly.UI.Controllers
{
    public class VagasController : Controller
    {
        private readonly HttpClient _client;

        public VagasController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("AcesslyApi");
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var vagas = await _client.GetFromJsonAsync<List<VagaViewModel>>("api/vagas");
                return View(vagas ?? new List<VagaViewModel>());
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = $"Erro ao conectar com a API: {ex.Message}";
                return View(new List<VagaViewModel>());
            }
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(VagaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                await _client.PostAsJsonAsync("api/vagas", model);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Erro ao criar vaga: {ex.Message}");
                return View(model);
            }
        }

        public async Task<IActionResult> Details(long id)
        {
            try
            {
                var vaga = await _client.GetFromJsonAsync<VagaViewModel>($"api/vagas/{id}");
                return View(vaga);
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
                var vaga = await _client.GetFromJsonAsync<VagaViewModel>($"api/vagas/{id}");
                return View(vaga);
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VagaViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                await _client.PutAsJsonAsync($"api/vagas/{model.IdVaga}", model);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Erro ao atualizar vaga: {ex.Message}");
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var vaga = await _client.GetFromJsonAsync<VagaViewModel>($"api/vagas/{id}");
                return View(vaga);
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            try
            {
                await _client.DeleteAsync($"api/vagas/{id}");
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = $"Erro ao excluir vaga: {ex.Message}";
                return View();
            }
        }
    }
}
