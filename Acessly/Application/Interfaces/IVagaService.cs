using Acessly.Application.DTOs.Common;
using Acessly.Application.DTOs.Vaga;

namespace Acessly.Application.Interfaces
{
    public interface IVagaService
    {
        Task<VagaResponseDto> CreateAsync(VagaCreateDto dto);
        Task<VagaResponseDto?> GetByIdAsync(long id);
        Task<PagedResultDto<VagaResponseDto>> SearchAsync(
            string? titulo,
            string? tipo,
            string? cidade,
            int page = 1,
            int pageSize = 10);
        Task<VagaResponseDto> UpdateAsync(long id, VagaUpdateDto dto);
        Task DeleteAsync(long id);
        Task<IEnumerable<VagaResponseDto>> GetByEmpresaAsync(long idEmpresa);
    }
}
