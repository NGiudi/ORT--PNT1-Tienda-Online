using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models
{
    public class Person
    {
        public Address address { get; set; }

        [Display(Name = "Documento")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public int doc_number { get; set; }

        [Display(Name = "Tipo")]
        [MaxLength(10, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string doc_type { get; set; }

        [Display(Name = "Email")]
        [MaxLength(50, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string email { get; set; }

        [Key]
        public int id { get; set; }

        [Display(Name = "Apellido")]
        [MaxLength(15, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string last_name { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(15, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string name { get; set; }

        [Display(Name = "Teléfono")]
        [MinLength(7, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string? phone { get; set; }
    }
}
