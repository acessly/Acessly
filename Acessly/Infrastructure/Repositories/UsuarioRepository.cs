using Microsoft.EntityFrameworkCore;
using Acessly.Domain.Entities;
using Acessly.Domain.Interfaces;

namespace Acessly.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AcesslyDbContext _context;

    public UsuarioRepository(AcesslyDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> GetByIdAsync(long id)
    {
        return await _context.Usuarios
            .Include(u => u.Empresa)
            .Include(u => u.Candidato)
            .FirstOrDefaultAsync(u => u.IdUsuario == id);
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios
            .Include(u => u.Empresa)
            .Include(u => u.Candidato)
            .ToListAsync();
    }

    public async Task<Usuario> AddAsync(Usuario entity)
    {
        await _context.Usuarios.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Usuario entity)
    {
        _context.Usuarios.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var usuario = await GetByIdAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        return await _context.Usuarios
            .Include(u => u.Empresa)
            .Include(u => u.Candidato)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.Usuarios.AnyAsync(u => u.Email == email);
    }
}
