using System.ComponentModel.DataAnnotations;

namespace Acessly.UI.Models.ViewModels
{
    public class VagaViewModel
    {
        public long IdVaga { get; set; }

        [Required]
        public long IdEmpresa { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [StringLength(1000)]
        public string Descricao { get; set; }

        [Required]
        [StringLength(30)]
        public string TipoVaga { get; set; }

        [StringLength(100)]
        public string Cidade { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        [Range(0, 1000000)]
        public decimal Salario { get; set; }

        [Required]
        [StringLength(255)]
        public string AcessibilidadeOferecida { get; set; }
    }
}
