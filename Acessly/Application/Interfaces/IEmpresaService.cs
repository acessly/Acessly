using Acessly.Application.DTOs.Empresa;

namespace Acessly.Application.Interfaces
{
    public interface IEmpresaService
    {
        Task<EmpresaResponseDto> CreateAsync(EmpresaCreateDto dto);
        Task<EmpresaResponseDto?> GetByIdAsync(long id);
        Task<IEnumerable<EmpresaResponseDto>> GetAllAsync();
        Task<EmpresaResponseDto> UpdateAsync(long id, EmpresaUpdateDto dto);
        Task DeleteAsync(long id);
        Task<IEnumerable<EmpresaResponseDto>> GetBySetorAsync(string setor);
        Task<IEnumerable<EmpresaResponseDto>> GetByNivelAcessibilidadeAsync(string nivel);
    }
}
