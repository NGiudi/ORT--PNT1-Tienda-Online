using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace GestorEmpresa.Models
{
    public class Product
    {
        [Display(Name = "Descuento")]
        [Range(0, 100, ErrorMessage = ErrorViewModel.PorcentRange)]
        public int discount { get; set; }

        public DateTime? deleted_at;

        [Display(Name = "Descripción")]
        [MaxLength(200, ErrorMessage = ErrorViewModel.MaxCharacters)]
        public int description { get; set; }

        [Key]
        public int id { get; set; }

        public List<String>? images { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(30, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string name { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public float price { get; set; }

        [Display(Name = "Stock")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public int stock { get; set; }
    }
}
