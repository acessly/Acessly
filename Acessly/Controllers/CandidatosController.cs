using Microsoft.AspNetCore.Mvc;
using Acessly.Application.DTOs.Candidato;
using Acessly.Application.Interfaces;
using Acessly.Application.Exceptions;

namespace Acessly.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidatosController : ControllerBase
{
    private readonly ICandidatoService _service;

    public CandidatosController(ICandidatoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CandidatoResponseDto>>> GetAll()
    {
        var candidatos = await _service.GetAllAsync();
        return Ok(candidatos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CandidatoResponseDto>> GetById(long id)
    {
        var candidato = await _service.GetByIdAsync(id);
        if (candidato == null)
            return NotFound(new ProblemDetails { Title = "Candidato não encontrado", Status = 404 });
        return Ok(candidato);
    }

    [HttpPost]
    public async Task<ActionResult<CandidatoResponseDto>> Create(CandidatoCreateDto dto)
    {
        try
        {
            var candidato = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = candidato.IdCandidato }, candidato);
        }
        catch (BusinessException ex)
        {
            return BadRequest(new ProblemDetails { Title = "Erro de negócio", Detail = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CandidatoResponseDto>> Update(long id, CandidatoUpdateDto dto)
    {
        try
        {
            var candidato = await _service.UpdateAsync(id, dto);
            return Ok(candidato);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ProblemDetails { Title = "Candidato não encontrado", Status = 404 });
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
            return NotFound(new ProblemDetails { Title = "Candidato não encontrado", Status = 404 });
        }
    }

    [HttpGet("deficiencia/{tipo}")]
    public async Task<ActionResult<IEnumerable<CandidatoResponseDto>>> GetByTipoDeficiencia(string tipo)
    {
        var candidatos = await _service.GetByTipoDeficienciaAsync(tipo);
        return Ok(candidatos);
    }
}
