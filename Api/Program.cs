using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Application.Interfaces;
using Application.Interfaces.UseCases;
using Application.UseCases.Clientes;
using Application.UseCases.Dashboard; // Asegúrate de tener este
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.UseCases.Proyectos;

var builder = WebApplication.CreateBuilder(args);

// --- SERVICIOS ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AgenciaDbContext>(opt => opt.UseInMemoryDatabase("AgenciaDb"));

// 1. Repositorios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProyectoRepository, ProyectoRepository>();

// 2. Casos de Uso (Interfaces -> Implementación)
builder.Services.AddScoped<IObtenerClientesUseCase, ObtenerClientesUseCase>();
builder.Services.AddScoped<ICrearClienteUseCase, CrearClienteUseCase>();
builder.Services.AddScoped<IEliminarClienteUseCase, EliminarClienteUseCase>();
builder.Services.AddScoped<IEditarClienteUseCase, EditarClienteUseCase>();


builder.Services.AddScoped<IObtenerDashboardUseCase, ObtenerDashboardUseCase>();

builder.Services.AddScoped<IObtenerProyectosUseCase, ObtenerProyectosUseCase>();
builder.Services.AddScoped<ICrearProyectoUseCase, CrearProyectoUseCase>();
builder.Services.AddScoped<IEliminarProyectoUseCase, EliminarProyectoUseCase>();
builder.Services.AddScoped<IEditarProyectoUseCase, EditarProyectoUseCase>();

// 3. CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirReact",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// --- PIPELINE ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("PermitirReact");
app.MapControllers();

// --- SEEDING (Datos de prueba) ---
using (var scope = app.Services.CreateScope())
{
    var clienteRepo = scope.ServiceProvider.GetRequiredService<IClienteRepository>();
    var proyectoRepo = scope.ServiceProvider.GetRequiredService<IProyectoRepository>();

    var clientes = await clienteRepo.ObtenerTodos();
    if (clientes.Count == 0)
    {
        await clienteRepo.Crear(new Cliente { Nombre = "Juan", Apellido = "Perez", Email = "juan@tienda.com", Telefono = "111" });
        await clienteRepo.Crear(new Cliente { Nombre = "Tech Solutions", Apellido = "S.A.", Email = "info@tech.com", Telefono = "222" });
    }

    var proyectos = await proyectoRepo.ObtenerTodos();
    if (proyectos.Count == 0)
    {
        await proyectoRepo.Crear(new Proyecto { Nombre = "Tienda Online", Descripcion = "E-commerce", Precio = 1500, ClienteId = 1 });
        await proyectoRepo.Crear(new Proyecto { Nombre = "Logo", Descripcion = "Vectorización", Precio = 300, ClienteId = 1 });
    }
}

app.Run();