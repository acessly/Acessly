using Acessly.Domain.Enums;

namespace Acessly.Domain.Entities
{
    public class Vaga
    {
        public long IdVaga { get; set; }
        public long IdEmpresa { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public TipoVaga TipoVaga { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public decimal? Salario { get; set; }
        public string AcessibilidadeOferecida { get; set; } = string.Empty;

        public Empresa Empresa { get; set; } = null!;
        public ICollection<Candidatura> Candidaturas { get; set; } = new List<Candidatura>();

        public void ValidarSalario()
        {
            if (Salario.HasValue && Salario.Value < 0)
                throw new ArgumentException("Salário não pode ser negativo");
        }

        public bool EhRemota() => TipoVaga == TipoVaga.Remoto;
    }
}
