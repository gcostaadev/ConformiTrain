#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Est�gio 1: Build da Aplica��o
# Usamos a imagem do SDK para compilar o projeto.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o ficheiro .csproj para restaurar as depend�ncias primeiro (camada de cache)
# Assumindo que .csproj est� na raiz, junto com o Dockerfile
COPY ["ConformiTrain.csproj", "."]
RUN dotnet restore "./ConformiTrain.csproj"

# Copia todo o resto do c�digo fonte
COPY . .
WORKDIR "/src/."
RUN dotnet build "./ConformiTrain.csproj" -c Release -o /app/build

# Est�gio 2: Publica��o
# Gera os ficheiros finais otimizados para produ��o
FROM build AS publish
RUN dotnet publish "./ConformiTrain.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Est�gio 3: Imagem Final de Produ��o
# Usamos a imagem do ASP.NET runtime, que � muito mais leve
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Exp�e a porta que a aplica��o vai usar dentro do contentor
EXPOSE 8080

# Comando para iniciar a aplica��o quando o contentor for executado
ENTRYPOINT ["dotnet", "ConformiTrain.dll"]

