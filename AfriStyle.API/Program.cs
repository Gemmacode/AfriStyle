using AfriStyle.API.Middleware;
using AfriStyle.Application;
using AfriStyle.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// ═══════════════════════════════════════════════════════════════
// SERVICE REGISTRATION
// ═══════════════════════════════════════════════════════════════

// Controllers
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "AfriStyle Fit API",
        Version = "v1",
        Description = "Hairstyle recommendations specialized for African & textured hair"
    });
});

// Application layer (MediatR, FluentValidation, Mapster)
builder.Services.AddApplication();

// Infrastructure layer (Repositories, Domain Services)
builder.Services.AddInfrastructure();

// CORS for React frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactDevPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000") // Vite + CRA
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// ═══════════════════════════════════════════════════════════════
// HTTP PIPELINE CONFIGURATION
// ═══════════════════════════════════════════════════════════════

var app = builder.Build();

// Global error handling (must be first)
app.UseMiddleware<ErrorHandlingMiddleware>();

// Swagger (development only)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AfriStyle Fit API v1");
        // Serve Swagger UI under /swagger so /swagger/index.html works (avoid root override)
        c.RoutePrefix = "swagger";
    });
}

// CORS
app.UseCors("ReactDevPolicy");  

// HTTPS redirection
app.UseHttpsRedirection();

// Authorization (not used in MVP, but included for future)
app.UseAuthorization();

// Map controllers
app.MapControllers();

// ═══════════════════════════════════════════════════════════════
// RUN
// ═══════════════════════════════════════════════════════════════

app.Run();