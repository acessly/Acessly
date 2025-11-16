namespace Acessly.Application.DTOs.Empresa
{
    public record EmpresaResponseDto
    {
        public long IdEmpresa { get; init; }
        public long IdUsuario { get; init; }
        public string Nome { get; init; } = string.Empty;
        public string Setor { get; init; } = string.Empty;
        public string NivelAcessibilidade { get; init; } = string.Empty;
        public string? Site { get; init; }
        public string? Descricao { get; init; }
    }
}
