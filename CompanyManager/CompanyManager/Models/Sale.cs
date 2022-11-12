using CompanyManager.Models;
using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Comprador")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public Person? Buyer { get; set; }

        public DateTime? DeletedAt { get; set; }

        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public List<ProductCart>? Products { get; set; }

        [Display(Name = "Total de la venta")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public float TotalPrice { get; set; }
    }
}
