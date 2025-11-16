using System.ComponentModel.DataAnnotations;

namespace Acessly.Application.DTOs.Candidato
{
    public record CandidatoCreateDto
    {
        [Required(ErrorMessage = "ID do usuário é obrigatório")]
        public long IdUsuario { get; init; }

        [Required(ErrorMessage = "Tipo de deficiência é obrigatório")]
        [RegularExpression("^(Fisica|Visual|Auditiva|Cognitiva)$",
            ErrorMessage = "Tipo deve ser 'Fisica', 'Visual', 'Auditiva' ou 'Cognitiva'")]
        public string TipoDeficiencia { get; init; } = string.Empty;

        public string? Habilidades { get; init; }

        [StringLength(255)]
        public string? Experiencia { get; init; }

        [Required(ErrorMessage = "Acessibilidade necessária é obrigatória")]
        [StringLength(255)]
        public string AcessibilidadeNecessaria { get; init; } = string.Empty;
    }
}
