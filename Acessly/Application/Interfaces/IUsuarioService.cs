using Acessly.Application.DTOs.Usuario;

namespace Acessly.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioResponseDto> CreateAsync(UsuarioCreateDto dto);
        Task<UsuarioResponseDto?> GetByIdAsync(long id);
        Task<IEnumerable<UsuarioResponseDto>> GetAllAsync();
        Task<UsuarioResponseDto> UpdateAsync(long id, UsuarioUpdateDto dto);
        Task DeleteAsync(long id);
        Task<UsuarioResponseDto?> GetByEmailAsync(string email);
    }
}
