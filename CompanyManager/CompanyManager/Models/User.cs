using System.ComponentModel.DataAnnotations;

namespace GestorEmpresa.Models
{
    public class User : Person
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Usuario")]
        [MaxLength(15, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string username { get; set; }

        [Display(Name = "Contraseña")]
        [MaxLength(20, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(8, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string password { get; set; }

        public UserRoles role { get; set; }
    }
}
