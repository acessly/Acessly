using Microsoft.EntityFrameworkCore;
using Acessly.Domain.Entities;
using Acessly.Domain.Interfaces;

namespace Acessly.Infrastructure.Repositories;

public class SuporteEmpresaRepository : ISuporteEmpresaRepository
{
    private readonly AcesslyDbContext _context;

    public SuporteEmpresaRepository(AcesslyDbContext context)
    {
        _context = context;
    }

    public async Task<SuporteEmpresa?> GetByIdAsync(long id)
    {
        return await _context.SuportesEmpresa
            .Include(s => s.Empresa)
            .FirstOrDefaultAsync(s => s.IdSuporte == id);
    }

    public async Task<IEnumerable<SuporteEmpresa>> GetAllAsync()
    {
        return await _context.SuportesEmpresa
            .Include(s => s.Empresa)
            .ToListAsync();
    }

    public async Task<SuporteEmpresa> AddAsync(SuporteEmpresa entity)
    {
        await _context.SuportesEmpresa.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(SuporteEmpresa entity)
    {
        _context.SuportesEmpresa.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var suporte = await GetByIdAsync(id);
        if (suporte != null)
        {
            _context.SuportesEmpresa.Remove(suporte);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<SuporteEmpresa>> GetByEmpresaAsync(long idEmpresa)
    {
        return await _context.SuportesEmpresa
            .Where(s => s.IdEmpresa == idEmpresa)
            .Include(s => s.Empresa)
            .ToListAsync();
    }

    public async Task<IEnumerable<SuporteEmpresa>> GetByTipoSuporteAsync(string tipoSuporte)
    {
        return await _context.SuportesEmpresa
            .Where(s => s.TipoSuporte.Contains(tipoSuporte))
            .Include(s => s.Empresa)
            .ToListAsync();
    }

    public async Task<int> CountByEmpresaAsync(long idEmpresa)
    {
        return await _context.SuportesEmpresa
            .CountAsync(s => s.IdEmpresa == idEmpresa);
    }
}
