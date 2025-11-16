using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acessly.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    ID_USUARIO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false),
                    TIPO_USUARIO = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    CIDADE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    ESTADO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    TELEFONE = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.ID_USUARIO);
                });

            migrationBuilder.CreateTable(
                name: "CANDIDATO",
                columns: table => new
                {
                    ID_CANDIDATO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_USUARIO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    TIPO_DEFICIENCIA = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    HABILIDADES = table.Column<string>(type: "CLOB", nullable: true),
                    EXPERIENCIA = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true),
                    ACESSIBILIDADE_NECESSARIA = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CANDIDATO", x => x.ID_CANDIDATO);
                    table.ForeignKey(
                        name: "FK_CANDIDATO_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EMPRESA",
                columns: table => new
                {
                    ID_EMPRESA = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_USUARIO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    NOME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    SETOR = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    NIVEL_ACESSIBILIDADE = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    SITE = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: true),
                    DESCRICAO = table.Column<string>(type: "CLOB", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPRESA", x => x.ID_EMPRESA);
                    table.ForeignKey(
                        name: "FK_EMPRESA_USUARIO_ID_USUARIO",
                        column: x => x.ID_USUARIO,
                        principalTable: "USUARIO",
                        principalColumn: "ID_USUARIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SUPORTE_EMPRESA",
                columns: table => new
                {
                    ID_SUPORTE = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_EMPRESA = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    TIPO_SUPORTE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DESCRICAO = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SUPORTE_EMPRESA", x => x.ID_SUPORTE);
                    table.ForeignKey(
                        name: "FK_SUPORTE_EMPRESA",
                        column: x => x.ID_EMPRESA,
                        principalTable: "EMPRESA",
                        principalColumn: "ID_EMPRESA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VAGA",
                columns: table => new
                {
                    ID_VAGA = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_EMPRESA = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    TITULO = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    DESCRICAO = table.Column<string>(type: "CLOB", nullable: true),
                    TIPO_VAGA = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    CIDADE = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    ESTADO = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true),
                    SALARIO = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: true),
                    ACESSIBILIDADE_OFERECIDA = table.Column<string>(type: "NVARCHAR2(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VAGA", x => x.ID_VAGA);
                    table.ForeignKey(
                        name: "FK_VAGA_EMPRESA",
                        column: x => x.ID_EMPRESA,
                        principalTable: "EMPRESA",
                        principalColumn: "ID_EMPRESA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CANDIDATURA",
                columns: table => new
                {
                    ID_CANDIDATURA = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_CANDIDATO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_VAGA = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    DATA_CANDIDATURA = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    STATUS = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false, defaultValue: "EmAnalise")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CANDIDATURA", x => x.ID_CANDIDATURA);
                    table.ForeignKey(
                        name: "FK_CANDIDATURA_CANDIDATO",
                        column: x => x.ID_CANDIDATO,
                        principalTable: "CANDIDATO",
                        principalColumn: "ID_CANDIDATO",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CANDIDATURA_VAGA",
                        column: x => x.ID_VAGA,
                        principalTable: "VAGA",
                        principalColumn: "ID_VAGA",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CANDIDATO_ID_USUARIO",
                table: "CANDIDATO",
                column: "ID_USUARIO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CANDIDATURA_ID_CANDIDATO",
                table: "CANDIDATURA",
                column: "ID_CANDIDATO");

            migrationBuilder.CreateIndex(
                name: "IX_CANDIDATURA_ID_VAGA",
                table: "CANDIDATURA",
                column: "ID_VAGA");

            migrationBuilder.CreateIndex(
                name: "IX_EMPRESA_ID_USUARIO",
                table: "EMPRESA",
                column: "ID_USUARIO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SUPORTE_EMPRESA_ID_EMPRESA",
                table: "SUPORTE_EMPRESA",
                column: "ID_EMPRESA");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_EMAIL",
                table: "USUARIO",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VAGA_ID_EMPRESA",
                table: "VAGA",
                column: "ID_EMPRESA");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CANDIDATURA");

            migrationBuilder.DropTable(
                name: "SUPORTE_EMPRESA");

            migrationBuilder.DropTable(
                name: "CANDIDATO");

            migrationBuilder.DropTable(
                name: "VAGA");

            migrationBuilder.DropTable(
                name: "EMPRESA");

            migrationBuilder.DropTable(
                name: "USUARIO");
        }
    }
}
