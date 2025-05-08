using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using SocialService.Data;
using SocialService.Repositories;
using SocialService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext (PostgreSQL via Npgsql)
builder.Services.AddDbContext<SocialDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Repositories and Services
builder.Services.AddScoped<ISocialRepository, SocialRepository>();
builder.Services.AddScoped<ISocialService, SocialService.Services.SocialService>();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Social Service API", Version = "v1" });
});


// Add Controllers with JSON options
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserve property names
    });


builder.WebHost.UseUrls("http://0.0.0.0:80"); // Set the URL for the application
var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();