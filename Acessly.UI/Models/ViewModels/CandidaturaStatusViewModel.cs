using System.ComponentModel.DataAnnotations;

namespace Acessly.UI.Models.ViewModels
{
    public class CandidaturaStatusViewModel
    {
        [Required]
        [StringLength(20)]
        public string Status { get; set; }
    }
}
