// Provides Entity Framework Core support
using Microsoft.EntityFrameworkCore;

// Import application models (ContactContext)
using ContactManagerMVC.Models;

// Create the WebApplication builder
var builder = WebApplication.CreateBuilder(args);

// ===================== SERVICES CONFIGURATION =====================

// Add MVC services (Controllers + Views) to the dependency injection container
builder.Services.AddControllersWithViews();

// Configure routing options
builder.Services.Configure<RouteOptions>(options =>
{
    // Force all generated URLs to be lowercase
    options.LowercaseUrls = true;

    // Append a trailing slash to URLs
    options.AppendTrailingSlash = true;
});

// Register the ContactContext with Entity Framework Core
// Uses SQL Server and the connection string from appsettings.json
builder.Services.AddDbContext<ContactContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ContactContext")
    )
);

// Build the application
var app = builder.Build();

// ===================== HTTP REQUEST PIPELINE =====================

// Configure error handling for non-development environments
if (!app.Environment.IsDevelopment())
{
    // Redirects users to a custom error page
    app.UseExceptionHandler("/Home/Error");

    // Enables HTTP Strict Transport Security (HSTS)
    app.UseHsts();
}

// Redirect HTTP requests to HTTPS
app.UseHttpsRedirection();

// Enables routing middleware
app.UseRouting();

// Enables authorization (no authentication configured yet)
app.UseAuthorization();

// Enables serving static files (CSS, JS, images)
app.MapStaticAssets();

// Configure the default MVC route pattern
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}"
)
.WithStaticAssets();

// Start the application
app.Run();
