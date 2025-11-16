using Acessly.Application.DTOs.Candidatura;

namespace Acessly.Application.Interfaces
{
    public interface ICandidaturaService
    {
        Task<CandidaturaResponseDto> CreateAsync(CandidaturaCreateDto dto);
        Task<CandidaturaResponseDto?> GetByIdAsync(long id);
        Task<IEnumerable<CandidaturaResponseDto>> GetAllAsync();
        Task<CandidaturaResponseDto> UpdateStatusAsync(long id, CandidaturaUpdateStatusDto dto);
        Task DeleteAsync(long id);
        Task<IEnumerable<CandidaturaResponseDto>> GetByCandidatoAsync(long idCandidato);
        Task<IEnumerable<CandidaturaResponseDto>> GetByVagaAsync(long idVaga);
    }
}
