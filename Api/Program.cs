using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- SERVICIOS ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AgenciaDbContext>(opt => opt.UseInMemoryDatabase("AgenciaDb"));

// Inyectamos LOS DOS repositorios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProyectoRepository, ProyectoRepository>(); // <--- NUEVO

var app = builder.Build();

// --- PIPELINE ---
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

// --- SEEDING (Datos Automáticos) ---
using (var scope = app.Services.CreateScope())
{
    var clienteRepo = scope.ServiceProvider.GetRequiredService<IClienteRepository>();
    var proyectoRepo = scope.ServiceProvider.GetRequiredService<IProyectoRepository>();

    // 1. Crear Clientes si no existen
    var clientes = await clienteRepo.ObtenerTodos();
    if (clientes.Count == 0)
    {
        await clienteRepo.Crear(new Cliente { Nombre = "Juan", Apellido = "Perez", Email = "juan@tienda.com", Telefono = "111" });
        await clienteRepo.Crear(new Cliente { Nombre = "Tech Solutions", Apellido = "S.A.", Email = "info@tech.com", Telefono = "222" });
        Console.WriteLine("--> Clientes creados.");
    }

    // 2. Crear Proyectos si no existen
    var proyectos = await proyectoRepo.ObtenerTodos();
    if (proyectos.Count == 0)
    {
        // Nota: Asumimos que Juan tiene Id=1 y Tech tiene Id=2 porque es autoincremental
        await proyectoRepo.Crear(new Proyecto { Nombre = "Tienda Online", Descripcion = "E-commerce en Shopify", Precio = 1500, ClienteId = 1 });
        await proyectoRepo.Crear(new Proyecto { Nombre = "Logo Rebranding", Descripcion = "Vectorización y manual", Precio = 300, ClienteId = 1 });
        await proyectoRepo.Crear(new Proyecto { Nombre = "Campaña Google Ads", Descripcion = "SEM Q1 2026", Precio = 800, ClienteId = 2 });
        Console.WriteLine("--> Proyectos creados.");
    }
}

app.Run();