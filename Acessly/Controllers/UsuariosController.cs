using Microsoft.AspNetCore.Mvc;
using Acessly.Application.DTOs.Usuario;
using Acessly.Application.Interfaces;
using Acessly.Application.Exceptions;

namespace Acessly.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioService _service;

    public UsuariosController(IUsuarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResponseDto>>> GetAll()
    {
        var usuarios = await _service.GetAllAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioResponseDto>> GetById(long id)
    {
        var usuario = await _service.GetByIdAsync(id);
        if (usuario == null)
            return NotFound(new ProblemDetails { Title = "Usuário não encontrado", Status = 404 });
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioResponseDto>> Create(UsuarioCreateDto dto)
    {
        try
        {
            var usuario = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = usuario.IdUsuario }, usuario);
        }
        catch (BusinessException ex)
        {
            return BadRequest(new ProblemDetails { Title = "Erro de negócio", Detail = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UsuarioResponseDto>> Update(long id, UsuarioUpdateDto dto)
    {
        try
        {
            var usuario = await _service.UpdateAsync(id, dto);
            return Ok(usuario);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ProblemDetails { Title = "Usuário não encontrado", Status = 404 });
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
            return NotFound(new ProblemDetails { Title = "Usuário não encontrado", Status = 404 });
        }
    }
}
