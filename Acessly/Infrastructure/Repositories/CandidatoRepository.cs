using Microsoft.EntityFrameworkCore;
using Acessly.Domain.Entities;
using Acessly.Domain.Enums;
using Acessly.Domain.Interfaces;

namespace Acessly.Infrastructure.Repositories;

public class CandidatoRepository : ICandidatoRepository
{
    private readonly AcesslyDbContext _context;

    public CandidatoRepository(AcesslyDbContext context)
    {
        _context = context;
    }

    public async Task<Candidato?> GetByIdAsync(long id)
    {
        return await _context.Candidatos
            .Include(c => c.Usuario)
            .Include(c => c.Candidaturas)
            .FirstOrDefaultAsync(c => c.IdCandidato == id);
    }

    public async Task<IEnumerable<Candidato>> GetAllAsync()
    {
        return await _context.Candidatos
            .Include(c => c.Usuario)
            .ToListAsync();
    }

    public async Task<Candidato> AddAsync(Candidato entity)
    {
        await _context.Candidatos.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Candidato entity)
    {
        _context.Candidatos.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var candidato = await GetByIdAsync(id);
        if (candidato != null)
        {
            _context.Candidatos.Remove(candidato);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Candidato>> GetByTipoDeficienciaAsync(TipoDeficiencia tipo)
    {
        return await _context.Candidatos
            .Where(c => c.TipoDeficiencia == tipo)
            .Include(c => c.Usuario)
            .ToListAsync();
    }

    public async Task<Candidato?> GetByUsuarioIdAsync(long idUsuario)
    {
        return await _context.Candidatos
            .Include(c => c.Usuario)
            .Include(c => c.Candidaturas)
            .FirstOrDefaultAsync(c => c.IdUsuario == idUsuario);
    }

    public async Task<IEnumerable<Candidato>> GetByHabilidadeAsync(string habilidade)
    {
        return await _context.Candidatos
            .Where(c => c.Habilidades != null && c.Habilidades.Contains(habilidade))
            .Include(c => c.Usuario)
            .ToListAsync();
    }
}
