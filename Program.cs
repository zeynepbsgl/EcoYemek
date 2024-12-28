using Microsoft.Extensions.FileProviders;
using MealBox.Models.Classes;
using Microsoft.Extensions.FileProviders;
using System.IO;
using MealBox.Models.Classes;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Context>();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSession();

// Authentication ve Authorization ekleniyor
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Index"; // Login sayfasının yolu
        options.AccessDeniedPath = "/Login/Index";

    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot altýndaki dosyalar için

// web klasörü için yeni statik dosya ayarlarý
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "web")), // "web" klasörüne iþaret eder
    RequestPath = "/web"  // URL'deki /web ile eriþilecek
});

app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
