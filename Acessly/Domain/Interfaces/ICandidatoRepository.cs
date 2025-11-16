using Acessly.Domain.Entities;
using Acessly.Domain.Enums;

namespace Acessly.Domain.Interfaces
{
    public interface ICandidatoRepository : IRepository<Candidato>
    {
        Task<IEnumerable<Candidato>> GetByTipoDeficienciaAsync(TipoDeficiencia tipo);
        Task<Candidato?> GetByUsuarioIdAsync(long idUsuario);
        Task<IEnumerable<Candidato>> GetByHabilidadeAsync(string habilidade);
    }
}
