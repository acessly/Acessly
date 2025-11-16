namespace Acessly.Application.DTOs.Candidatura
{
    public record CandidaturaResponseDto
    {
        public long IdCandidatura { get; init; }
        public long IdCandidato { get; init; }
        public long IdVaga { get; init; }
        public DateTime DataCandidatura { get; init; }
        public string Status { get; init; } = string.Empty;
    }
}
