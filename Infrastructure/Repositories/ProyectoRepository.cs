using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProyectoRepository : IProyectoRepository
    {
        private readonly AgenciaDbContext _context;

        public ProyectoRepository(AgenciaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Proyecto>> ObtenerTodos()
        {
            return await _context.Proyectos.ToListAsync();
        }

        public async Task Crear(Proyecto proyecto)
        {
            _context.Proyectos.Add(proyecto);
            await _context.SaveChangesAsync();
        }

        // --- AGREGAR ESTOS DOS ---
        public async Task Editar(Proyecto proyecto)
        {
            var existente = await _context.Proyectos.FindAsync(proyecto.Id);
            if (existente != null)
            {
                existente.Nombre = proyecto.Nombre;
                existente.Descripcion = proyecto.Descripcion;
                existente.Precio = proyecto.Precio;
                existente.ClienteId = proyecto.ClienteId;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Eliminar(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto != null)
            {
                _context.Proyectos.Remove(proyecto);
                await _context.SaveChangesAsync();
            }
        }
    }
}