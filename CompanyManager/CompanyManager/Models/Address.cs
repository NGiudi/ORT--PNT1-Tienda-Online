using System.ComponentModel.DataAnnotations;

namespace GestorEmpresa.Models
{
    public class Address
    {
        [Display(Name = "Provincia")]
        [MaxLength(40, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(5, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string city { get; set; }

        [Display(Name = "Departamento")]
        [MaxLength(4, ErrorMessage = ErrorViewModel.MaxCharacters)]
        public int? department { get; set; }

        [Display(Name = "Piso")]
        [MaxLength(2, ErrorMessage = ErrorViewModel.MaxCharacters)]
        public int? floor { get; set; }

        [Display(Name = "Altura")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public int street_number { get; set; }

        [Display(Name = "Calle")]
        [MaxLength(50, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string street_name { get; set; }

        [Display(Name = "Código Postal")]
        [MaxLength(5, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string zip_code { get; set; }
    }
}
