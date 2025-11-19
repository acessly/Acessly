using Acessly.UI.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Acessly.UI.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly HttpClient _client;

        public EmpresasController(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("AcesslyApi");
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var empresas = await _client.GetFromJsonAsync<List<EmpresaViewModel>>("api/empresas");
                return View(empresas ?? new List<EmpresaViewModel>());
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = $"Erro ao conectar com a API: {ex.Message}";
                return View(new List<EmpresaViewModel>());
            }
        }

        public IActionResult Create()
        {
            var model = new EmpresaViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmpresaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _client.PostAsJsonAsync("api/empresas", model);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Erro ao criar empresa: {ex.Message}");
                return View(model);
            }
        }

        public async Task<IActionResult> Details(long id)
        {
            try
            {
                var empresa = await _client.GetFromJsonAsync<EmpresaViewModel>($"api/empresas/{id}");
                return View(empresa);
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
                var empresa = await _client.GetFromJsonAsync<EmpresaViewModel>($"api/empresas/{id}");
                return View(empresa);
            }
            catch (HttpRequestException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, EmpresaViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _client.PutAsJsonAsync($"api/empresas/{id}", model);
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError("", $"Erro ao atualizar empresa: {ex.Message}");
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var empresa = await _client.GetFromJsonAsync<EmpresaViewModel>($"api/empresas/{id}");
                return View(empresa);
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
                await _client.DeleteAsync($"api/empresas/{id}");
                return RedirectToAction(nameof(Index));
            }
            catch (HttpRequestException ex)
            {
                ViewBag.Error = $"Erro ao excluir empresa: {ex.Message}";
                return View();
            }
        }
    }
}
