namespace Acessly.Application.DTOs.Candidato
{
    public record CandidatoResponseDto
    {
        public long IdCandidato { get; init; }
        public long IdUsuario { get; init; }
        public string TipoDeficiencia { get; init; } = string.Empty;
        public string? Habilidades { get; init; }
        public string? Experiencia { get; init; }
        public string AcessibilidadeNecessaria { get; init; } = string.Empty;
    }
}
