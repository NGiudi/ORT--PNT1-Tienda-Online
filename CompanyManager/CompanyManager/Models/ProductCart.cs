using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CompanyManager.Models
{
    public class ProductCart
    {
        public int Id { get; set; }

        [Display(Name = "Cantidad")]
        [Range(1, int.MaxValue, ErrorMessage = ErrorViewModel.StockErrorRange)]
        public int Quantity { get; set; }

        public string? Name { get; set; }

        public float Price { get; set; }

        public float UnitPrice { get; set; }

        public void SetProducto(Product p)
        {
            Id = p.Id;
            Name = p.Name;
            UnitPrice = p.Price - p.Price * p.Discount / 100;
            Price = Quantity * UnitPrice;
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
            Price = UnitPrice * quantity;
        }
    }
}