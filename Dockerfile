#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Estágio 1: Build da Aplicação
# Usamos a imagem do SDK para compilar o projeto.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o ficheiro .csproj para restaurar as dependências primeiro (camada de cache)
# Assumindo que .csproj está na raiz, junto com o Dockerfile
COPY ["ConformiTrain.csproj", "."]
RUN dotnet restore "./ConformiTrain.csproj"

# Copia todo o resto do código fonte
COPY . .
WORKDIR "/src/."
RUN dotnet build "./ConformiTrain.csproj" -c Release -o /app/build

# Estágio 2: Publicação
# Gera os ficheiros finais otimizados para produção
FROM build AS publish
RUN dotnet publish "./ConformiTrain.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio 3: Imagem Final de Produção
# Usamos a imagem do ASP.NET runtime, que é muito mais leve
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Expõe a porta que a aplicação vai usar dentro do contentor
EXPOSE 8080

# Comando para iniciar a aplicação quando o contentor for executado
ENTRYPOINT ["dotnet", "ConformiTrain.dll"]

