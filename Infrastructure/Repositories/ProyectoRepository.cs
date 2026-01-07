using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Proyecto>> ObtenerPorCliente(int clienteId)
        {
            return await _context.Proyectos.Where(p => p.ClienteId == clienteId).ToListAsync();
        }

        public async Task<Proyecto?> ObtenerPorId(int id)
        {
            return await _context.Proyectos.FindAsync(id);
        }

        public async Task Crear(Proyecto proyecto)
        {
            _context.Proyectos.Add(proyecto);
            await _context.SaveChangesAsync();
        }

        public async Task Actualizar(Proyecto proyecto)
        {
            _context.Entry(proyecto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
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