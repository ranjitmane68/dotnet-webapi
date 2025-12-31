// Import required namespaces using Microsoft.EntityFrameworkCore; // Provides EF Core functionality for database access using Microsoft.OpenApi; // Provides OpenAPI/Swagger support using NZWalks.API.Data; // Namespace containing your DbContext (NZWalksDbContext) var builder = WebApplication.CreateBuilder(args); // Creates a WebApplicationBuilder which sets up configuration, logging, and dependency injection // -------------------- Service Registration -------------------- // Registers OpenAPI services (for generating API documentation automatically) // Learn more: https://aka.ms/aspnet/openapi builder.Services.AddOpenApi(); // Registers controller support (enables attribute-based routing and MVC-style controllers) builder.Services.AddControllers(); // Registers endpoint API explorer (used by Swagger to discover endpoints) builder.Services.AddEndpointsApiExplorer(); // Registers Swagger generator (creates Swagger/OpenAPI specification for your API) builder.Services.AddSwaggerGen(); // Registers Entity Framework Core DbContext with SQL Server provider // Uses connection string "NZWalksConnectionStrings" from appsettings.json builder.Services.AddDbContext<NZWalksDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionStrings"))); var app = builder.Build(); // Builds the WebApplication (finalizes DI container and prepares middleware pipeline) // -------------------- Middleware Pipeline -------------------- // Configure middleware only in Development environment if (app.Environment.IsDevelopment()) { app.MapOpenApi(); // Maps OpenAPI endpoints (e.g., /openapi/v1.json) app.UseSwagger(); // Enables Swagger middleware to serve generated JSON app.UseSwaggerUI(); // Enables Swagger UI (interactive API documentation at /swagger) } // Redirects HTTP requests to HTTPS automatically app.UseHttpsRedirection(); // Example data array (used in WeatherForecast demo controller) var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" }; // Middleware for security/authentication app.UseHttpsRedirection(); // (duplicate call, safe but redundant) app.UseAuthentication(); // Enables authentication middleware (validates user identity) app.UseAuthorization(); // Enables authorization middleware (checks user permissions) // Maps controller endpoints (routes defined in controllers are activated) app.MapControllers(); // Runs the application (starts listening for HTTP requests) app.Run(); // -------------------- Demo Record -------------------- // Record type representing a WeatherForecast model // Includes computed property TemperatureF (converts Celsius to Fahrenheit) record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary) { public int TemperatureF => 32 + (int)(TemperatureC / 0.5556); }// Import required namespaces
using Microsoft.EntityFrameworkCore;   // Provides EF Core functionality for database access
using NZWalks.API.Data;               // Namespace containing your DbContext (NZWalksDbContext)

var builder = WebApplication.CreateBuilder(args);
// Creates a WebApplicationBuilder which sets up configuration, logging, and dependency injection

// -------------------- Service Registration --------------------

// Registers OpenAPI services (for generating API documentation automatically)
// Learn more: https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Registers controller support (enables attribute-based routing and MVC-style controllers)
builder.Services.AddControllers();

// Registers endpoint API explorer (used by Swagger to discover endpoints)
builder.Services.AddEndpointsApiExplorer();

// Registers Swagger generator (creates Swagger/OpenAPI specification for your API)
builder.Services.AddSwaggerGen();

// Registers Entity Framework Core DbContext with SQL Server provider
// Uses connection string "NZWalksConnectionStrings" from appsettings.json
builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionStrings")));

var app = builder.Build();
// Builds the WebApplication (finalizes DI container and prepares middleware pipeline)

// -------------------- Middleware Pipeline --------------------

// Configure middleware only in Development environment
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();   // Maps OpenAPI endpoints (e.g., /openapi/v1.json)
    app.UseSwagger();   // Enables Swagger middleware to serve generated JSON
    app.UseSwaggerUI(); // Enables Swagger UI (interactive API documentation at /swagger)
}

// Redirects HTTP requests to HTTPS automatically
app.UseHttpsRedirection();

// Middleware for security/authentication
app.UseAuthentication();   // Enables authentication middleware (validates user identity)
app.UseAuthorization();    // Enables authorization middleware (checks user permissions)

// Maps controller endpoints (routes defined in controllers are activated)
app.MapControllers();

// Runs the application (starts listening for HTTP requests)
app.Run();

