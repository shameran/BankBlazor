using Microsoft.EntityFrameworkCore;
using BankAPI;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<BankDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankBlazorConnection"))
);


builder.Services.AddControllers();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

// Aktivera HTTPS
app.UseHttpsRedirection();

// Aktivera Routing och Controllers
app.MapControllers();

app.Run();
