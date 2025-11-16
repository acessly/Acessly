using System.ComponentModel.DataAnnotations;

namespace Acessly.Application.DTOs.Candidato
{
    public record CandidatoUpdateDto
    {
        [RegularExpression("^(Fisica|Visual|Auditiva|Cognitiva)$")]
        public string? TipoDeficiencia { get; init; }

        public string? Habilidades { get; init; }

        [StringLength(255)]
        public string? Experiencia { get; init; }

        [StringLength(255)]
        public string? AcessibilidadeNecessaria { get; init; }
    }
}
