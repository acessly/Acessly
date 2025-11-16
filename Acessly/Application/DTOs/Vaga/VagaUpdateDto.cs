using System.ComponentModel.DataAnnotations;

namespace Acessly.Application.DTOs.Vaga
{
    public record VagaUpdateDto
    {
        [StringLength(100)]
        public string? Titulo { get; init; }

        public string? Descricao { get; init; }

        [RegularExpression("^(Remoto|Presencial|Hibrido)$")]
        public string? TipoVaga { get; init; }

        [StringLength(100)]
        public string? Cidade { get; init; }

        [StringLength(50)]
        public string? Estado { get; init; }

        [Range(0, double.MaxValue)]
        public decimal? Salario { get; init; }

        [StringLength(255)]
        public string? AcessibilidadeOferecida { get; init; }
    }
}
