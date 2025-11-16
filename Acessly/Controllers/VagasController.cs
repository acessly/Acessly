using Microsoft.AspNetCore.Mvc;
using Acessly.Application.DTOs.Common;
using Acessly.Application.DTOs.Vaga;
using Acessly.Application.Interfaces;
using Acessly.Application.Exceptions;

namespace Acessly.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VagasController : ControllerBase
{
    private readonly IVagaService _service;

    public VagasController(IVagaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VagaResponseDto>>> GetAll()
    {
        var vagas = await _service.GetAllAsync();
        return Ok(vagas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VagaResponseDto>> GetById(long id)
    {
        var vaga = await _service.GetByIdAsync(id);
        if (vaga == null)
            return NotFound(new ProblemDetails { Title = "Vaga não encontrada", Status = 404 });
        return Ok(vaga);
    }

    [HttpPost]
    public async Task<ActionResult<VagaResponseDto>> Create(VagaCreateDto dto)
    {
        try
        {
            var vaga = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = vaga.IdVaga }, vaga);
        }
        catch (BusinessException ex)
        {
            return BadRequest(new ProblemDetails { Title = "Erro de negócio", Detail = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<VagaResponseDto>> Update(long id, VagaUpdateDto dto)
    {
        try
        {
            var vaga = await _service.UpdateAsync(id, dto);
            return Ok(vaga);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ProblemDetails { Title = "Vaga não encontrada", Status = 404 });
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
            return NotFound(new ProblemDetails { Title = "Vaga não encontrada", Status = 404 });
        }
    }

    [HttpGet("search")]
    public async Task<ActionResult<PagedResultDto<VagaResponseDto>>> Search(
        [FromQuery] string? titulo,
        [FromQuery] string? tipo,
        [FromQuery] string? cidade,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _service.SearchAsync(titulo, tipo, cidade, page, pageSize);
        return Ok(result);
    }

    [HttpGet("empresa/{idEmpresa}")]
    public async Task<ActionResult<IEnumerable<VagaResponseDto>>> GetByEmpresa(long idEmpresa)
    {
        var vagas = await _service.GetByEmpresaAsync(idEmpresa);
        return Ok(vagas);
    }
}
