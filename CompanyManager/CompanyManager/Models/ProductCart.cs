using System.ComponentModel.DataAnnotations;

namespace CompanyManager.Models
{
    public class ProductCart
    {
        [Key]
        public int Id { get; set; }
        
        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public int SaleId { get; set; }

        [Display(Name = "Cantidad")]
        [Range(1, int.MaxValue, ErrorMessage = ErrorViewModel.StockErrorRange)]
        public int Quantity { get; set; }

        public string? Name { get; set; }

        public float UnitPrice { get; set; }

        public void SetProducto(Product p)
        {
            ProductId = p.Id;
            Name = p.Name;
            UnitPrice = p.Price - p.Price * p.Discount / 100;
        }

        public float getTotalPrice() {
            return UnitPrice * Quantity;
        }
    }
}