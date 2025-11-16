using Acessly.Domain.Enums;

namespace Acessly.Domain.Entities
{
    public class Candidato
    {
        public long IdCandidato { get; set; }
        public long IdUsuario { get; set; }
        public TipoDeficiencia TipoDeficiencia { get; set; }
        public string? Habilidades { get; set; }
        public string? Experiencia { get; set; }
        public string? AcessibilidadeNecessaria { get; set; } = string.Empty;

        public Usuario Usuario { get; set; } = null!;
        public ICollection<Candidatura> Candidaturas { get; set; } = new List<Candidatura>();

        public void ValidarAcessibilidadeNecessaria()
        {
            if (string.IsNullOrWhiteSpace(AcessibilidadeNecessaria))
                throw new ArgumentException("Acessibilidade necessária deve ser informada");     
        }

        public bool TemExperiencia() => !string.IsNullOrWhiteSpace(Experiencia);
    }
}
