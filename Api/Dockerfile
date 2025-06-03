# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

# Instala o Git na imagem (se não estiver presente na imagem base)
RUN apt-get update && \
    apt-get install -y git && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

USER $APP_UID

WORKDIR /app

# Declare um argumento de build para o token
ARG GITLAB_PAT

# Configura o Git para usar o token temporariamente para o domínio do GitLab
# Note: O GitLab prefere 'oauth2' como username para PATs.
RUN git config --global url."https://oauth2:${GITLAB_PAT}@gitlab.com/".insteadOf "https://gitlab.com/"

# Faz o clone do repositório da infraestrutura
RUN git clone https://gitlab.com/iwebwork/csharpinfraestrutura.git csharpinfraestrutura

# Faz o clone do repositório do projeto
RUN git clone https://github.com/iwebwork/ApiBlogEmadaio.git ApiBlogEmadaio

# Limpa a configuração do Git para não deixar o token na imagem final
# Embora ele já esteja "baked" em uma camada anterior, esta é uma boa prática.
RUN git config --global --unset url."https://oauth2:${GITLAB_PAT}@gitlab.com/".insteadOf || true

EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src

# Copia os arquivos de projeto (.csproj) da API e da biblioteca.
# A estrutura de pastas relativa ao Dockerfile precisa ser mantida dentro do contêiner.
COPY --from=base /app/csharpinfraestrutura ./csharpinfraestrutura
COPY --from=base /app/ApiBlogEmadaio ./ApiBlogEmadaio


# Restaura todos os pacotes NuGet e resolve referências de projeto para a solução principal.
# Isso irá restaurar pacotes para AMBOS os projetos e garantir que a infraestrutura seja construída para a API.
RUN dotnet restore "ApiBlogEmadaio/Api/Api.csproj"

# Altera o WORKDIR para o diretório do projeto API principal.
WORKDIR /src/ApiBlogEmadaio/Api

# Copia todo o restante do código-fonte para o WORKDIR /src.
# Isso deve ser feito após o restore para otimizar o cache.
COPY . .

# Constrói o projeto API principal.
# O projeto da biblioteca será construído automaticamente como uma dependência.
RUN dotnet build "Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# O WORKDIR permanece /src/ApiBlogEmadaio/Api do estágio 'build'.
RUN dotnet publish "./Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]