using System.ComponentModel.DataAnnotations;

namespace Acessly.Application.DTOs.Empresa
{
    public record EmpresaCreateDto
    {
        [Required(ErrorMessage = "ID do usuário é obrigatório")]
        public long IdUsuario { get; init; }

        [Required(ErrorMessage = "Nome da empresa é obrigatório")]
        [StringLength(100)]
        public string Nome { get; init; } = string.Empty;

        [Required(ErrorMessage = "Setor é obrigatório")]
        [StringLength(100)]
        public string Setor { get; init; } = string.Empty;

        [Required(ErrorMessage = "Nível de acessibilidade é obrigatório")]
        [RegularExpression("^(Baixo|Medio|Alto)$", ErrorMessage = "Nível deve ser 'Baixo', 'Medio' ou 'Alto'")]
        public string NivelAcessibilidade { get; init; } = string.Empty;

        [Url(ErrorMessage = "URL inválida")]
        [StringLength(255)]
        public string? Site { get; init; }

        public string? Descricao { get; init; }
    }
}
