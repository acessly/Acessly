using Acessly.Domain.Entities;
using Acessly.Domain.Enums;

namespace Acessly.Domain.Interfaces
{
    public interface ICandidaturaRepository : IRepository<Candidatura>
    {
        Task<IEnumerable<Candidatura>> GetByCandidatoAsync(long idCandidato);
        Task<IEnumerable<Candidatura>> GetByVagaAsync(long idVaga);
        Task<IEnumerable<Candidatura>> GetByStatusAsync(StatusCandidatura status);
        Task<bool> ExisteCandidaturaAsync(long idCandidato, long idVaga);
        Task<IEnumerable<Candidatura>> GetByCandidatoAndStatusAsync(long idCandidato, StatusCandidatura status);
    }
}
