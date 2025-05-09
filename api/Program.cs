using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// 1. Add services to the container.

// Swagger/OpenAPI setup (for the gateway itself)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Portfolio API Gateway", Version = "v1" });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });

});

// 2. Load Ocelot configuration
builder.Configuration
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// 3. Register Ocelot
builder.Services.AddOcelot(builder.Configuration);

builder.WebHost.UseUrls("http://0.0.0.0:80");
var app = builder.Build();

// 4. Swagger UI (if in Development)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        // Serve at /swagger for gateway metadata
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Portfolio API Gateway V1");
    });
}

// Enable CORS
app.UseCors("DevFrontend");

// 5. Standard middleware order
app.UseHttpsRedirection();
app.UseRouting();

app.MapGet("/", () => "🟢 API Gateway is running");

// 6. Use Ocelot as the final middleware
await app.UseOcelot();

// 7. Start the app
await app.RunAsync();
