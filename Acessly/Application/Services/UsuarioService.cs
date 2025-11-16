using BCrypt.Net;
using Acessly.Application.DTOs.Usuario;
using Acessly.Application.Interfaces;
using Acessly.Domain.Entities;
using Acessly.Domain.Enums;
using Acessly.Domain.Interfaces;

namespace Acessly.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<UsuarioResponseDto> CreateAsync(UsuarioCreateDto dto)
        {
            var emailExists = await _repository.EmailExistsAsync(dto.Email);
            if (emailExists)
                throw new InvalidOperationException("Email já cadastrado");

            if (!Enum.TryParse<TipoUsuario>(dto.TipoUsuario, out var tipoUsuario))
                throw new ArgumentException("Tipo de usuário inválido");

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = BCrypt.Net.BCrypt.HashPassword(dto.Senha), // Hash da senha
                TipoUsuario = tipoUsuario,
                Cidade = dto.Cidade,
                Estado = dto.Estado,
                Telefone = dto.Telefone
            };

            usuario.ValidarEmail();
            usuario.ValidarSenha();

            var created = await _repository.AddAsync(usuario);

            return MapToResponse(created);
        }

        public async Task<UsuarioResponseDto?> GetByIdAsync(long id)
        {
            var usuario = await _repository.GetByIdAsync(id);
            return usuario != null ? MapToResponse(usuario) : null;
        }

        public async Task<IEnumerable<UsuarioResponseDto>> GetAllAsync()
        {
            var usuarios = await _repository.GetAllAsync();
            return usuarios.Select(MapToResponse);
        }

        public async Task<UsuarioResponseDto> UpdateAsync(long id, UsuarioUpdateDto dto)
        {
            var usuario = await _repository.GetByIdAsync(id);
            if (usuario == null)
                throw new KeyNotFoundException($"Usuário com ID {id} não encontrado");

            if (!string.IsNullOrWhiteSpace(dto.Nome))
                usuario.Nome = dto.Nome;

            if (dto.Cidade != null)
                usuario.Cidade = dto.Cidade;

            if (dto.Estado != null)
                usuario.Estado = dto.Estado;

            if (dto.Telefone != null)
                usuario.Telefone = dto.Telefone;

            await _repository.UpdateAsync(usuario);

            return MapToResponse(usuario);
        }

        public async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<UsuarioResponseDto?> GetByEmailAsync(string email)
        {
            var usuario = await _repository.GetByEmailAsync(email);
            return usuario != null ? MapToResponse(usuario) : null;
        }

        private static UsuarioResponseDto MapToResponse(Usuario usuario) => new()
        {
            IdUsuario = usuario.IdUsuario,
            Nome = usuario.Nome,
            Email = usuario.Email,
            TipoUsuario = usuario.TipoUsuario.ToString(),
            Cidade = usuario.Cidade,
            Estado = usuario.Estado,
            Telefone = usuario.Telefone
        };
    }
}
