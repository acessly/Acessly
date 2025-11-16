using Acessly.Application.DTOs.Candidato;

namespace Acessly.Application.Interfaces
{
    public interface ICandidatoService
    {
        Task<CandidatoResponseDto> CreateAsync(CandidatoCreateDto dto);
        Task<CandidatoResponseDto?> GetByIdAsync(long id);
        Task<IEnumerable<CandidatoResponseDto>> GetAllAsync();
        Task<CandidatoResponseDto> UpdateAsync(long id, CandidatoUpdateDto dto);
        Task DeleteAsync(long id);
        Task<IEnumerable<CandidatoResponseDto>> GetByTipoDeficienciaAsync(string tipo);
    }
}
