using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace CompanyManager.Controllers
{
    [Authorize(Roles = "ADMIN,SELLER")]
    public class SalesController : Controller
    {
        private readonly CMContext _context;

        public SalesController(CMContext context)
        {
            _context = context;
        }

        private bool SaleExists(int id)
        {
            return _context.Sale.Any(e => e.Id == id);
        }

        // Vista de listado de ventas.
        public async Task<IActionResult> Index(int? id)
        {
            var sales = from s in _context.Sale select s;
            if (id != null)
            {
                var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == id);
                ViewBag.Product = product.Name;
                sales = sales.Where(s => s.Products.Any(pc => pc.ProductId == id));
            }

            // Asigna el usuario a la venta.
            foreach (var s in sales)
            {
                var buyer = await _context.User.FirstOrDefaultAsync(e => e.Id == s.BuyerId);
                s.Buyer = buyer;
            }
            return View(await sales.ToListAsync());
        }

        // Vista detalle de la venta.
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sale == null) {
                return NotFound();
            }

            var sale = await _context.Sale.FirstOrDefaultAsync(m => m.Id == id);

            if (sale == null) {
                return NotFound();
            }

            var buyer = await _context.User.FirstOrDefaultAsync(e => e.Id == sale.BuyerId);
            sale.Buyer = buyer;

            // get product cart list.
            List<ProductCart>? list = await _context.ProductCart.Where(pc => pc.SaleId == id).ToListAsync();
            sale.Products = list;

            // get product item.
            foreach (ProductCart pc in list) {
                Product? product = await _context.Product.FirstOrDefaultAsync(p => p.Id == pc.ProductId);
                pc.Product = product;
            }

            return View(sale);
        }

        // Vista de eliminar de venta.
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sale == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale.FirstOrDefaultAsync(m => m.Id == id);

            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // Al eliminar venta, luego de apretar en el botón eliminar.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sale == null) {
                return Problem("Entity set 'CMContext.Sale'  is null.");
            }

            var sale = await _context.Sale.FirstOrDefaultAsync(s => s.Id == id);

            if (sale != null) {
                DeleteProducsInSale(id);

                _context.Sale.Remove(sale);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private void ReturnProductsToStock(List<ProductCart> pList) {
            foreach (ProductCart pc in pList) {
                Product? findProduct = _context.Product.Where(p => p.Id == pc.ProductId).FirstOrDefault();
                
                if (findProduct != null) {
                    findProduct.Stock += pc.Quantity;
                    findProduct.SoldItems -= pc.Quantity;
                    var log = StocksController.CreateStockLog(findProduct, pc.Quantity, "Devolucion");
                    _context.Stock.Add(log);
                    _context.Product.Update(findProduct);
                }
            }
        }
        
        private void DeleteProducsInSale(int saleId) {
            List<ProductCart> products = _context.ProductCart
                .Where(pc => pc.SaleId == saleId)
                .ToList();

            ReturnProductsToStock(products);

            _context.ProductCart.RemoveRange(products);
        }
    }
}
