using System.ComponentModel.DataAnnotations;

namespace Acessly.UI.Models.ViewModels
{
    public class CandidatoViewModel
    {
        public long IdCandidato { get; set; }

        [Required]
        public long IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string TipoDeficiencia { get; set; }

        [StringLength(500)]
        public string Habilidades { get; set; }

        [StringLength(255)]
        public string Experiencia { get; set; }

        [Required]
        [StringLength(255)]
        public string AcessibilidadeNecessaria { get; set; }
    }
}
