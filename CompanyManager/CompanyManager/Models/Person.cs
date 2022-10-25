using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Documento")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public int DocNumber { get; set; }

        [Display(Name = "Tipo")]
        [MaxLength(10, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string DocType { get; set; }

        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string Email { get; set; }

        [Display(Name = "Apellido")]
        [MaxLength(15, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string LastName { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(15, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string Name { get; set; }

        [Display(Name = "Teléfono")]
        [MinLength(7, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string? Phone { get; set; }
    }
}
