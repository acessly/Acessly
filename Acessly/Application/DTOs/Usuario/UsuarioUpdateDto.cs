using System.ComponentModel.DataAnnotations;

namespace Acessly.Application.DTOs.Usuario
{
    public record UsuarioUpdateDto
    {
        [StringLength(100, MinimumLength = 3)]
        public string? Nome { get; init; }

        [StringLength(100)]
        public string? Cidade { get; init; }

        [StringLength(50)]
        public string? Estado { get; init; }

        [Phone]
        [StringLength(20)]
        public string? Telefone { get; init; }
    }
}
