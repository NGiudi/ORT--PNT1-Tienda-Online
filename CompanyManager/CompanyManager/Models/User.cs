using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models
{
    public class User : Person
    {
        public DateTime? DeletedAt;

        [Display(Name = "Usuario")]
        [MaxLength(15, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string Username { get; set; }

        [Display(Name = "Contraseña")]
        [MaxLength(20, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(8, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string Password { get; set; }

        public UserRoles Role { get; set; }
    }
}
