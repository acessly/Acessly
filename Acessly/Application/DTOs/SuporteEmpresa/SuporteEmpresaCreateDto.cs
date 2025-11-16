using System.ComponentModel.DataAnnotations;

namespace Acessly.Application.DTOs.SuporteEmpresa
{
    public record SuporteEmpresaCreateDto
    {
        [Required(ErrorMessage = "ID da empresa é obrigatório")]
        public long IdEmpresa { get; init; }

        [Required(ErrorMessage = "Tipo de suporte é obrigatório")]
        [StringLength(100)]
        public string TipoSuporte { get; init; } = string.Empty;

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(255)]
        public string Descricao { get; init; } = string.Empty;
    }
}
