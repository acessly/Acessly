using System.ComponentModel.DataAnnotations;

namespace Acessly.Application.DTOs.Candidatura
{
    public record CandidaturaUpdateStatusDto
    {
        [Required(ErrorMessage = "Status é obrigatório")]
        [RegularExpression("^(EmAnalise|Aprovado|Reprovado)$",
        ErrorMessage = "Status deve ser 'EmAnalise', 'Aprovado' ou 'Reprovado'")]
        public string Status { get; init; } = string.Empty;
    }
}
