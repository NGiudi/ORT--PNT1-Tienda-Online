using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "Stock Actualizado")]
        public int CurrentStock { get; set; }

        [Display(Name = "Actualizacion de Stock")]
        public int StockUpdate { get; set; }

        [Display(Name = "Fecha Actualizacion")]
        public DateTime UpdatedAt { get; set; }

        [Display(Name = "Razon")]
        public string Reason { get; set; }
    }
}
