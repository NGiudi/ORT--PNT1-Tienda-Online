using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyManager.Models;
using Microsoft.AspNetCore.Authorization;
using SQLitePCL;

namespace CompanyManager.Controllers
{
    [Authorize(Roles = "ADMIN,SELLER")]
    public class StocksController : Controller
    {
        private readonly CMContext _context;

        public StocksController(CMContext context)
        {
            _context = context;
        }

        // GET: Stocks
        public async Task<IActionResult> Index(int? id)
        {
            var stock = from s in _context.Stock select s;
            if (id != null)
            {
                var product = await _context.Product.FirstOrDefaultAsync(p => p.Id == id);
                ViewBag.Product = product.Name;
                stock = stock.Where(s => s.ProductId == id);
                stock = stock.OrderByDescending(s => s.UpdatedAt);
            }
            return View(await stock.ToListAsync());
        }

        public static Stock CreateStockLog(Product product, int stockUpdate, string reason)
        {
            Stock log = new Stock()
            {
                ProductId = product.Id,
                StockUpdate = stockUpdate,
                CurrentStock = product.Stock,
                UpdatedAt = DateTime.Now,
                Reason = reason,
            };
            return log;
        }
    }
}
