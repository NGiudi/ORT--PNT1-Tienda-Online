using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CompanyManager.Models;

namespace CompanyManager.Controllers
{
    public class SessionController : Controller
    {
        private readonly CMContext _context;

        public SessionController(CMContext context)
        {
            _context = context;
        }

        // Según el rol del usuario se redirecciona a diferentes vistas.
        private IActionResult UsersRedirect (UserRoles userRole) {
            if (userRole == UserRoles.ADMIN || userRole == UserRoles.SELLER) { 
                return RedirectToAction("Index", "Products");
            }

            return RedirectToAction("Index", "Store");
        }

        // Vista del login.
        public IActionResult Login()
        {
            return View();
        }

        // Al iniciar sesión, luego de apretar el botón de iniciar sesión.
        [HttpPost]
        public IActionResult Login(User user) {
            var findUser = _context.User
                .Where(item => item.Username == user.Username && item.Password == user.Password)
                .FirstOrDefault();

            if (findUser != null) {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, findUser.Name),
                };

                claims.Add(new Claim(ClaimTypes.Role, findUser.Role.ToString()));
                
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity)).Wait();

                return UsersRedirect(findUser.Role);
            } else {
                return View();
            }
        }

        // Al cerrar sesión, luego de apretar el botón de logout.
        // Se borra las cookies y se redirecciona a la vista de la tienda.
        public IActionResult Logout() {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Index", "Store");
        }
    }
}
