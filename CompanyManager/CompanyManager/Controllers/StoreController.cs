using CompanyManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CompanyManager.Controllers
{
    public class StoreController : Controller
    {
        private readonly CMContext _context;
        private static int cartItems = 0;

        public StoreController(CMContext context) {
            _context = context;
        }

        // Vista de listado de productos.
        public async Task<IActionResult> Index() {
            var productsList = await _context.Product.Where(p => p.Stock > 0).ToListAsync();
            return View(productsList);
        }

        //Crea ViewBag del producto para mostrar los detalles
        private void GenerateProductViewBag(Product product)
        {
            ViewBag.Name = product.Name;
            ViewBag.Description = product.Description;
            ViewBag.Image = product.Image;
            ViewBag.Stock = product.Stock;
            ViewBag.OriginalPrice = product.Price;
            ViewBag.Discount = product.Discount;
        }

        public static int ViewCartItems()
        {
            return cartItems;
        }

        // Vista detalle de producto.
        public async Task<IActionResult> Details(int? id) {
            var product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);

            if (product == null) {
                return NotFound();
            }

            GenerateProductViewBag(product);

            ProductCart productCart = new ProductCart()
            {
                ProductId = product.Id,
                Product = product,
                Quantity = 1,
                UnitPrice = product.CalculateDiscount(),
            };

            return View(productCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // Al agregar producto en el carrito, luego de apretar en el botón agregar al carrito.
        public async Task<IActionResult> Details(ProductCart model) {
            var product = await _context.Product.Where(p => p.Id == model.ProductId).FirstOrDefaultAsync();

            if (product == null) {
                return NotFound();
            }

            ModelState.Remove("Quantity");
            GenerateProductViewBag(product);
            model.SetProducto(product);

            if (ModelState.IsValid) {
                if (productHaveStock(model).Result) {
                    model.Id = 0;
                    AddProductToCart(model);
                    return RedirectToAction(nameof(Cart));
                }

                // Error falta de stock.
                ModelState.AddModelError("Quantity", ErrorViewModel.InsufficientStock);
            }
            
            return View(model);
        }

        // Vistas listado de productos en el carrito.
        public IActionResult Cart()
        {
            var model = ProductsInCart;
            return View(model);
        }

        // Cuando el usuario concretar la compra, lugeo de apretar en el botón de finalzar compra.
        [Authorize]
        public async Task<IActionResult> Sale()
        {
            User? findUser = _context.User.Where(u => u.Id == int.Parse(HttpContext.User.Identity.Name)).FirstOrDefault();
            
            var sale = new Sale()
            {
                Buyer = findUser,
                BuyerId = findUser.Id,
                Products = ProductsInCart,
                TotalPrice = calculateTotalSale(ProductsInCart),
                SaleDate = DateTime.Now,
            };

            if (sale.TotalPrice > 0) {
                _context.Add(sale);
                await _context.SaveChangesAsync();
                cartItems = 0;

                // Actualizar los stocks.
                foreach (ProductCart p in sale.Products) {
                    UpdateStock(p.ProductId, p.Quantity);
                }

                //Vaciar carrito.
                ProductsInCart = new List<ProductCart>();
            }

            return RedirectToAction(nameof(Index));
        }

        // Al eliminar producto del carrito, luego de apretar en el botón eliminar.
        public IActionResult DeleteProductInCart(int id) {
            var carrito = ProductsInCart;
            var productoExistente = carrito.Where(o => o.ProductId == id).FirstOrDefault();

            //Si el producto no esta, lo agrego, sino remplazo la cantidad
            if (productoExistente != null) {
                carrito.Remove(productoExistente);
                cartItems--;
                ProductsInCart = carrito;
            }

            return RedirectToAction(nameof(Cart));
        }

        // Carrito de productos.
        public List<ProductCart> ProductsInCart {
            get
            {
                var value = HttpContext.Session.GetString("Productos");

                if (value == null)
                    return new List<ProductCart>();
                else
                    return JsonConvert.DeserializeObject<List<ProductCart>>(value);
            }
            set
            {
                var js = JsonConvert.SerializeObject(value);
                HttpContext.Session.SetString("Productos", js);
            }
        }

        // Método para validar que exista el stock del producto.
        private async Task<Boolean> productHaveStock(ProductCart pCarrito) {
            var p = await _context.Product.FirstOrDefaultAsync((p) => p.Id == pCarrito.ProductId);

            if (p == null) {
                return false;
            }

            return (p.Stock >= pCarrito.Quantity);
        }

        
        // Método para calcular el total de la lista de productos.
        private float calculateTotalSale(List<ProductCart> list) {
            float totalSale = 0;

            foreach (ProductCart p in list) {
                totalSale += p.getTotalPrice();
            }

            return totalSale;
        }

        // Método para agregar producto en el carrito.
        private void AddProductToCart(ProductCart pCarrito) {
            var carrito = ProductsInCart;
            var pExistente = carrito.Where(o => o.ProductId == pCarrito.ProductId).FirstOrDefault();

            // Si el producto no esta, lo agrego, sino remplazo la cantidad.
            if(pCarrito.Quantity > 0)
            {
                if (pExistente == null) {
                    carrito.Add(pCarrito);
                    cartItems++;
                }
                else {
                    pExistente.Quantity = pCarrito.Quantity;
                }
            }
            ProductsInCart = carrito;
        }
        
        // Método para quitar los productos comprados.
        private async void UpdateStock(int id, int quantity)
        {
            Product? findProduct = _context.Product.Where(p => p.Id == id).FirstOrDefault();

            if (findProduct != null) {
                findProduct.Stock -= quantity;
                _context.Update(findProduct);
                await _context.SaveChangesAsync();
            }
        }

    }
}
