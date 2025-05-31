using Microsoft.EntityFrameworkCore;
using BankAPI;
using BankBlazor.Shared.Models;
using BankBlazor.Shared.Dtos;

var builder = WebApplication.CreateBuilder(args);

// Lägg till CORS-policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        builder => builder
        
            .WithOrigins("https://localhost:7249", "http://localhost:5279")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Lägg till DbContext (uppdatera connection string i appsettings.json)
builder.Services.AddDbContext<BankDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankBlazorConnection"))
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aktivera CORS
app.UseCors("AllowBlazorApp");

// Swagger (valfritt)
app.UseSwagger();
app.UseSwaggerUI();

// HTTPS och Routing
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
