namespace Acessly.Application.DTOs.SuporteEmpresa
{
    public record SuporteEmpresaResponseDto
    {
        public long IdSuporte { get; init; }
        public long IdEmpresa { get; init; }
        public string TipoSuporte { get; init; } = string.Empty;
        public string Descricao { get; init; } = string.Empty;
    }
}
