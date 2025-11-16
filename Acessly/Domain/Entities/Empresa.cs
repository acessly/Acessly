using System.Security.Cryptography.X509Certificates;
using Acessly.Domain.Enums;

namespace Acessly.Domain.Entities
{
    public class Empresa
    {
        public long IdEmpresa { get; set; }
        public long IdUsuario { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Setor { get; set; } = string.Empty;
        public NivelAcessibilidade NivelAcessibilidade { get; set; }
        public string? Site { get; set; }
        public string? Descricao { get; set; }

        public Usuario Usuario { get; set; } = null!;
        public ICollection<Vaga> Vagas { get; set; } = new List<Vaga>();
        public ICollection<SuporteEmpresa> Suportes { get; set; } = new List<SuporteEmpresa>();

        public void ValidarNome()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new ArgumentException("Nome da empresa é obrigatório");
        }

        public bool PossuiAltaAcessibilidade() => NivelAcessibilidade == NivelAcessibilidade.Alto;
    }
}
