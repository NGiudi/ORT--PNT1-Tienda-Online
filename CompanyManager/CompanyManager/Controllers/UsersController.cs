using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CompanyManager.Models;
using Microsoft.AspNetCore.Authorization;

namespace CompanyManager.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class UsersController : Controller
    {
        private readonly CMContext _context;

        public UsersController(CMContext context) {
            _context = context;
        }

        private bool UserExists(int id) {
            return _context.User.Any(e => e.Id == id);
        }

        // Vista de listado de usuarios.
        public async Task<IActionResult> Index() {
              return View(await _context.User.ToListAsync());
        }

        // Vista detalle del usuario.
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.User == null) {
                return NotFound();
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);

            if (user == null) {
                return NotFound();
            }

            return View(user);
        }

        // Vista vacía de creación de del usuario.
        public IActionResult Create() {
            return View();
        }

        // Al crear usuario, luego de apretar en el botón crear.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user) {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // Vista de edición de usuario.
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Al editar usuario, luego de apretar en el botón editar.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user) {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // Vista de eliminar de usuario.
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);
            
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // Al eliminar usuario, luego de apretar en el botón eliminar.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.User == null) {
                return Problem("Entity set 'CMContext.User'  is null.");
            }

            var user = await _context.User.FindAsync(id);
            
            if (user != null) {
                _context.User.Remove(user);
                _context.SaveChanges();
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
