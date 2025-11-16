using Acessly.Domain.Entities;
using Acessly.Domain.Enums;

namespace Acessly.Domain.Interfaces
{
    public interface IEmpresaRepository : IRepository<Empresa>
    {
        Task<IEnumerable<Empresa>> GetBySetorAsync(string setor);
        Task<IEnumerable<Empresa>> GetByNivelAcessibilidadeAsync(NivelAcessibilidade nivel);
        Task<Empresa?> GetByUsuarioIdAsync(long idUsuario);
    }
}
