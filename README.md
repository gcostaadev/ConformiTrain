<p align="center">
  <a href="https://learn.microsoft.com/en-us/dotnet/csharp/">
    <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="C#" width="60" style="margin-right: 10px;" />
  </a>
  <a href="https://www.docker.com/">
    <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/docker/docker-original-wordmark.svg" alt="Docker" width="80" style="margin-right: 10px;" />
  </a>
  <a href="https://github.com/features/actions">
    <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/githubactions/githubactions-original.svg" alt="GitHub Actions" width="60" />
  </a>
</p>

<h1 align="center">Projeto DevOps - ConformiTrain (ESG)</h1>

<p align="center">
  <img src="https://img.shields.io/badge/.NET-8.0-blueviolet?style=for-the-badge&logo=dotnet" alt=".NET 8"/>
  <img src="https://img.shields.io/badge/Docker-Ready-blue?style=for-the-badge&logo=docker" alt="Docker"/>
  <img src="https://img.shields.io/badge/GitHub%20Actions-CI%2FCD-success?style=for-the-badge&logo=githubactions" alt="CI/CD"/>
  <img src="https://img.shields.io/badge/Oracle-Database-red?style=for-the-badge&logo=oracle" alt="Oracle"/>
  <img src="https://img.shields.io/badge/Status-Build%20Passing-brightgreen?style=for-the-badge" alt="Build Passing"/>
</p>

🚀 Este projeto implementa um ciclo completo de DevOps para a aplicação ConformiTrain

Uma API em C# .NET com foco em ESG (Environmental, Social & Governance).
O objetivo é aplicar práticas de containerização, orquestração e automação de CI/CD, garantindo integração e entrega contínua com qualidade e segurança.

## 🧩 Arquitetura Geral
A aplicação é composta por:
* 🧠 **Backend:** API REST em .NET 8
* 🧮 **Banco de Dados:** Oracle
* 🐳 **Containerização:** Docker e Docker Compose
* ⚙️ **Pipeline CI/CD:** GitHub Actions

## 🐳 Como Executar Localmente com Docker

### ✅ Pré-requisitos
* Docker instalado
* Docker Compose instalado

### 1️⃣ Clone o repositório

### 2️⃣ Crie o arquivo de ambiente .env

Crie um arquivo chamado .env na raiz do projeto com o seguinte conteúdo:

DB_PASSWORD=SenhaForteParaOracle123!


### 3️⃣ Suba os containers

Execute o comando abaixo para construir e iniciar a aplicação e o banco Oracle:

* docker-compose up --build -d

### 4️⃣ Acesse a aplicação
Após a inicialização:
* 🔗 **Swagger UI:** `http://localhost:8000/swagger`

---

## ⚙️ Pipeline CI/CD — GitHub Actions
O pipeline é definido em `.github/workflows/pipeline-ci.yml` e é executado a cada push na branch `main`.
Ele contém 4 jobs principais, que garantem um fluxo completo de Integração e Entrega Contínua:

| Etapa | Descrição | Objetivo |
| :--- | :--- | :--- |
| 🧪 **test** | Compila e executa testes unitários (`dotnet test`) | Garante a qualidade do código |
| 🏗️ **build-and-push** | Constrói a imagem Docker e envia para o Docker Hub | Gera o artefato da aplicação |
| 🚀 **deploy-staging** (simulado) | Simula deploy em ambiente de homologação | Validação antes da produção |
| 🔒 **deploy-production** (simulado) | Simula deploy em produção (com aprovação manual) | Segurança e controle do release |

## 📦 Containerização — Dockerfile
A imagem da aplicação é criada usando multi-stage build, otimizando o tamanho final e garantindo segurança.

```dockerfile
# Etapa 1: Build
FROM [mcr.microsoft.com/dotnet/sdk:8.0](https://mcr.microsoft.com/dotnet/sdk:8.0) AS build
WORKDIR /src
COPY ["ConformiTrain.csproj", "."]
RUN dotnet restore "./ConformiTrain.csproj"
COPY . .
RUN dotnet build "ConformiTrain.csproj" -c Release -o /app/build

# Etapa 2: Publish
FROM build AS publish
RUN dotnet publish "ConformiTrain.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 3: Final (Runtime)
FROM [mcr.microsoft.com/dotnet/aspnet:8.0](https://mcr.microsoft.com/dotnet/aspnet:8.0) AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ConformiTrain.dll"]

```
## 🖼️ Evidências do Funcionamento


### 🐋 Execução local com Docker Compose 

<img width="1560" height="80" alt="image" src="https://github.com/user-attachments/assets/6593f5e6-f152-4f3a-834e-07e0630f3432" />

---

### 📘 Swagger UI acessível localmente 

<img width="1575" height="948" alt="image" src="https://github.com/user-attachments/assets/2a82ae28-d3a0-4cf1-b2c8-593d9fac2e17" />

---

### 🧩 Pipeline no GitHub Actions concluído

<img width="1883" height="444" alt="image" src="https://github.com/user-attachments/assets/2da4566e-8bb9-4b0e-aba3-e19bcde668d6" />

---

### 🐙 Imagem publicada no Docker Hub 

<img width="935" height="468" alt="image" src="https://github.com/user-attachments/assets/0fa007f7-cedb-4b10-80a5-80b60d9dad44" />

---

## 🧰 Tecnologias Utilizadas

| Categoria | Tecnologias |
| :--- | :--- |
| **Backend** | C# • .NET 8 |
| **Banco de Dados** | Oracle |
| **Containerização** | Docker |
| **Orquestração Local**| Docker Compose |
| **CI/CD** | GitHub Actions |

## 📋 Checklist de Entrega

| Item | Status |
| :--- | :--- |
| 📦 Projeto compactado (.ZIP) com estrutura organizada | ☑️ |
| 🐋 Dockerfile funcional | ☑️ |
| ⚙️ docker-compose.yml configurado | ☑️ |
| 🔁 Pipeline com build, teste e deploy | ☑️ |
| 🧾 README.md completo com prints | ☑️ |
| 📚 Documentação técnica (PDF/PPT) | ☑️ |
| 🚀 Deploy realizado em staging e produção | ☑️ |







