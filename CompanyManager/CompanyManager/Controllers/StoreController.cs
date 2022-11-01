using CompanyManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Controllers
{
    public class StoreController : Controller
    {
        private readonly CMContext _context;

        public StoreController(CMContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var productsList = _context.Product.Where(p => p.Stock > 0);

            foreach (Product p in productsList) {
                p.Price = p.Price - (p.Price * p.Discount / 100);
            }

            return View(await productsList.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            } else {
                product.Price = product.Price - (product.Price * product.Discount / 100);
            }

            return View(product);
        }
    }
}
