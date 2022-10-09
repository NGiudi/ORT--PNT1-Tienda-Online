using System.ComponentModel.DataAnnotations;

namespace GestorEmpresa.Models
{
    public class Sale
    {
        [Display(Name = "Comprador")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public Person buyer { get; set; }

        [Key]
        public int id { get; set; }

        public List<Product> products { get; set; }

        [Display(Name = "Total de la venta")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public float total_price { get; set; }

        [Display(Name = "Tracking id")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public int tracking_number { get; set; }

        public User seller{ get; set; }
    }
}
