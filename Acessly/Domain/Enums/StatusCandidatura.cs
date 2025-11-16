using System.Runtime.Serialization;

namespace Acessly.Domain.Enums
{
    public enum StatusCandidatura
    {
        [EnumMember(Value = "Em Analise")]
        EmAnalise,

        [EnumMember(Value = "Aprovado")]
        Aprovado,

        [EnumMember(Value = "Reprovado")]
        Reprovado
    }
}
