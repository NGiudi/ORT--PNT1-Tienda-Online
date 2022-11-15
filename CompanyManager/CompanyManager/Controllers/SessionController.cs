using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CompanyManager.Models;
using System.Security.Principal;

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
                .Where(item => item.Email == user.Email && item.Password == user.Password)
                .FirstOrDefault();

            ModelState.Remove("Password");

            if (findUser != null) {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, findUser.Id.ToString()),
                };

                claims.Add(new Claim(ClaimTypes.NameIdentifier, findUser.Username));
                claims.Add(new Claim(ClaimTypes.Role, findUser.Role.ToString()));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity)).Wait();

                return UsersRedirect(findUser.Role);
            }

            ModelState.AddModelError("Password", ErrorViewModel.InvalidLogin);
            return View();
        }

        public IActionResult Register() {
            return View();
        }

        // Al confirmar registro, luego de apretar el botón de iniciar sesión.
        [HttpPost]
        public async Task<IActionResult> Register(User user) {
            // Validar mail repetido.
            var findUser = _context.User
                .Where(u => u.Email.Equals(user.Email))
                .FirstOrDefault();

            ModelState.Remove("Email");

            // guardar usuario
            if (findUser == null) {
                _context.Add(user);
                await _context.SaveChangesAsync();
                Login(user);
                return RedirectToAction("Index", "Store");
            }
            
            ModelState.AddModelError("Email", ErrorViewModel.InvalidEmail);
            return View(user);
        }

        // Al cerrar sesión, luego de apretar el botón de logout.
        // Se borra las cookies y se redirecciona a la vista de la tienda.
        public IActionResult Logout() {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Index", "Store");
        }
    }
}
