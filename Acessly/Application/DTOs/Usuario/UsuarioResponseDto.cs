namespace Acessly.Application.DTOs.Usuario
{
    public record UsuarioResponseDto
    {
        public long IdUsuario { get; init; }
        public string Nome { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string TipoUsuario { get; init; } = string.Empty;
        public string? Cidade { get; init; }
        public string? Estado { get; init; }
        public string? Telefone { get; init; }
    }
}
