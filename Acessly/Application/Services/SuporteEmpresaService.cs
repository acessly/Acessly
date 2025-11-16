using Acessly.Application.DTOs.SuporteEmpresa;
using Acessly.Application.Interfaces;
using Acessly.Domain.Entities;
using Acessly.Domain.Interfaces;

namespace Acessly.Application.Services
{
    public class SuporteEmpresaService : ISuporteEmpresaService
    {
        private readonly ISuporteEmpresaRepository _repository;
        private readonly IEmpresaRepository _empresaRepository;

        public SuporteEmpresaService(ISuporteEmpresaRepository repository, IEmpresaRepository empresaRepository)
        {
            _repository = repository;
            _empresaRepository = empresaRepository;
        }

        public async Task<SuporteEmpresaResponseDto> CreateAsync(SuporteEmpresaCreateDto dto)
        {
            // Validação: empresa existe?
            var empresa = await _empresaRepository.GetByIdAsync(dto.IdEmpresa);
            if (empresa == null)
                throw new KeyNotFoundException($"Empresa com ID {dto.IdEmpresa} não encontrada");

            var suporte = new SuporteEmpresa
            {
                IdEmpresa = dto.IdEmpresa,
                TipoSuporte = dto.TipoSuporte,
                Descricao = dto.Descricao
            };

            suporte.ValidarTipoSuporte();
            suporte.ValidarDescricao();

            var created = await _repository.AddAsync(suporte);

            return MapToResponse(created);
        }

        public async Task<SuporteEmpresaResponseDto?> GetByIdAsync(long id)
        {
            var suporte = await _repository.GetByIdAsync(id);
            return suporte != null ? MapToResponse(suporte) : null;
        }

        public async Task<IEnumerable<SuporteEmpresaResponseDto>> GetAllAsync()
        {
            var suportes = await _repository.GetAllAsync();
            return suportes.Select(MapToResponse);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<SuporteEmpresaResponseDto>> GetByEmpresaAsync(long idEmpresa)
        {
            var suportes = await _repository.GetByEmpresaAsync(idEmpresa);
            return suportes.Select(MapToResponse);
        }

        private static SuporteEmpresaResponseDto MapToResponse(SuporteEmpresa suporte) => new()
        {
            IdSuporte = suporte.IdSuporte,
            IdEmpresa = suporte.IdEmpresa,
            TipoSuporte = suporte.TipoSuporte,
            Descricao = suporte.Descricao
        };
    }
}
