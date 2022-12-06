using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models
{
    public class CreditCard
    {
        [Key]

        public int Id { get; set; }
        
        public int SaleId { get; set; }
        // agregar sale id

        [Display(Name = "Numero de la tarjeta")]
        [MaxLength(18, ErrorMessage = ErrorViewModel.MaxCharacters)]
        [MinLength(13, ErrorMessage = ErrorViewModel.MinCharacters)]
        public int CardNumber { get; set; }

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
