using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyManager.Models;
using System.Security.Claims;

namespace CompanyManager.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly CMContext _context;

        public PurchasesController(CMContext context)
        {
            _context = context;
        }

        // Vista listado de compras del usuario.
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.Name);
            var purchases = await _context.Sale.Where(o => o.BuyerId == int.Parse(userId)).ToListAsync();

            foreach (var p in purchases)
            {
                var buyer = await _context.User.FirstOrDefaultAsync(e => e.Id == p.BuyerId);
                p.Buyer = buyer;
            }
            return View(purchases);
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
    }
}
