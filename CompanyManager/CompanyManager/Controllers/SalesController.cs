﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CompanyManager.Models;
using Microsoft.AspNetCore.Authorization;
using CompanyManager.Migrations;

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
        public async Task<IActionResult> Index()
        {
              return View(await _context.Sale.ToListAsync());
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
