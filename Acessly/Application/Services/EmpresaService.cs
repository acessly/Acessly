using Acessly.Application.DTOs.Empresa;
using Acessly.Application.Interfaces;
using Acessly.Domain.Entities;
using Acessly.Domain.Enums;
using Acessly.Domain.Interfaces;

namespace Acessly.Application.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public EmpresaService(IEmpresaRepository repository, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<EmpresaResponseDto> CreateAsync(EmpresaCreateDto dto)
        {
            // Validação: usuário existe?
            var usuario = await _usuarioRepository.GetByIdAsync(dto.IdUsuario);
            if (usuario == null)
                throw new KeyNotFoundException($"Usuário com ID {dto.IdUsuario} não encontrado");

            // Validação: usuário é do tipo Empresa?
            if (usuario.TipoUsuario != TipoUsuario.Empresa)
                throw new InvalidOperationException("Usuário não é do tipo Empresa");

            // Converter string para enum
            if (!Enum.TryParse<NivelAcessibilidade>(dto.NivelAcessibilidade, out var nivelAcessibilidade))
                throw new ArgumentException("Nível de acessibilidade inválido");

            var empresa = new Empresa
            {
                IdUsuario = dto.IdUsuario,
                Nome = dto.Nome,
                Setor = dto.Setor,
                NivelAcessibilidade = nivelAcessibilidade,
                Site = dto.Site,
                Descricao = dto.Descricao
            };

            empresa.ValidarNome();

            var created = await _repository.AddAsync(empresa);

            return MapToResponse(created);
        }

        public async Task<EmpresaResponseDto?> GetByIdAsync(long id)
        {
            var empresa = await _repository.GetByIdAsync(id);
            return empresa != null ? MapToResponse(empresa) : null;
        }

        public async Task<IEnumerable<EmpresaResponseDto>> GetAllAsync()
        {
            var empresas = await _repository.GetAllAsync();
            return empresas.Select(MapToResponse);
        }

        public async Task<EmpresaResponseDto> UpdateAsync(long id, EmpresaUpdateDto dto)
        {
            var empresa = await _repository.GetByIdAsync(id);
            if (empresa == null)
                throw new KeyNotFoundException($"Empresa com ID {id} não encontrada");

            if (!string.IsNullOrWhiteSpace(dto.Nome))
            {
                empresa.Nome = dto.Nome;
                empresa.ValidarNome();
            }

            if (!string.IsNullOrWhiteSpace(dto.Setor))
                empresa.Setor = dto.Setor;

            if (!string.IsNullOrWhiteSpace(dto.NivelAcessibilidade))
            {
                if (!Enum.TryParse<NivelAcessibilidade>(dto.NivelAcessibilidade, out var nivel))
                    throw new ArgumentException("Nível de acessibilidade inválido");
                empresa.NivelAcessibilidade = nivel;
            }

            if (dto.Site != null)
                empresa.Site = dto.Site;

            if (dto.Descricao != null)
                empresa.Descricao = dto.Descricao;

            await _repository.UpdateAsync(empresa);

            return MapToResponse(empresa);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<EmpresaResponseDto>> GetBySetorAsync(string setor)
        {
            var empresas = await _repository.GetBySetorAsync(setor);
            return empresas.Select(MapToResponse);
        }

        public async Task<IEnumerable<EmpresaResponseDto>> GetByNivelAcessibilidadeAsync(string nivel)
        {
            if (!Enum.TryParse<NivelAcessibilidade>(nivel, out var nivelEnum))
                throw new ArgumentException("Nível de acessibilidade inválido");

            var empresas = await _repository.GetByNivelAcessibilidadeAsync(nivelEnum);
            return empresas.Select(MapToResponse);
        }

        private static EmpresaResponseDto MapToResponse(Empresa empresa) => new()
        {
            IdEmpresa = empresa.IdEmpresa,
            IdUsuario = empresa.IdUsuario,
            Nome = empresa.Nome,
            Setor = empresa.Setor,
            NivelAcessibilidade = empresa.NivelAcessibilidade.ToString(),
            Site = empresa.Site,
            Descricao = empresa.Descricao
        };
    }
}
