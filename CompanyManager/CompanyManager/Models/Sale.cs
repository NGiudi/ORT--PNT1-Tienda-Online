using CompanyManager.Models;
using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models
{
    public class Sale
    {
        [Display(Name = "Comprador")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public Person buyer { get; set; }

        public DateTime? deleted_at;

        [Key]
        public int id { get; set; }

        public List<SaleProduct> products { get; set; }

        [Display(Name = "Total de la venta")]
        [Required(ErrorMessage = ErrorViewModel.RequiredField)]
        public float total_price { get; set; }

        public User seller { get; set; }
    }
}
