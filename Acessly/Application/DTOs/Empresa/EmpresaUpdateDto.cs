using System.ComponentModel.DataAnnotations;

namespace Acessly.Application.DTOs.Empresa
{
    public record EmpresaUpdateDto
    {
        [StringLength(100)]
        public string? Nome { get; init; }

        [StringLength(100)]
        public string? Setor { get; init; }

        [RegularExpression("^(Baixo|Medio|Alto)$")]
        public string? NivelAcessibilidade { get; init; }

        [Url]
        [StringLength(255)]
        public string? Site { get; init; }

        public string? Descricao { get; init; }
    }
}
