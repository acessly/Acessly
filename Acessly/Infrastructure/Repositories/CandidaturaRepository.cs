using Microsoft.EntityFrameworkCore;
using Acessly.Domain.Entities;
using Acessly.Domain.Enums;
using Acessly.Domain.Interfaces;

namespace Acessly.Infrastructure.Repositories;

public class CandidaturaRepository : ICandidaturaRepository
{
    private readonly AcesslyDbContext _context;

    public CandidaturaRepository(AcesslyDbContext context)
    {
        _context = context;
    }

    public async Task<Candidatura?> GetByIdAsync(long id)
    {
        return await _context.Candidaturas
            .Include(c => c.Candidato)
                .ThenInclude(ca => ca.Usuario)
            .Include(c => c.Vaga)
                .ThenInclude(v => v.Empresa)
            .FirstOrDefaultAsync(c => c.IdCandidatura == id);
    }

    public async Task<IEnumerable<Candidatura>> GetAllAsync()
    {
        return await _context.Candidaturas
            .Include(c => c.Candidato)
                .ThenInclude(ca => ca.Usuario)
            .Include(c => c.Vaga)
                .ThenInclude(v => v.Empresa)
            .ToListAsync();
    }

    public async Task<Candidatura> AddAsync(Candidatura entity)
    {
        await _context.Candidaturas.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Candidatura entity)
    {
        _context.Candidaturas.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var candidatura = await GetByIdAsync(id);
        if (candidatura != null)
        {
            _context.Candidaturas.Remove(candidatura);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Candidatura>> GetByCandidatoAsync(long idCandidato)
    {
        return await _context.Candidaturas
            .Where(c => c.IdCandidato == idCandidato)
            .Include(c => c.Vaga)
                .ThenInclude(v => v.Empresa)
            .OrderByDescending(c => c.DataCandidatura)
            .ToListAsync();
    }

    public async Task<IEnumerable<Candidatura>> GetByVagaAsync(long idVaga)
    {
        return await _context.Candidaturas
            .Where(c => c.IdVaga == idVaga)
            .Include(c => c.Candidato)
                .ThenInclude(ca => ca.Usuario)
            .OrderByDescending(c => c.DataCandidatura)
            .ToListAsync();
    }

    public async Task<IEnumerable<Candidatura>> GetByStatusAsync(StatusCandidatura status)
    {
        return await _context.Candidaturas
            .Where(c => c.Status == status)
            .Include(c => c.Candidato)
                .ThenInclude(ca => ca.Usuario)
            .Include(c => c.Vaga)
                .ThenInclude(v => v.Empresa)
            .ToListAsync();
    }

    public async Task<bool> ExisteCandidaturaAsync(long idCandidato, long idVaga)
    {
        if (idCandidato <= 0 || idVaga <= 0)
            throw new ArgumentException("idCandidato e idVaga devem ser maiores que zero.");

        return await _context.Candidaturas
            .AnyAsync(c => c.IdCandidato == idCandidato && c.IdVaga == idVaga);
    }


    public async Task<IEnumerable<Candidatura>> GetByCandidatoAndStatusAsync(long idCandidato, StatusCandidatura status)
    {
        return await _context.Candidaturas
            .Where(c => c.IdCandidato == idCandidato && c.Status == status)
            .Include(c => c.Vaga)
                .ThenInclude(v => v.Empresa)
            .OrderByDescending(c => c.DataCandidatura)
            .ToListAsync();
    }
}

