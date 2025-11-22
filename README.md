![Imagem](https://drive.google.com/uc?export=view&id=1jxtajfbO8P61xZxIJa6VCgLjaGXQtRD-)

![.NET](https://img.shields.io/badge/.NET-000000?style=flat&logo=dotnet&logoColor=512BD4)
![C#](https://img.shields.io/badge/C%23-000000?style=flat&logo=csharp&logoColor=239120)
![Oracle Database](https://img.shields.io/badge/Oracle%20Database-000000?style=flat&logo=oracle&logoColor=F80000)
![API RESTful](https://img.shields.io/badge/API%20RESTful-000000?style=flat&logo=fastapi&logoColor=009688)
![Swagger](https://img.shields.io/badge/Swagger-000000?style=flat&logo=swagger&logoColor=85EA2D)

## 🧿 Visão geral
O Acessly é uma plataforma de gestão acessível que integra uma **API RESTful** feita em **ASP.NET Core** com uma **interface web MVC**, fornecendo funcionalidades para gerenciamento de usuários, empresas, candidaturas, vagas e suportes empresariais.

### 👩‍🦽‍➡️ Conectando talentos PCDs a oportunidades reais de trabalho

O Acessly conecta pessoas com deficiência a oportunidades reais de forma inclusiva, transparente e justa, promovendo match entre candidatos e empresas baseado em competências e acessibilidade.

- ☑️ Competências validadas
- ☑️ Match real entre perfil e vaga
- ☑️ Transparência total
- ☑️ Dignidade e equidade

## 🏗️ Decisões arquiteturais

- Arquitetura baseada em microserviços/API REST para backend e aplicação MVC no frontend.
- Comunicação entre **front-end** e **back-end** feita via **HTTP** usando **HttpClient**.
- Uso de **Entity Framework Core** para ORM e **migrations** para banco de dados Oracle.
- Validações implementadas nos ViewModels com DataAnnotations.
- Interface responsiva construída com **Bootstrap 5** e Bootstrap Icons.
- Documentação e testes facilitados com **Swagger** para a API.

### 📁 Estrutura do projeto

A solução está dividida em dois projetos principais:

**Acessly (API)**

A API RESTful em ASP.NET Core segue a arquitetura em camadas com as seguintes responsabilidades

🎯 **Application** 

   - `DTOs`: objetos para transferência de dados entre camadas, isolando a estrutura interna das entidades.

   - `Exceptions`: classes de exceções customizadas para tratamento de erros específicos da aplicação.

   - `Interfaces`: contratos (interfaces) para serviços da camada de aplicação.

   - `Services`: implementação da lógica de negócio e orquestração entre repositórios e controllers.

🌐 **Controllers**

   - Endpoints da API RESTful que recebem requisições HTTP.

   - Utilizam serviços da camada Application para processar requests.

   - Retornam DTOs para garantir separação entre entidades de domínio e dados expostos.

💎 **Domain**

   - `Entities`: entidades de domínio (`Candidato`, `Candidatura`, `Empresa`, `SuporteEmpresa`, `Usuario`, `Vaga`) com regras de negócio e comportamento.

   - `Enums`: enumerações como `TipoDeficiencia`, `StatusCandidatura`, etc., garantindo tipagem forte.

   - `Interfaces`: contratos para repositórios, seguindo o padrão Repository.

🗄️ **Infrastructure**

   - `Repositories`: implementação dos repositórios usando Entity Framework Core para acesso ao banco Oracle.

   - AcesslyDbContext: contexto do Entity Framework que mapeia entidades para o banco de dados.

   - AcesslyDbContextFactory: factory para criação do DbContext, útil para migrations e testes.

📦 **Migrations**

   - Scripts de migração do Entity Framework Core para versionamento e evolução do schema do banco de dados Oracle.

---

**Acessly.UI (Aplicação MVC)**

A interface web MVC consome a API através de HttpClient:

🎮 **Controllers**

   - Controladores MVC que intermediam entre Views e a API.

   - Realizam chamadas HTTP (GET, POST, PUT, DELETE) para os endpoints da API.

   - Tratam erros e validações do lado do servidor.

📋 **Models/ViewModels**

   - `ViewModels` específicos para as Views, contendo validações via DataAnnotations.

   - Separação entre modelos de domínio (API) e modelos de apresentação (UI).

🎨 **Views**

   - Views Razor renderizadas no servidor.

   - Interface responsiva construída com Bootstrap 5 e Bootstrap Icons.

   - Validação client-side usando jQuery Validation.

## 📈 Diagrama de arquitetura

```text
┌─────────────────────────────────────────────────────────────────────┐
│                          CAMADA DE APRESENTAÇÃO                     │
│                                                                     │
│  ┌──────────────────────┐              ┌─────────────────────────┐  │
│  │      Cliente Web     │              │       Swagger UI        │  │
│  │      (Navegador)     │              │   (Documentação API)    │  │
│  └──────────┬───────────┘              └───────────┬─────────────┘  │
│             │                                       │               │
│             │ HTTP/HTTPS                           │ HTTP/HTTPS     │
└─────────────┼───────────────────────────────────────┼───────────────┘
              │                                       │
              ▼                                       ▼
┌─────────────────────────────────────────────────────────────────────┐
│                            ACESSLY.UI (MVC)                         │
│  ┌──────────────────────────────────────────────────────────────┐   │
│  │  Controllers MVC                                             │   │
│  │  (UsuariosController, EmpresasController, etc.)              │   │
│  └──────────────────────┬───────────────────────────────────────┘   │
│                         │                                           │
│  ┌──────────────────────▼──────────────────────────────────────┐    │
│  │  ViewModels (CandidatoViewModel, VagaViewModel, etc.)       │    │
│  └──────────────────────┬──────────────────────────────────────┘    │
│                         │                                           │
│  ┌──────────────────────▼──────────────────────────────────────┐    │
│  │  Views (Razor) + Bootstrap 5 + jQuery Validation            │    │
│  └─────────────────────────────────────────────────────────────┘    │
└─────────────────────────┼───────────────────────────────────────────┘
                          │
                          │ HttpClient (REST API Calls)
                          │
                          ▼
┌─────────────────────────────────────────────────────────────────────┐
│                          ACESSLY (API REST)                         │
│                                                                     │
│  ┌──────────────────────────────────────────────────────────────┐   │
│  │  📡 API CONTROLLERS                                          │   |                                   
│  │  (UsuariosController, CandidatosController, VagasController) │   │
│  └──────────────────────┬───────────────────────────────────────┘   │
│                         │                                           │
│  ┌──────────────────────▼───────────────────────────────────────┐   │
│  │  🎯 APPLICATION LAYER                                        │  │
│  │  ├── Services (Business Logic)                               │   │
│  │  ├── DTOs (Data Transfer Objects)                            │   │
│  │  ├── Interfaces (Contratos de Serviços)                      │   │
│  │  └── Exceptions (Tratamento de erros)                        │   │
│  └──────────────────────┬───────────────────────────────────────┘   │
│                         │                                           │
│  ┌──────────────────────▼───────────────────────────────────────┐   │
│  │  💎 DOMAIN LAYER                                            │    │
│  │  ├── Entities (Usuario, Candidato, Vaga, Empresa, etc.)      │   │
│  │  ├── Enums (TipoDeficiencia, StatusCandidatura, etc.)        │   │
│  │  └── Interfaces (Contratos de Repositórios)                  │   │
│  └──────────────────────┬───────────────────────────────────────┘   │
│                         │                                           │
│  ┌──────────────────────▼───────────────────────────────────────┐   │
│  │  🗄️ INFRASTRUCTURE LAYER                                     │   │
│  │  ├── Repositories (Implementação EF Core)                    │   │
│  │  ├── AcesslyDbContext (Contexto do Banco)                    │   │
│  │  ├── AcesslyDbContextFactory                                 │   │
│  │  └── Migrations (Versionamento do Schema)                    │   │
│  └──────────────────────┬───────────────────────────────────────┘   │
└─────────────────────────┼───────────────────────────────────────────┘
                          │
                          │ Entity Framework Core
                          │
                          ▼
┌─────────────────────────────────────────────────────────────────────┐
│                         🗃️ ORACLE DATABASE                          │
│                                                                     │
│             Tabelas: Usuarios, Candidatos, Vagas, Empresas,         |
│                   Candidaturas, SuportesEmpresa                     │
└─────────────────────────────────────────────────────────────────────┘
```


## ⚙️ Como rodar o projeto

Pré-requisitos

- .NET 9.0
- Oracle Database acessível e configurado
- IDE recomendada: Visual Studio 2022 ou Visual Studio Code

### ⛓️ Migrations

1. Abra o terminal na pasta do projeto **Acessly**:

```bash
dotnet ef migrations add InitialCreate
```

2. Atualize o banco de dados com as migrations:

```bash
dotnet ef database update
```

### 🔐 Variáveis de ambiente

Configure as variáveis necessárias no `appsettings.json`

```json
"ConnectionStrings": {
    "OracleConnection": "User Id=usuarioPassword=senha;Data Source=oracle.fiap.com.br:1521/ORCL;"
}
```

## 🌐 Rotas / Endpoints

**API - Candidatos**

| Método | Endpoint                           | Descrição                              |
| ------ | ---------------------------------- | -------------------------------------- |
| GET    | /api/candidatos                    | Lista todos os candidatos              |
| POST   | /api/candidatos                    | Cria novo candidato                    |
| GET    | /api/candidatos/{id}               | Detalhes do candidato                  |
| PUT    | /api/candidatos/{id}               | Atualiza candidato                     |
| DELETE | /api/candidatos/{id}               | Deleta candidato                       |
| GET    | /api/candidatos/deficiencia/{tipo} | Filtra candidatos por tipo deficiência |

**API - Candidaturas**

| Método | Endpoint                                  | Descrição                          |
| ------ | ----------------------------------------- | ---------------------------------- |
| GET    | /api/candidaturas                         | Lista todas as candidaturas        |
| POST   | /api/candidaturas                         | Cria nova candidatura              |
| GET    | /api/candidaturas/{id}                    | Detalhes da candidatura            |
| DELETE | /api/candidaturas/{id}                    | Deleta candidatura                 |
| PUT    | /api/candidaturas/{id}/status             | Atualiza status da candidatura     |
| GET    | /api/candidaturas/candidato/{idCandidato} | Busca candidaturas de um candidato |
| GET    | /api/candidaturas/vaga/{idVaga}           | Busca candidaturas para uma vaga   |

**API - Empresas**

| Método | Endpoint                             | Descrição                                  |
| ------ | ------------------------------------ | ------------------------------------------ |
| GET    | /api/empresas                        | Lista todas as empresas                    |
| POST   | /api/empresas                        | Cria nova empresa                          |
| GET    | /api/empresas/{id}                   | Detalhes da empresa                        |
| PUT    | /api/empresas/{id}                   | Atualiza empresa                           |
| DELETE | /api/empresas/{id}                   | Deleta empresa                             |
| GET    | /api/empresas/setor/{setor}          | Lista empresas de um setor específico      |
| GET    | /api/empresas/acessibilidade/{nivel} | Lista empresas por nível de acessibilidade |

**API - SuportesEmpresa**

| Método | Endpoint                                 | Descrição                  |
| ------ | ---------------------------------------- | -------------------------- |
| GET    | /api/suportesempresa                     | Lista todos os suportes    |
| POST   | /api/suportesempresa                     | Cria novo suporte          |
| GET    | /api/suportesempresa/{id}                | Detalhes do suporte        |
| DELETE | /api/suportesempresa/{id}                | Deleta suporte             |
| GET    | /api/suportesempresa/empresa/{idEmpresa} | Lista suportes por empresa |

**API - Usuarios**

| Método | Endpoint           | Descrição               |
| ------ | ------------------ | ----------------------- |
| GET    | /api/usuarios      | Lista todos os usuários |
| POST   | /api/usuarios      | Cria novo usuário       |
| GET    | /api/usuarios/{id} | Detalhes do usuário     |
| PUT    | /api/usuarios/{id} | Atualiza usuário        |
| DELETE | /api/usuarios/{id} | Deleta usuário          |

**API - Vagas**

| Método | Endpoint                       | Descrição                |
| ------ | ------------------------------ | ------------------------ |
| GET    | /api/vagas                     | Lista todas as vagas     |
| POST   | /api/vagas                     | Cria nova vaga           |
| GET    | /api/vagas/{id}                | Detalhes da vaga         |
| PUT    | /api/vagas/{id}                | Atualiza vaga            |
| DELETE | /api/vagas/{id}                | Deleta vaga              |
| GET    | /api/vagas/search              | Pesquisa vagas (filtros) |
| GET    | /api/vagas/empresa/{idEmpresa} | Lista vagas por empresa  |

---

### 🖼️ Navegação Web MVC - Rotas principais

| Rota                    | Descrição                         |
| ----------------------- | --------------------------------- |
| /Usuarios/Index         | Listagem de usuários              |
| /Empresas/Index         | Listagem de empresas              |
| /Candidatos/Index       | Listagem de candidatos            |
| /Candidaturas/Index     | Listagem de candidaturas          |
| /SuportesEmpresas/Index | Listagem de suportes empresariais |
| /Vagas/Index            | Listagem de vagas                 |

---

## 💻 Exemplos de uso em CURL para os endpoints

### 👨‍💼 Candidatos

- Listar todos os candidatos

```bash
curl -X GET https://localhost:7084/api/candidatos -H "Accept: application/json"
```

- Criar novo candidato
```bash
curl -X POST https://localhost:7084/api/candidatos \
-H "Content-Type: application/json" \
-d '{"IdUsuario":1,"TipoDeficiencia":"Fisica","Habilidades":"Java,C#","Experiencia":"3 anos","AcessibilidadeNecessaria":"Rampas"}'
```

- Buscar novo candidato por ID
```bash
curl -X GET https://localhost:7084/api/candidatos/1 -H "Accept: application/json"
```

- Atualizar candidato
```bash
curl -X PUT https://localhost:7084/api/candidatos/1 \
-H "Content-Type: application/json" \
-d '{"IdCandidato":1,"IdUsuario":1,"TipoDeficiencia":"Visual","Habilidades":"Java","Experiencia":"4 anos","AcessibilidadeNecessaria":"Leitor de tela"}'
```

- Deleter candidato
```bash
curl -X GET https://localhost:7084/api/candidatos/deficiencia/Fisica -H "Accept: application/json"
```

- Filtrar candidatos por deficiência
```bash
curl -X GET https://localhost:7084/api/candidatos/deficiencia/Fisica -H "Accept: application/json"
```

### 🪪 Candidaturas

Listar todas candidaturas
```bash
curl -X GET https://localhost:7084/api/candidaturas -H "Accept: application/json"
```

- Criar nova candidatura
```bash
curl -X POST https://localhost:7084/api/candidaturas \
-H "Content-Type: application/json" \
-d '{"IdCandidato":1,"IdVaga":2,"DataCandidatura":"2025-11-20","Status":"EmAnalise"}'
```

- Buscar candidatura por Id
```bash
curl -X GET https://localhost:7084/api/candidaturas/1 -H "Accept: application/json"
```

- Deletar candidatura
```bash
curl -X DELETE https://localhost:7084/api/candidaturas/1
```

- Atualizar status da candidatura
```bash
curl -X PUT https://localhost:7084/api/candidaturas/1/status \
-H "Content-Type: application/json" \
-d '{"Status":"Aprovado"}'
```

- Listar candidaturas de um candidato
```bash
curl -X GET https://localhost:7084/api/candidaturas/candidato/1 -H "Accept: application/json"
```

- Listar candidaturas para uma vaga
```bash
curl -X GET https://localhost:7084/api/candidaturas/vaga/2 -H "Accept: application/json"
```

### 🏬 Empresas

- Listar empresas
```bash
curl -X GET https://localhost:7084/api/empresas -H "Accept: application/json"
```

- Criar empresa
```bash
curl -X POST https://localhost:7084/api/empresas \
-H "Content-Type: application/json" \
-d '{"Nome":"Empresa XYZ","Setor":"Tecnologia","NivelAcessibilidade":"Alto"}'
```

- Buscar empresa por Id
```bash
curl -X GET https://localhost:7084/api/empresas/1 -H "Accept: application/json"
```

- Atualizar empresa
```bash
curl -X PUT https://localhost:7084/api/empresas/1 \
-H "Content-Type: application/json" \
-d '{"IdEmpresa":1,"Nome":"Empresa XYZ Atualizada","Setor":"Tecnologia","NivelAcessibilidade":"Médio"}'
```

- Deletar empresa
```bash
curl -X DELETE https://localhost:7084/api/empresas/1
```

- Filtrar empresas por setor
```bash
curl -X GET https://localhost:7084/api/empresas/setor/Tecnologia -H "Accept: application/json"
```

- Filtrar empresas por nível de acessibilidade
```bash
curl -X GET https://localhost:7084/api/empresas/acessibilidade/Alto -H "Accept: application/json"
```

### 🦽 SuportesEmpresa

- Listar todos os suportes
```bash
curl -X GET https://localhost:7084/api/suportesempresa -H "Accept: application/json"
```

- Criar novo suporte
```bash
curl -X POST https://localhost:7084/api/suportesempresa \
-H "Content-Type: application/json" \
-d '{"IdEmpresa":1,"TipoSuporte":"Técnico","Descricao":"Suporte para sistema"}'
```

- Buscar suporte por Id
```bash
curl -X GET https://localhost:7084/api/suportesempresa/1 -H "Accept: application/json"
```

- Deletar suporte
```bash
curl -X DELETE https://localhost:7084/api/suportesempresa/1
```

- Listar suportes por empresa
```bash
curl -X GET https://localhost:7084/api/suportesempresa/empresa/1 -H "Accept: application/json"
```

### 👥 Usuários

- Listar usuários
```bash
curl -X GET https://localhost:7084/api/usuarios -H "Accept: application/json"
```

- Criar usuário
```bash
curl -X POST https://localhost:7084/api/usuarios \
-H "Content-Type: application/json" \
-d '{"Nome":"João Silva","Email":"joao@exemplo.com"}'
```

- Buscar usuário por Id
```bash
curl -X GET https://localhost:7084/api/usuarios/1 -H "Accept: application/json"
```

- Atualizar usuário
```bash
curl -X PUT https://localhost:7084/api/usuarios/1 \
-H "Content-Type: application/json" \
-d '{"IdUsuario":1,"Nome":"João Silva Atualizado","Email":"joaoupdate@exemplo.com"}'
```

- Deletar usuário
```bash
curl -X DELETE https://localhost:7084/api/usuarios/1
```

### ♿ Vagas

- Listar vagas
```bash
curl -X GET https://localhost:7084/api/vagas -H "Accept: application/json"
```

- Criar vaga
```bash
curl -X POST https://localhost:7084/api/vagas \
-H "Content-Type: application/json" \
-d '{"Titulo":"Desenvolvedor Java","IdEmpresa":1,"Descricao":"Vaga para dev Java com experiência"}'
```

- Buscar vaga por Id
```bash
curl -X GET https://localhost:7084/api/vagas/1 -H "Accept: application/json"
```

- Atualizar vaga
```bash
curl -X PUT https://localhost:7084/api/vagas/1 \
-H "Content-Type: application/json" \
-d '{"IdVaga":1,"Titulo":"Desenvolvedor Java Sênior","IdEmpresa":1,"Descricao":"Atualização da descrição da vaga"}'
```

- Deletar vaga
```bash
curl -X DELETE https://localhost:7084/api/vagas/1
```

- Pesquisar vagas
```bash
curl -X GET https://localhost:7084/api/vagas/search?query=java -H "Accept: application/json"
```

- Listar vagas por empresa
```bash
curl -X GET https://localhost:7084/api/vagas/empresa/1 -H "Accept: application/json"
```

## 📹 Exemplo de CRUD completo

![crud_net-ezgif com-video-to-gif-converter](https://github.com/user-attachments/assets/2ba96d1e-c351-4c38-808d-7ecf99b94646)


## 📖 Documentação e fluxos visuais

### 🔍 Swagger

![Imagem](https://drive.google.com/uc?export=view&id=1JzfWPRjFWElm7V9pHELdyJgvgaGzCrFt)

A API possui **Swagger** configurado e disponível em:

`http://localhost:5212/index.html`

- O **Swagger UI** fornece documentação interativa dos endpoints, parâmetros, respostas, além de sample requests, podendo testar diretamente do navegador.

### 🎨 Aplicação MVC

![Imagem](https://drive.google.com/uc?export=view&id=1eV-gMw71Yq4swrEqY40zKCS5Y5xfbcr0)
![Imagem](https://drive.google.com/uc?export=view&id=1HJrLCrU5gIvgDCJ-UQxy4m7Ukk4Crwvo)

Ao rodar a aplicação, a interface web estará disponível em `https://localhost:7084`

A interface web possui design limpo com **Bootstrap**. Está disponível fluxo principal para:

- Cadastro de usuários, empresas, candidaturas, vagas e suportes.
- Páginas de listagem, criação, edição, exclusão e visualização detalhada.
- Validação inline e mensagens de erros claras.
