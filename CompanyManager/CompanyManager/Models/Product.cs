using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(30, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string Name { get; set; }
        
        public DateTime? DeletedAt { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(200, ErrorMessage = ErrorViewModel.MaxCharacters)]
        public string? Description { get; set; }

        [Display(Name = "Imagen")]
        public string? Image { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public float Price { get; set; }
        
        [Display(Name = "Descuento")]
        [Range(0, 100, ErrorMessage = ErrorViewModel.PorcentRange)]
        public int Discount { get; set; }

        [Display(Name = "Stock")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public int Stock { get; set; }
    }
}
