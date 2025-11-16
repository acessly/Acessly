using Microsoft.AspNetCore.Mvc;
using Acessly.Application.DTOs.SuporteEmpresa;
using Acessly.Application.Interfaces;
using Acessly.Application.Exceptions;

namespace Acessly.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuportesEmpresaController : ControllerBase
{
    private readonly ISuporteEmpresaService _service;

    public SuportesEmpresaController(ISuporteEmpresaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SuporteEmpresaResponseDto>>> GetAll()
    {
        var suportes = await _service.GetAllAsync();
        return Ok(suportes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SuporteEmpresaResponseDto>> GetById(long id)
    {
        var suporte = await _service.GetByIdAsync(id);
        if (suporte == null)
            return NotFound(new ProblemDetails { Title = "Suporte não encontrado", Status = 404 });
        return Ok(suporte);
    }

    [HttpPost]
    public async Task<ActionResult<SuporteEmpresaResponseDto>> Create(SuporteEmpresaCreateDto dto)
    {
        try
        {
            var suporte = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = suporte.IdSuporte }, suporte);
        }
        catch (BusinessException ex)
        {
            return BadRequest(new ProblemDetails { Title = "Erro de negócio", Detail = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ProblemDetails { Title = "Suporte não encontrado", Status = 404 });
        }
    }

    [HttpGet("empresa/{idEmpresa}")]
    public async Task<ActionResult<IEnumerable<SuporteEmpresaResponseDto>>> GetByEmpresa(long idEmpresa)
    {
        var suportes = await _service.GetByEmpresaAsync(idEmpresa);
        return Ok(suportes);
    }
}
