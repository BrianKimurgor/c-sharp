using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using EducationService.Data;
using EducationService.Repositories;
using EducationService.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<EducationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IEducationService, EducationService.Services.EducationService>();



//swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Education Service API", Version = "v1" });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserve property names
    });



builder.WebHost.UseUrls("http://0.0.0.0:80"); // Set the URL for the application
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();


