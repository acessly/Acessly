using Acessly.Application.Interfaces;
using Acessly.Application.Services;
using Acessly.Domain.Interfaces;
using Acessly.Infrastructure;
using Acessly.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AcesslyDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("OracleConnection")));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();
builder.Services.AddScoped<ICandidatoRepository, CandidatoRepository>();
builder.Services.AddScoped<IVagaRepository, VagaRepository>();
builder.Services.AddScoped<ICandidaturaRepository, CandidaturaRepository>();
builder.Services.AddScoped<ISuporteEmpresaRepository, SuporteEmpresaRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<ICandidatoService, CandidatoService>();
builder.Services.AddScoped<IVagaService, VagaService>();
builder.Services.AddScoped<ICandidaturaService, CandidaturaService>();
builder.Services.AddScoped<ISuporteEmpresaService, SuporteEmpresaService>();


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
