using System.ComponentModel.DataAnnotations;

namespace Acessly.Application.DTOs.Vaga
{
    public record VagaCreateDto
    {
        [Required(ErrorMessage = "ID da empresa é obrigatório")]
        public long IdEmpresa { get; init; }

        [Required(ErrorMessage = "Título é obrigatório")]
        [StringLength(100)]
        public string Titulo { get; init; } = string.Empty;

        public string? Descricao { get; init; }

        [Required(ErrorMessage = "Tipo de vaga é obrigatório")]
        [RegularExpression("^(Remoto|Presencial|Hibrido)$",
            ErrorMessage = "Tipo deve ser 'Remoto', 'Presencial' ou 'Hibrido'")]
        public string TipoVaga { get; init; } = string.Empty;

        [StringLength(100)]
        public string? Cidade { get; init; }

        [StringLength(50)]
        public string? Estado { get; init; }

        [Range(0, double.MaxValue, ErrorMessage = "Salário deve ser maior ou igual a zero")]
        public decimal? Salario { get; init; }

        [Required(ErrorMessage = "Acessibilidade oferecida é obrigatória")]
        [StringLength(255)]
        public string AcessibilidadeOferecida { get; init; } = string.Empty;
    }
}
