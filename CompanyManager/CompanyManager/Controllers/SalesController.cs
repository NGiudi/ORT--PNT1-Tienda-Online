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

        private async Task<Person> GetPerson(int id)
        {
            Person? person = await _context.User.FirstOrDefaultAsync(e => e.Id == id);

            return person;
        }

        // Vista de listado de ventas.
        public async Task<IActionResult> Index()
        {
            var ventas = await _context.Sale.ToListAsync();

            // Asigna el usuario a la venta.
            foreach (var vent in ventas)
            {
                GetPerson(vent.BuyerId);
            }
            return View(ventas);
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

            GetPerson(sale.BuyerId);

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
