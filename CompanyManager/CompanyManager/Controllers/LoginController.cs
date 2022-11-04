using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CompanyManager.Models;

namespace CompanyManager.Controllers
{
    public class LoginController : Controller
    {
        private readonly CMContext _context;

        public LoginController(CMContext context)
        {
            _context = context;
        }

        private User? UserValidation(string Username, string Password)
        {
            return _context.User
                    .Where(item => item.Username == Username && item.Password == Password)
                    .FirstOrDefault();
        }

        private IActionResult UsersRedirect (UserRoles userRole) {
            if (userRole == UserRoles.ADMIN || userRole == UserRoles.SELLER) { 
                return RedirectToAction("Index", "Sales");
            }

            return RedirectToAction("Index", "Store");
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user) {
            var findUser = UserValidation(user.Username, user.Password);

            if (findUser != null) {
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, findUser.Name),
                };

                claims.Add(new Claim(ClaimTypes.Role, findUser.Role.ToString()));
                
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity)).Wait();

                //TODO: según el usuario redireccionarlo a una pantalla diferente.
                return UsersRedirect(findUser.Role);
            } else {
                return View();
            }
        }

        public IActionResult Logout() {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
            return RedirectToAction("Index", "Login");
        }
    }
}
