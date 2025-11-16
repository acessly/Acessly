using Microsoft.EntityFrameworkCore;
using Acessly.Domain.Entities;
using Acessly.Domain.Enums;

namespace Acessly.Infrastructure;

public class AcesslyDbContext : DbContext
{
    public AcesslyDbContext(DbContextOptions<AcesslyDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Candidato> Candidatos { get; set; }
    public DbSet<Vaga> Vagas { get; set; }
    public DbSet<Candidatura> Candidaturas { get; set; }
    public DbSet<SuporteEmpresa> SuportesEmpresa { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("USUARIO");

            entity.HasKey(e => e.IdUsuario);

            entity.Property(e => e.IdUsuario)
                .HasColumnName("ID_USUARIO")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Senha)
                .HasColumnName("SENHA")
                .HasMaxLength(255)
                .IsRequired();

            entity.Property(e => e.TipoUsuario)
                .HasColumnName("TIPO_USUARIO")
                .HasMaxLength(20)
                .IsRequired()
                .HasConversion<string>();

            entity.Property(e => e.Cidade)
                .HasColumnName("CIDADE")
                .HasMaxLength(100);

            entity.Property(e => e.Estado)
                .HasColumnName("ESTADO")
                .HasMaxLength(50);

            entity.Property(e => e.Telefone)
                .HasColumnName("TELEFONE")
                .HasMaxLength(20);

            entity.HasIndex(e => e.Email).IsUnique();

            // Relacionamentos
            entity.HasOne(u => u.Empresa)
                .WithOne(e => e.Usuario)
                .HasForeignKey<Empresa>(e => e.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(u => u.Candidato)
                .WithOne(c => c.Usuario)
                .HasForeignKey<Candidato>(c => c.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.ToTable("EMPRESA");

            entity.HasKey(e => e.IdEmpresa);

            entity.Property(e => e.IdEmpresa)
                .HasColumnName("ID_EMPRESA")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.IdUsuario)
                .HasColumnName("ID_USUARIO")
                .IsRequired();

            entity.Property(e => e.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Setor)
                .HasColumnName("SETOR")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.NivelAcessibilidade)
                .HasColumnName("NIVEL_ACESSIBILIDADE")
                .HasMaxLength(50)
                .IsRequired()
                .HasConversion<string>();

            entity.Property(e => e.Site)
                .HasColumnName("SITE")
                .HasMaxLength(255);

            entity.Property(e => e.Descricao)
                .HasColumnName("DESCRICAO")
                .HasColumnType("CLOB");
        });

        modelBuilder.Entity<Candidato>(entity =>
        {
            entity.ToTable("CANDIDATO");

            entity.HasKey(e => e.IdCandidato);

            entity.Property(e => e.IdCandidato)
                .HasColumnName("ID_CANDIDATO")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.IdUsuario)
                .HasColumnName("ID_USUARIO")
                .IsRequired();

            entity.Property(e => e.TipoDeficiencia)
                .HasColumnName("TIPO_DEFICIENCIA")
                .HasMaxLength(100)
                .IsRequired()
                .HasConversion<string>();

            entity.Property(e => e.Habilidades)
                .HasColumnName("HABILIDADES")
                .HasColumnType("CLOB");

            entity.Property(e => e.Experiencia)
                .HasColumnName("EXPERIENCIA")
                .HasMaxLength(255);

            entity.Property(e => e.AcessibilidadeNecessaria)
                .HasColumnName("ACESSIBILIDADE_NECESSARIA")
                .HasMaxLength(255)
                .IsRequired();
        });

        modelBuilder.Entity<Vaga>(entity =>
        {
            entity.ToTable("VAGA");

            entity.HasKey(e => e.IdVaga);

            entity.Property(e => e.IdVaga)
                .HasColumnName("ID_VAGA")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.IdEmpresa)
                .HasColumnName("ID_EMPRESA")
                .IsRequired();

            entity.Property(e => e.Titulo)
                .HasColumnName("TITULO")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Descricao)
                .HasColumnName("DESCRICAO")
                .HasColumnType("CLOB");

            entity.Property(e => e.TipoVaga)
                .HasColumnName("TIPO_VAGA")
                .HasMaxLength(30)
                .IsRequired()
                .HasConversion<string>();

            entity.Property(e => e.Cidade)
                .HasColumnName("CIDADE")
                .HasMaxLength(100);

            entity.Property(e => e.Estado)
                .HasColumnName("ESTADO")
                .HasMaxLength(50);

            entity.Property(e => e.Salario)
                .HasColumnName("SALARIO")
                .HasPrecision(10, 2);

            entity.Property(e => e.AcessibilidadeOferecida)
                .HasColumnName("ACESSIBILIDADE_OFERECIDA")
                .HasMaxLength(255)
                .IsRequired();

            // Relacionamento com Empresa
            entity.HasOne(v => v.Empresa)
                .WithMany(e => e.Vagas)
                .HasForeignKey(v => v.IdEmpresa)
                .HasConstraintName("FK_VAGA_EMPRESA")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Candidatura>(entity =>
        {
            entity.ToTable("CANDIDATURA");

            entity.HasKey(e => e.IdCandidatura);

            entity.Property(e => e.IdCandidatura)
                .HasColumnName("ID_CANDIDATURA")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.IdCandidato)
                .HasColumnName("ID_CANDIDATO")
                .IsRequired();

            entity.Property(e => e.IdVaga)
                .HasColumnName("ID_VAGA")
                .IsRequired();

            entity.Property(e => e.DataCandidatura)
                .HasColumnName("DATA_CANDIDATURA")
                .IsRequired();

            entity.Property(e => e.Status)
                .HasColumnName("STATUS")
                .HasMaxLength(20)
                .IsRequired()
                .HasConversion<string>()
                .HasDefaultValue(StatusCandidatura.EmAnalise);

            // Relacionamento com Candidato
            entity.HasOne(c => c.Candidato)
                .WithMany(ca => ca.Candidaturas)
                .HasForeignKey(c => c.IdCandidato)
                .HasConstraintName("FK_CANDIDATURA_CANDIDATO")
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento com Vaga
            entity.HasOne(c => c.Vaga)
                .WithMany(v => v.Candidaturas)
                .HasForeignKey(c => c.IdVaga)
                .HasConstraintName("FK_CANDIDATURA_VAGA")
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<SuporteEmpresa>(entity =>
        {
            entity.ToTable("SUPORTE_EMPRESA");

            entity.HasKey(e => e.IdSuporte);

            entity.Property(e => e.IdSuporte)
                .HasColumnName("ID_SUPORTE")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.IdEmpresa)
                .HasColumnName("ID_EMPRESA")
                .IsRequired();

            entity.Property(e => e.TipoSuporte)
                .HasColumnName("TIPO_SUPORTE")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(255)
                .IsRequired();

            // Relacionamento com Empresa
            entity.HasOne(s => s.Empresa)
                .WithMany(e => e.Suportes)
                .HasForeignKey(s => s.IdEmpresa)
                .HasConstraintName("FK_SUPORTE_EMPRESA")
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
