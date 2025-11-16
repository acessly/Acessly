using Acessly.Application.DTOs.Candidatura;
using Acessly.Application.Interfaces;
using Acessly.Domain.Entities;
using Acessly.Domain.Enums;
using Acessly.Domain.Interfaces;

namespace Acessly.Application.Services
{
    public class CandidaturaService : ICandidaturaService
    {
        private readonly ICandidaturaRepository _repository;
        private readonly ICandidatoRepository _candidatoRepository;
        private readonly IVagaRepository _vagaRepository;

        public CandidaturaService(
        ICandidaturaRepository repository,
        ICandidatoRepository candidatoRepository,
        IVagaRepository vagaRepository)
        {
            _repository = repository;
            _candidatoRepository = candidatoRepository;
            _vagaRepository = vagaRepository;
        }

        public async Task<CandidaturaResponseDto> CreateAsync(CandidaturaCreateDto dto)
        {
            // Validação: candidato existe?
            var candidato = await _candidatoRepository.GetByIdAsync(dto.IdCandidato);
            if (candidato == null)
                throw new KeyNotFoundException($"Candidato com ID {dto.IdCandidato} não encontrado");

            // Validação: vaga existe?
            var vaga = await _vagaRepository.GetByIdAsync(dto.IdVaga);
            if (vaga == null)
                throw new KeyNotFoundException($"Vaga com ID {dto.IdVaga} não encontrada");

            // Validação: candidato já se candidatou a esta vaga?
            var jaExiste = await _repository.ExisteCandidaturaAsync(dto.IdCandidato, dto.IdVaga);
            if (jaExiste)
                throw new InvalidOperationException("Candidato já se candidatou a esta vaga");

            var candidatura = new Candidatura
            {
                IdCandidato = dto.IdCandidato,
                IdVaga = dto.IdVaga,
                DataCandidatura = DateTime.Now,
                Status = StatusCandidatura.EmAnalise
            };

            var created = await _repository.AddAsync(candidatura);

            return MapToResponse(created);
        }

        public async Task<CandidaturaResponseDto?> GetByIdAsync(long id)
        {
            var candidatura = await _repository.GetByIdAsync(id);
            return candidatura != null ? MapToResponse(candidatura) : null;
        }

        public async Task<IEnumerable<CandidaturaResponseDto>> GetAllAsync()
        {
            var candidaturas = await _repository.GetAllAsync();
            return candidaturas.Select(MapToResponse);
        }

        public async Task<CandidaturaResponseDto> UpdateStatusAsync(long id, CandidaturaUpdateStatusDto dto)
        {
            var candidatura = await _repository.GetByIdAsync(id);
            if (candidatura == null)
                throw new KeyNotFoundException($"Candidatura com ID {id} não encontrada");

            // Converter string para enum
            if (!Enum.TryParse<StatusCandidatura>(dto.Status, out var status))
                throw new ArgumentException("Status inválido");

            // Usar os métodos de regra de negócio da entidade
            if (status == StatusCandidatura.Aprovado)
                candidatura.Aprovar();
            else if (status == StatusCandidatura.Reprovado)
                candidatura.Reprovar();
            else
                candidatura.Status = status;

            await _repository.UpdateAsync(candidatura);

            return MapToResponse(candidatura);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<CandidaturaResponseDto>> GetByCandidatoAsync(long idCandidato)
        {
            var candidaturas = await _repository.GetByCandidatoAsync(idCandidato);
            return candidaturas.Select(MapToResponse);
        }

        public async Task<IEnumerable<CandidaturaResponseDto>> GetByVagaAsync(long idVaga)
        {
            var candidaturas = await _repository.GetByVagaAsync(idVaga);
            return candidaturas.Select(MapToResponse);
        }

        private static CandidaturaResponseDto MapToResponse(Candidatura candidatura) => new()
        {
            IdCandidatura = candidatura.IdCandidatura,
            IdCandidato = candidatura.IdCandidato,
            IdVaga = candidatura.IdVaga,
            DataCandidatura = candidatura.DataCandidatura,
            Status = candidatura.Status.ToString()
        };
    }
}
