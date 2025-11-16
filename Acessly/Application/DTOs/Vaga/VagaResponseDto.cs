namespace Acessly.Application.DTOs.Vaga
{
    public record VagaResponseDto
    {
        public long IdVaga { get; init; }
        public long IdEmpresa { get; init; }
        public string Titulo { get; init; } = string.Empty;
        public string? Descricao { get; init; }
        public string TipoVaga { get; init; } = string.Empty;
        public string? Cidade { get; init; }
        public string? Estado { get; init; }
        public decimal? Salario { get; init; }
        public string AcessibilidadeOferecida { get; init; } = string.Empty;
    }
}
