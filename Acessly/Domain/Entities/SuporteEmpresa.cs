namespace Acessly.Domain.Entities
{
    public class SuporteEmpresa
    {
        public long IdSuporte { get; set; }
        public long IdEmpresa { get; set; }
        public string TipoSuporte { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        public Empresa Empresa { get; set; } = null!;

        public void ValidarTipoSuporte()
        {
            if (string.IsNullOrWhiteSpace(TipoSuporte))
                throw new ArgumentException("Tipo de suporte é obrigatório");
        }
    }
}
