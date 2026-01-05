using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURACIÓN DE SERVICIOS ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Para la pantalla azul

// Base de datos en memoria (paquete que instalaste)
builder.Services.AddDbContext<AgenciaDbContext>(opt => opt.UseInMemoryDatabase("AgenciaDb"));

// Inyección del Repositorio
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

var app = builder.Build();

// --- 2. CONFIGURACIÓN DEL MOTOR ---

// Activar Swagger SIEMPRE (sin condiciones)
app.UseSwagger();
app.UseSwaggerUI();

// Mapear los controladores (IMPORTANTE)
app.MapControllers();

// ¡EL TRUCO! Un mensaje en la raíz para que veas que funciona
app.MapGet("/", () => "¡HOLA! El servidor está VIVO. Ve a /swagger/index.html para probarlo.");

app.Run();