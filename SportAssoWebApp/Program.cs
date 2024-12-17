using Microsoft.EntityFrameworkCore;
using SportAssoWebApp.Models;
using Microsoft.AspNetCore.Identity;
using SportAssoWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure database connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SportAssoContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Identity with Roles
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // Add Role Management
    .AddEntityFrameworkStores<SportAssoContext>();

var app = builder.Build();

// Seed Admin Role and User
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        await DataSeeder.SeedAdminUser(services); // Call the seeder
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages(); 


app.UseAuthentication(); // Add Authentication middleware
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
