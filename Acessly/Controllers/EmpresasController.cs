using Microsoft.AspNetCore.Mvc;
using Acessly.Application.DTOs.Empresa;
using Acessly.Application.Interfaces;
using Acessly.Application.Exceptions;

namespace Acessly.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpresasController : ControllerBase
{
    private readonly IEmpresaService _service;

    public EmpresasController(IEmpresaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmpresaResponseDto>>> GetAll()
    {
        var empresas = await _service.GetAllAsync();
        return Ok(empresas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmpresaResponseDto>> GetById(long id)
    {
        var empresa = await _service.GetByIdAsync(id);
        if (empresa == null)
            return NotFound(new ProblemDetails { Title = "Empresa não encontrada", Status = 404 });
        return Ok(empresa);
    }

    [HttpPost]
    public async Task<ActionResult<EmpresaResponseDto>> Create(EmpresaCreateDto dto)
    {
        try
        {
            var empresa = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = empresa.IdEmpresa }, empresa);
        }
        catch (BusinessException ex)
        {
            return BadRequest(new ProblemDetails { Title = "Erro de negócio", Detail = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EmpresaResponseDto>> Update(long id, EmpresaUpdateDto dto)
    {
        try
        {
            var empresa = await _service.UpdateAsync(id, dto);
            return Ok(empresa);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ProblemDetails { Title = "Empresa não encontrada", Status = 404 });
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
            return NotFound(new ProblemDetails { Title = "Empresa não encontrada", Status = 404 });
        }
    }

    [HttpGet("setor/{setor}")]
    public async Task<ActionResult<IEnumerable<EmpresaResponseDto>>> GetBySetor(string setor)
    {
        var empresas = await _service.GetBySetorAsync(setor);
        return Ok(empresas);
    }

    [HttpGet("acessibilidade/{nivel}")]
    public async Task<ActionResult<IEnumerable<EmpresaResponseDto>>> GetByNivelAcessibilidade(string nivel)
    {
        var empresas = await _service.GetByNivelAcessibilidadeAsync(nivel);
        return Ok(empresas);
    }
}
