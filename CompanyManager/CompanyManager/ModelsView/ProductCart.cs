using CompanyManager.Models;

namespace CompanyManager.ModelsView
{
    public class ProductCart
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public float UnitPrice { get; set; }


        //public ProductCart()
        //{
        //    this.Id = 0;
        //    this.Description = "";
        //    this.Quantity = 0;
        //    this.Name = "";
        //    this.Price = 0;
        //}

        //public ProductCart(Product product) {
        //    this.Id = product.Id;
        //    this.Description = product.Description;
        //    this.Quantity = 0;
        //    this.Name = product.Name;
        //    this.Price = product.Price - (product.Price * product.Discount / 100);
        //}

        public void SetProducto(Product p)
        {
            this.Id = p.Id;
            this.Name = p.Name;
            this.UnitPrice = p.Price - (p.Price * p.Discount / 100);
            this.Price = this.Quantity * this.UnitPrice;
        }

        public void SetQuantity(int quantity) {
            this.Quantity = quantity;
            this.Price = this.UnitPrice * quantity;
        }
    }
}