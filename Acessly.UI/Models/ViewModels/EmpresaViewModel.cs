using System.ComponentModel.DataAnnotations;

namespace Acessly.UI.Models.ViewModels
{
    public class EmpresaViewModel
    {
        public long IdEmpresa { get; set; }

        [Required]
        public long IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string Setor { get; set; }

        [Required]
        [StringLength(50)]
        public string NivelAcessibilidade { get; set; }

        [StringLength(255)]
        [Url]
        public string Site { get; set; }

        [StringLength(1000)]
        public string Descricao { get; set; }
    }
}
