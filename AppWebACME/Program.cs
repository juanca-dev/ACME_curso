//using Services.ACME;

//namespace AppWebACME
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            // Add services to the container.
//            builder.Services.AddControllersWithViews();
//            // 

//            // Le decimos al contenedor de dependencias que reconozca tus clases de servicios
//            // 
//            builder.Services.AddScoped<RequisicionService>();
//            builder.Services.AddScoped<EmpresaService>();
//            builder.Services.AddScoped<ArticuloService>();

//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (!app.Environment.IsDevelopment())
//            {
//                app.UseExceptionHandler("/Home/Error");
//            }
//            app.UseRouting();

//            app.UseAuthorization();

//            app.MapStaticAssets();
//            app.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=Home}/{action=Index}/{id?}/{otherParameter?}")
//                .WithStaticAssets();

//            app.Run();
//        }
//    }
//}


using Microsoft.AspNetCore.Authentication.Cookies;
using Services.ACME;

namespace AppWebACME
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ── Servicios MVC ─────────────────────────────────────────────
            builder.Services.AddControllersWithViews();

            // ── Servicios existentes de la aplicación ─────────────────────
            builder.Services.AddScoped<RequisicionService>();
            builder.Services.AddScoped<EmpresaService>();
            builder.Services.AddScoped<ArticuloService>();

            // ── Servicios nuevos de autenticación ─────────────────────────
            builder.Services.AddScoped<DataAccess.ACME.UsuarioDA>();
            builder.Services.AddScoped<IAccountService, AccountService>();

            // ── Cookie Authentication ─────────────────────────────────────
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    options.AccessDeniedPath = "/Account/AccesoDenegado";
                    options.ExpireTimeSpan = TimeSpan.FromHours(8);
                    options.SlidingExpiration = true;
                    options.Cookie.HttpOnly = true;
                    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // SameAsRequest para desarrollo local sin HTTPS
                    options.Cookie.SameSite = SameSiteMode.Strict;
                });

            // ─────────────────────────────────────────────────────────────
            var app = builder.Build();
            // ─────────────────────────────────────────────────────────────

            // ── Pipeline HTTP ─────────────────────────────────────────────
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRouting();

            app.UseAuthentication();   // ← NUEVO: debe ir ANTES de UseAuthorization
            app.UseAuthorization();

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}/{otherParameter?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}