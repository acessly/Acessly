using Acessly.Domain.Entities;

namespace Acessly.Domain.Interfaces
{
    public interface ISuporteEmpresaRepository : IRepository<SuporteEmpresa>
    {
        Task<IEnumerable<SuporteEmpresa>> GetByEmpresaAsync(long idEmpresa);
        Task<IEnumerable<SuporteEmpresa>> GetByTipoSuporteAsync(string tipoSuporte);
        Task<int> CountByEmpresaAsync(long idEmpresa);
    }
}
