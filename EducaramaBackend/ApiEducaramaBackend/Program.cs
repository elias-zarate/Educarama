using Datos.EntidadesDb;
using Microsoft.EntityFrameworkCore;

//Area de servicios
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var cadenaDeConexion = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<EducaramaContext>(options =>
{
    options.UseSqlServer(cadenaDeConexion);
});

//Area de middlewares
app.MapGet("/", () => "Hello World!");

app.Run();
