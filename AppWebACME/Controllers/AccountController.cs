using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Models.ACME;
using Services.ACME;
using System.Security.Claims;

namespace AppWebACME.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // ── GET /Account/Login ────────────────────────────────────────────
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");

            ViewData["ReturnUrl"] = returnUrl;
            return View(new LoginViewModel());
        }

        // ── POST /Account/Login ───────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
                return View(model);

            var usuario = await _accountService.ValidarCredencialesAsync(
                model.NombreUsuario, model.Password);

            if (usuario is null)
            {
                ModelState.AddModelError(string.Empty,
                    "Usuario o contraseña incorrectos.");
                return View(model);
            }

            // Construir Claims
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
                new(ClaimTypes.Name,           usuario.NombreUsuario),
                new(ClaimTypes.GivenName,      usuario.NombreCompleto),
                new(ClaimTypes.Email,          usuario.Correo),
                new(ClaimTypes.Role,           usuario.NombreRol ?? "Operador"),
            };

            var identidad = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identidad);

            var propiedades = new AuthenticationProperties
            {
                IsPersistent = model.Recordarme,
                ExpiresUtc = model.Recordarme
                    ? DateTimeOffset.UtcNow.AddDays(7)
                    : DateTimeOffset.UtcNow.AddHours(8)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                propiedades);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }
        // ── SOLO PARA DESARROLLO — eliminar antes de producción ──
        [HttpGet]
        public IActionResult GenerarHash(string pass = "Admin@2024")
        {
            var (hash, salt) = _accountService.GenerarHash(pass);
            return Content($"HASH: {hash}\nSALT: {salt}");
        }

        // ── GET /Account/Logout ───────────────────────────────────────────
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        // ── GET /Account/AccesoDenegado ───────────────────────────────────
        [HttpGet]
        public IActionResult AccesoDenegado() => View();
    }
}