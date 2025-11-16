using Microsoft.AspNetCore.Mvc;
using Acessly.Application.DTOs.Candidatura;
using Acessly.Application.Interfaces;
using Acessly.Application.Exceptions;

namespace Acessly.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CandidaturasController : ControllerBase
{
    private readonly ICandidaturaService _service;

    public CandidaturasController(ICandidaturaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CandidaturaResponseDto>>> GetAll()
    {
        var candidaturas = await _service.GetAllAsync();
        return Ok(candidaturas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CandidaturaResponseDto>> GetById(long id)
    {
        var candidatura = await _service.GetByIdAsync(id);
        if (candidatura == null)
            return NotFound(new ProblemDetails { Title = "Candidatura não encontrada", Status = 404 });
        return Ok(candidatura);
    }

    [HttpPost]
    public async Task<ActionResult<CandidaturaResponseDto>> Create(CandidaturaCreateDto dto)
    {
        try
        {
            var candidatura = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = candidatura.IdCandidatura }, candidatura);
        }
        catch (BusinessException ex)
        {
            return BadRequest(new ProblemDetails { Title = "Erro de negócio", Detail = ex.Message });
        }
    }

    [HttpPut("{id}/status")]
    public async Task<ActionResult<CandidaturaResponseDto>> UpdateStatus(long id, CandidaturaUpdateStatusDto dto)
    {
        try
        {
            var candidatura = await _service.UpdateStatusAsync(id, dto);
            return Ok(candidatura);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ProblemDetails { Title = "Candidatura não encontrada", Status = 404 });
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
            return NotFound(new ProblemDetails { Title = "Candidatura não encontrada", Status = 404 });
        }
    }

    [HttpGet("candidato/{idCandidato}")]
    public async Task<ActionResult<IEnumerable<CandidaturaResponseDto>>> GetByCandidato(long idCandidato)
    {
        var candidaturas = await _service.GetByCandidatoAsync(idCandidato);
        return Ok(candidaturas);
    }

    [HttpGet("vaga/{idVaga}")]
    public async Task<ActionResult<IEnumerable<CandidaturaResponseDto>>> GetByVaga(long idVaga)
    {
        var candidaturas = await _service.GetByVagaAsync(idVaga);
        return Ok(candidaturas);
    }
}
