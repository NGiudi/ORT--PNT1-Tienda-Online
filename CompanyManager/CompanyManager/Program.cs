using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CMContext>(options => options.UseSqlite(@"filename=C:\DB\Stock.db"));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuracion de Session para el carrito.
builder.Services.AddSession();

// Configuración de cookies para el logueo.
// Tiene fecha de expiracion las cookies.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option => {
        option.LoginPath = "/Session/Login";
        option.ExpireTimeSpan = TimeSpan.FromDays(1);
        option.AccessDeniedPath = "/Store";
    });

var app = builder.Build();

// Configuracion de session para carrito.
app.UseSession();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Store}/{action=Index}/{id?}");

app.Run();
