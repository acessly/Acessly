using Acessly.Application.DTOs.Candidato;
using Acessly.Application.Interfaces;
using Acessly.Domain.Entities;
using Acessly.Domain.Enums;
using Acessly.Domain.Interfaces;

namespace Acessly.Application.Services
{
    public class CandidatoService : ICandidatoService
    {
        private readonly ICandidatoRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public CandidatoService(ICandidatoRepository repository, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<CandidatoResponseDto> CreateAsync(CandidatoCreateDto dto)
        {
            // Validação: usuário existe?
            var usuario = await _usuarioRepository.GetByIdAsync(dto.IdUsuario);
            if (usuario == null)
                throw new KeyNotFoundException($"Usuário com ID {dto.IdUsuario} não encontrado");

            // Validação: usuário é do tipo Candidato?
            if (usuario.TipoUsuario != TipoUsuario.Candidato)
                throw new InvalidOperationException("Usuário não é do tipo Candidato");

            // Converter string para enum
            if (!Enum.TryParse<TipoDeficiencia>(dto.TipoDeficiencia, out var tipoDeficiencia))
                throw new ArgumentException("Tipo de deficiência inválido");

            var candidato = new Candidato
            {
                IdUsuario = dto.IdUsuario,
                TipoDeficiencia = tipoDeficiencia,
                Habilidades = dto.Habilidades,
                Experiencia = dto.Experiencia,
                AcessibilidadeNecessaria = dto.AcessibilidadeNecessaria
            };

            candidato.ValidarAcessibilidadeNecessaria();

            var created = await _repository.AddAsync(candidato);

            return MapToResponse(created);
        }

        public async Task<CandidatoResponseDto?> GetByIdAsync(long id)
        {
            var candidato = await _repository.GetByIdAsync(id);
            return candidato != null ? MapToResponse(candidato) : null;
        }

        public async Task<IEnumerable<CandidatoResponseDto>> GetAllAsync()
        {
            var candidatos = await _repository.GetAllAsync();
            return candidatos.Select(MapToResponse);
        }

        public async Task<CandidatoResponseDto> UpdateAsync(long id, CandidatoUpdateDto dto)
        {
            var candidato = await _repository.GetByIdAsync(id);
            if (candidato == null)
                throw new KeyNotFoundException($"Candidato com ID {id} não encontrado");

            if (!string.IsNullOrWhiteSpace(dto.TipoDeficiencia))
            {
                if (!Enum.TryParse<TipoDeficiencia>(dto.TipoDeficiencia, out var tipo))
                    throw new ArgumentException("Tipo de deficiência inválido");
                candidato.TipoDeficiencia = tipo;
            }

            if (dto.Habilidades != null)
                candidato.Habilidades = dto.Habilidades;

            if (dto.Experiencia != null)
                candidato.Experiencia = dto.Experiencia;

            if (!string.IsNullOrWhiteSpace(dto.AcessibilidadeNecessaria))
            {
                candidato.AcessibilidadeNecessaria = dto.AcessibilidadeNecessaria;
                candidato.ValidarAcessibilidadeNecessaria();
            }

            await _repository.UpdateAsync(candidato);

            return MapToResponse(candidato);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CandidatoResponseDto>> GetByTipoDeficienciaAsync(string tipo)
        {
            if (!Enum.TryParse<TipoDeficiencia>(tipo, out var tipoEnum))
                throw new ArgumentException("Tipo de deficiência inválido");

            var candidatos = await _repository.GetByTipoDeficienciaAsync(tipoEnum);
            return candidatos.Select(MapToResponse);
        }

        private static CandidatoResponseDto MapToResponse(Candidato candidato) => new()
        {
            IdCandidato = candidato.IdCandidato,
            IdUsuario = candidato.IdUsuario,
            TipoDeficiencia = candidato.TipoDeficiencia.ToString(),
            Habilidades = candidato.Habilidades,
            Experiencia = candidato.Experiencia,
            AcessibilidadeNecessaria = candidato.AcessibilidadeNecessaria
        };
    }
}
