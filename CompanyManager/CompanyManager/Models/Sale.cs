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

        public int BuyerId { get; set; }

        [Display(Name = "Fecha de Compra")]
        public DateTime? SaleDate { get; set; }

        [Display(Name = "Productos")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public List<ProductCart>? Products { get; set; }

        [Display(Name = "Total")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public float TotalPrice { get; set; }

        [Display(Name = "Numero de la tarjeta")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        [MaxLength(18, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(13, ErrorMessage = ErrorViewModel.MinCharacters)]
        public string CardNumber { get; set; }

        [Display(Name = "Nombre y apellido")]
        [MaxLength(18, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(8, ErrorMessage = ErrorViewModel.MinCharacters)]
        public string CardName { get; set; }

        public int ExpirationM { get; set; }

        // select del 1 al 12
        public int ExpirationY { get; set; }

        // select este año + 5 en adelante

        [MaxLength(4, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(3, ErrorMessage = ErrorViewModel.MinCharacters)]
        public int CardCVV { get; set; }
    }
}
