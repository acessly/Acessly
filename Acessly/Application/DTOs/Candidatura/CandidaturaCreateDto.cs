using System.ComponentModel.DataAnnotations;

namespace Acessly.Application.DTOs.Candidatura
{
    public record CandidaturaCreateDto
    {
        [Required(ErrorMessage = "ID do candidato é obrigatório")]
        public long IdCandidato { get; init; }

        [Required(ErrorMessage = "ID da vaga é obrigatório")]
        public long IdVaga { get; init; }
    }
}
