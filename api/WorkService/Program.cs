using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using WorkService.Data;
using WorkService.Repositories;
using WorkService.Services;

var builder = WebApplication.CreateBuilder(args);

//Add DbContext (PostgreSQL via Npgsql)
builder.Services.AddDbContext<WorkDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IWorkRepository, WorkRepository>();
builder.Services.AddScoped<IWorkService, WorkService.Services.WorkService>();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Work Service API", Version = "v1" });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Preserve property names
    });

var app = builder.Build();

//middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();


await app.RunAsync();  