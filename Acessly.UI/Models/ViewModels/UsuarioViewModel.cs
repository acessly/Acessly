using System.ComponentModel.DataAnnotations;

namespace Acessly.UI.Models.ViewModels
{
    public class UsuarioViewModel
    {
        public long IdUsuario { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(128)]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoUsuario { get; set; }

        [StringLength(100)]
        public string Cidade { get; set; }

        [StringLength(50)]
        public string Estado { get; set; }

        [StringLength(20)]
        public string Telefone { get; set; }
    }
}
