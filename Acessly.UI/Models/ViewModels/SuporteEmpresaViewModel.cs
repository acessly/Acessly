using System.ComponentModel.DataAnnotations;

namespace Acessly.UI.Models.ViewModels
{
    public class SuporteEmpresaViewModel
    {
        public long IdSuporte { get; set; }

        [Required]
        public long IdEmpresa { get; set; }

        [Required]
        [StringLength(100)]
        public string TipoSuporte { get; set; }

        [Required]
        [StringLength(255)]
        public string Descricao { get; set; }
    }
}
