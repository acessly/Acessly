using System.ComponentModel.DataAnnotations;

namespace Acessly.Application.DTOs.Usuario
{
    public record UsuarioCreateDto
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; init; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [StringLength(100)]
        public string Email { get; init; } = string.Empty;

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
        [StringLength(255)]
        public string Senha { get; init; } = string.Empty;

        [Required(ErrorMessage = "Tipo de usuário é obrigatório")]
        [RegularExpression("^(Candidato|Empresa)$", ErrorMessage = "Tipo deve ser 'Candidato' ou 'Empresa'")]
        public string TipoUsuario { get; init; } = string.Empty;

        [StringLength(100)]
        public string? Cidade { get; init; }

        [StringLength(50)]
        public string? Estado { get; init; }

        [Phone(ErrorMessage = "Telefone inválido")]
        [StringLength(20)]
        public string? Telefone { get; init; }
    }
}
