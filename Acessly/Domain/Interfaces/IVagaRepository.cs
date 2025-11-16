using Acessly.Domain.Entities;
using Acessly.Domain.Enums;

namespace Acessly.Domain.Interfaces
{
    public interface IVagaRepository : IRepository<Vaga>
    {
        Task<IEnumerable<Vaga>> SearchAsync(
            string? titulo, 
            TipoVaga? tipo, 
            string? cidade, 
            int page, 
            int pageSize);
        Task<IEnumerable<Vaga>> GetByEmpresaAsync(long idEmpresa);
        Task<IEnumerable<Vaga>> GetByTipoAsync(TipoVaga tipo);
        Task<IEnumerable<Vaga>> GetByLocalizacaoAsync(string? cidade, string? estado);
        Task<int> CountAsync(string? titulo, TipoVaga? tipo, string? cidade);
    }
}
