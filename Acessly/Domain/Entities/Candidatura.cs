using Acessly.Domain.Enums;

namespace Acessly.Domain.Entities
{
    public class Candidatura
    {
        public long IdCandidatura { get; set; }
        public long IdCandidato { get; set; }
        public long IdVaga { get; set; }
        public DateTime DataCandidatura { get; set; }
        public StatusCandidatura Status { get; set; } = StatusCandidatura.EmAnalise;

        public Candidato Candidato { get; set; } = null!;
        public Vaga Vaga { get; set; } = null!;

        public void Aprovar()
        {
            if (Status != StatusCandidatura.EmAnalise)
                throw new InvalidOperationException("Apenas candidaturas em análise podem ser aprovadas");
            Status = StatusCandidatura.Aprovado;
        }

        public void Reprovar()
        {
            if (Status != StatusCandidatura.EmAnalise)
                throw new InvalidOperationException("Apenas candidaturas em análise podem ser reprovadas");
            Status = StatusCandidatura.Reprovado;
        }
    }
}
