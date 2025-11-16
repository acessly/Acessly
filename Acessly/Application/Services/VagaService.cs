using Acessly.Application.DTOs.Common;
using Acessly.Application.DTOs.Vaga;
using Acessly.Application.Interfaces;
using Acessly.Domain.Entities;
using Acessly.Domain.Enums;
using Acessly.Domain.Interfaces;

namespace Acessly.Application.Services
{
    public class VagaService : IVagaService
    {
        private readonly IVagaRepository _repository;
        private readonly IEmpresaRepository _empresaRepository;

        public VagaService(IVagaRepository repository, IEmpresaRepository empresaRepository)
        {
            _repository = repository;
            _empresaRepository = empresaRepository;
        }

        public async Task<VagaResponseDto> CreateAsync(VagaCreateDto dto)
        {
            // Validação: empresa existe?
            var empresa = await _empresaRepository.GetByIdAsync(dto.IdEmpresa);
            if (empresa == null)
                throw new KeyNotFoundException($"Empresa com ID {dto.IdEmpresa} não encontrada");

            // Converter string para enum
            if (!Enum.TryParse<TipoVaga>(dto.TipoVaga, out var tipoVaga))
                throw new ArgumentException("Tipo de vaga inválido");

            var vaga = new Vaga
            {
                IdEmpresa = dto.IdEmpresa,
                Titulo = dto.Titulo,
                Descricao = dto.Descricao,
                TipoVaga = tipoVaga,
                Cidade = dto.Cidade,
                Estado = dto.Estado,
                Salario = dto.Salario,
                AcessibilidadeOferecida = dto.AcessibilidadeOferecida
            };

            vaga.ValidarSalario();

            var created = await _repository.AddAsync(vaga);

            return MapToResponse(created);
        }

        public async Task<VagaResponseDto?> GetByIdAsync(long id)
        {
            var vaga = await _repository.GetByIdAsync(id);
            return vaga != null ? MapToResponse(vaga) : null;
        }

        public async Task<PagedResultDto<VagaResponseDto>> SearchAsync(
        string? titulo,
        string? tipo,
        string? cidade,
        int page = 1,
        int pageSize = 10)
        {
            TipoVaga? tipoVaga = null;
            if (!string.IsNullOrWhiteSpace(tipo))
            {
                if (!Enum.TryParse<TipoVaga>(tipo, out var tipoEnum))
                    throw new ArgumentException("Tipo de vaga inválido");
                tipoVaga = tipoEnum;
            }

            var vagas = await _repository.SearchAsync(titulo, tipoVaga, cidade, page, pageSize);
            var totalCount = await _repository.CountAsync(titulo, tipoVaga, cidade);

            var items = vagas.Select(MapToResponse);

            return new PagedResultDto<VagaResponseDto>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<VagaResponseDto> UpdateAsync(long id, VagaUpdateDto dto)
        {
            var vaga = await _repository.GetByIdAsync(id);
            if (vaga == null)
                throw new KeyNotFoundException($"Vaga com ID {id} não encontrada");

            if (!string.IsNullOrWhiteSpace(dto.Titulo))
                vaga.Titulo = dto.Titulo;

            if (dto.Descricao != null)
                vaga.Descricao = dto.Descricao;

            if (!string.IsNullOrWhiteSpace(dto.TipoVaga))
            {
                if (!Enum.TryParse<TipoVaga>(dto.TipoVaga, out var tipo))
                    throw new ArgumentException("Tipo de vaga inválido");
                vaga.TipoVaga = tipo;
            }

            if (dto.Cidade != null)
                vaga.Cidade = dto.Cidade;

            if (dto.Estado != null)
                vaga.Estado = dto.Estado;

            if (dto.Salario.HasValue)
            {
                vaga.Salario = dto.Salario;
                vaga.ValidarSalario();
            }

            if (!string.IsNullOrWhiteSpace(dto.AcessibilidadeOferecida))
                vaga.AcessibilidadeOferecida = dto.AcessibilidadeOferecida;

            await _repository.UpdateAsync(vaga);

            return MapToResponse(vaga);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<VagaResponseDto>> GetByEmpresaAsync(long idEmpresa)
        {
            var vagas = await _repository.GetByEmpresaAsync(idEmpresa);
            return vagas.Select(MapToResponse);
        }

        public async Task<IEnumerable<VagaResponseDto>> GetAllAsync()
        {
            var vagas = await _repository.GetAllAsync();
            return vagas.Select(MapToResponse);
        }


        private static VagaResponseDto MapToResponse(Vaga vaga) => new()
        {
            IdVaga = vaga.IdVaga,
            IdEmpresa = vaga.IdEmpresa,
            Titulo = vaga.Titulo,
            Descricao = vaga.Descricao,
            TipoVaga = vaga.TipoVaga.ToString(),
            Cidade = vaga.Cidade,
            Estado = vaga.Estado,
            Salario = vaga.Salario,
            AcessibilidadeOferecida = vaga.AcessibilidadeOferecida
        };
    }
}
