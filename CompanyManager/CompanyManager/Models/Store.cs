using System.ComponentModel.DataAnnotations;

namespace GestorEmpresa.Models
{
    public class Store
    {
        [Key]
        public int id { get; set; }

        [Display(Name = "Nombre de la tienda")]
        [MaxLength(30, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public string name { get; set; }

        public List<Product> products { get; set; }

        public List<Sale> sales { get; set; }
    }
}
