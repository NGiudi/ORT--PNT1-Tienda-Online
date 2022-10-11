using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models
{
    public class Address
    {
        [Display(Name = "Provincia")]
        [MaxLength(40, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(5, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string City { get; set; }

        [Display(Name = "Departamento")]
        [MaxLength(4, ErrorMessage = ErrorViewModel.MaxCharacters)]
        public int? Department { get; set; }

        [Display(Name = "Piso")]
        [MaxLength(2, ErrorMessage = ErrorViewModel.MaxCharacters)]
        public int? Floor { get; set; }

        [Display(Name = "Altura")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public int StreetNumber { get; set; }

        [Display(Name = "Calle")]
        [MaxLength(50, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string? StreetName { get; set; }

        [Display(Name = "Código Postal")]
        [MaxLength(5, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string ZipCode { get; set; }
    }
}
