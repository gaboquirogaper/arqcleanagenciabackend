using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AgenciaDbContext _context;

        public ClienteRepository(AgenciaDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> ObtenerTodos()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task Crear(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
        }

        // --- AGREGAR DESDE AQUÍ ---
        public async Task Editar(Cliente cliente)
        {
            var existente = await _context.Clientes.FindAsync(cliente.Id);
            if (existente != null)
            {
                existente.Nombre = cliente.Nombre;
                existente.Apellido = cliente.Apellido;
                existente.Email = cliente.Email;
                existente.Telefono = cliente.Telefono;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Eliminar(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }
        }
        // --- HASTA AQUÍ ---
    }
}