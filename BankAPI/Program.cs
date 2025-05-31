using Microsoft.EntityFrameworkCore;
using BankAPI;
using BankBlazor.Shared.Models;
using BankBlazor.Shared.Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        builder => builder
            .WithOrigins("https://localhost:7249", "http://localhost:5279")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddDbContext<BankDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("BankBlazorConnection"),
        sqlOptions => sqlOptions.CommandTimeout(60)
    )
);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowBlazorApp");

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
