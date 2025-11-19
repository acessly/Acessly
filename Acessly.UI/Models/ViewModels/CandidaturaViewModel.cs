using System.ComponentModel.DataAnnotations;

namespace Acessly.UI.Models.ViewModels
{
    public class CandidaturaViewModel
    {
        public long IdCandidatura { get; set; }

        [Required]
        public long IdCandidato { get; set; }

        [Required]
        public long IdVaga { get; set; }

        public DateTime DataCandidatura { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = string.Empty;
    }
}
