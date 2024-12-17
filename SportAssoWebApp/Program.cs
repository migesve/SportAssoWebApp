using Microsoft.EntityFrameworkCore;
using SportAssoWebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SportAssoContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "/Adherents",
    pattern: "{controller=Adherents}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "/Creneaux",
    pattern: "{controller=Creneaux}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "/Inscriptions",
    pattern: "{controller=Inscriptions}/{action=Index}/{id?}");



app.Run();
