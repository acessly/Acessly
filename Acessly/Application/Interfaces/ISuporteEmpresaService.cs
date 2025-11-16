using Acessly.Application.DTOs.SuporteEmpresa;

namespace Acessly.Application.Interfaces
{
    public interface ISuporteEmpresaService
    {
        Task<SuporteEmpresaResponseDto> CreateAsync(SuporteEmpresaCreateDto dto);
        Task<SuporteEmpresaResponseDto?> GetByIdAsync(long id);
        Task<IEnumerable<SuporteEmpresaResponseDto>> GetAllAsync();
        Task DeleteAsync(long id);
        Task<IEnumerable<SuporteEmpresaResponseDto>> GetByEmpresaAsync(long idEmpresa);
    }
}
