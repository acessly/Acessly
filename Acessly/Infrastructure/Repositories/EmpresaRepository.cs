using Microsoft.EntityFrameworkCore;
using Acessly.Domain.Entities;
using Acessly.Domain.Enums;
using Acessly.Domain.Interfaces;

namespace Acessly.Infrastructure.Repositories;

public class EmpresaRepository : IEmpresaRepository
{
    private readonly AcesslyDbContext _context;

    public EmpresaRepository(AcesslyDbContext context)
    {
        _context = context;
    }

    public async Task<Empresa?> GetByIdAsync(long id)
    {
        return await _context.Empresas
            .Include(e => e.Usuario)
            .Include(e => e.Vagas)
            .Include(e => e.Suportes)
            .FirstOrDefaultAsync(e => e.IdEmpresa == id);
    }

    public async Task<IEnumerable<Empresa>> GetAllAsync()
    {
        return await _context.Empresas
            .Include(e => e.Usuario)
            .ToListAsync();
    }

    public async Task<Empresa> AddAsync(Empresa entity)
    {
        await _context.Empresas.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Empresa entity)
    {
        _context.Empresas.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var empresa = await GetByIdAsync(id);
        if (empresa != null)
        {
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Empresa>> GetBySetorAsync(string setor)
    {
        return await _context.Empresas
            .Where(e => e.Setor.Contains(setor))
            .Include(e => e.Usuario)
            .ToListAsync();
    }

    public async Task<IEnumerable<Empresa>> GetByNivelAcessibilidadeAsync(NivelAcessibilidade nivel)
    {
        return await _context.Empresas
            .Where(e => e.NivelAcessibilidade == nivel)
            .Include(e => e.Usuario)
            .ToListAsync();
    }

    public async Task<Empresa?> GetByUsuarioIdAsync(long idUsuario)
    {
        return await _context.Empresas
            .Include(e => e.Usuario)
            .Include(e => e.Vagas)
            .Include(e => e.Suportes)
            .FirstOrDefaultAsync(e => e.IdUsuario == idUsuario);
    }
}
