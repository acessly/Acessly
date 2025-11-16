using Microsoft.EntityFrameworkCore;
using Acessly.Domain.Entities;
using Acessly.Domain.Enums;
using Acessly.Domain.Interfaces;

namespace Acessly.Infrastructure.Repositories;

public class VagaRepository : IVagaRepository
{
    private readonly AcesslyDbContext _context;

    public VagaRepository(AcesslyDbContext context)
    {
        _context = context;
    }

    public async Task<Vaga?> GetByIdAsync(long id)
    {
        return await _context.Vagas
            .Include(v => v.Empresa)
            .Include(v => v.Candidaturas)
            .FirstOrDefaultAsync(v => v.IdVaga == id);
    }

    public async Task<IEnumerable<Vaga>> GetAllAsync()
    {
        return await _context.Vagas
            .Include(v => v.Empresa)
            .ToListAsync();
    }

    public async Task<Vaga> AddAsync(Vaga entity)
    {
        await _context.Vagas.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(Vaga entity)
    {
        _context.Vagas.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var vaga = await GetByIdAsync(id);
        if (vaga != null)
        {
            _context.Vagas.Remove(vaga);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Vaga>> SearchAsync(string? titulo, TipoVaga? tipo, string? cidade, int page, int pageSize)
    {
        var query = _context.Vagas.AsQueryable();

        if (!string.IsNullOrWhiteSpace(titulo))
            query = query.Where(v => v.Titulo.Contains(titulo));

        if (tipo.HasValue)
            query = query.Where(v => v.TipoVaga == tipo.Value);

        if (!string.IsNullOrWhiteSpace(cidade))
            query = query.Where(v => v.Cidade == cidade);

        return await query
            .Include(v => v.Empresa)
            .OrderBy(v => v.Titulo)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Vaga>> GetByEmpresaAsync(long idEmpresa)
    {
        return await _context.Vagas
            .Where(v => v.IdEmpresa == idEmpresa)
            .Include(v => v.Empresa)
            .ToListAsync();
    }

    public async Task<IEnumerable<Vaga>> GetByTipoAsync(TipoVaga tipo)
    {
        return await _context.Vagas
            .Where(v => v.TipoVaga == tipo)
            .Include(v => v.Empresa)
            .ToListAsync();
    }

    public async Task<IEnumerable<Vaga>> GetByLocalizacaoAsync(string? cidade, string? estado)
    {
        var query = _context.Vagas.AsQueryable();

        if (!string.IsNullOrWhiteSpace(cidade))
            query = query.Where(v => v.Cidade == cidade);

        if (!string.IsNullOrWhiteSpace(estado))
            query = query.Where(v => v.Estado == estado);

        return await query.Include(v => v.Empresa).ToListAsync();
    }

    public async Task<int> CountAsync(string? titulo, TipoVaga? tipo, string? cidade)
    {
        var query = _context.Vagas.AsQueryable();

        if (!string.IsNullOrWhiteSpace(titulo))
            query = query.Where(v => v.Titulo.Contains(titulo));

        if (tipo.HasValue)
            query = query.Where(v => v.TipoVaga == tipo.Value);

        if (!string.IsNullOrWhiteSpace(cidade))
            query = query.Where(v => v.Cidade == cidade);

        return await query.CountAsync();
    }
}
