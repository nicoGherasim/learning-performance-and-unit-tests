using Microsoft.EntityFrameworkCore;
using ProductsApp.Application.Services;
using ProductsApp.DataAccess;
using ProductsApp.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer("Server=localhost;Database=ProductsDatabase;User Id=sa;Password=SQLServerPassword123;TrustServerCertificate=True");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
