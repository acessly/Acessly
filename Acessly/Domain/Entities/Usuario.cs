using Acessly.Domain.Enums;

namespace Acessly.Domain.Entities
{
    public class Usuario
    {
        public long IdUsuario { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public TipoUsuario TipoUsuario { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public string? Telefone { get; set; }

        public Empresa? Empresa { get; set; }
        public Candidato? Candidato { get; set; }

        public void ValidarEmail()
        {
            if (!Email.Contains("@"))
                throw new ArgumentException("Email inválido");
        }

        public void ValidarSenha()
        {
            if (Senha.Length < 6)
                throw new ArgumentException("Senha deve ter no mínimo 6 caracteres");
        }
    }
}
