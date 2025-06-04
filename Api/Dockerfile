#Estágio 1 - 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS git_clone

# Instala o Git na imagem (se não estiver presente na imagem base)
RUN apt-get update && \
    apt-get install -y git && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

WORKDIR /app

# Define a variável de ambiente para que a aplicação escute APENAS em HTTP.
# Isso sobrescreve qualquer configuração de HTTPS de launchSettings.json no contexto do Docker.
ENV ASPNETCORE_URLS="http://+:8080"

#EXPOSE 8080
#EXPOSE 8081

# Declare um argumento de build para o token
ARG GITLAB_PAT

# Configura o Git para usar o token temporariamente para o domínio do GitLab
# Note: O GitLab prefere 'oauth2' como username para PATs.
RUN git config --global url."https://oauth2:${GITLAB_PAT}@gitlab.com/".insteadOf "https://gitlab.com/"

# Faz o clone do repositório da infraestrutura
RUN git clone --branch master https://gitlab.com/iwebwork/csharpinfraestrutura.git

# Faz o clone do repositório do projeto
#RUN git clone --branch master https://github.com/iwebwork/ApiBlogEmadaio.git

# Limpa a configuração do Git para não deixar o token na imagem final
# Embora ele já esteja "baked" em uma camada anterior, esta é uma boa prática.
RUN git config --global --unset url."https://oauth2:${GITLAB_PAT}@gitlab.com/".insteadOf || true

# --- DEBUGA: LISTA PASTAS ---
RUN echo "--- Conteúdo de /app após clones (estágio git_clone): ---" && \
    ls -l /app && \
    echo "---------------------------------------------------------"

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Estágio 2 - 
# This stage is used to build the service project
ARG BUILD_CONFIGURATION=Release

# Define o WORKDIR para a raiz onde os repositórios clonados serão copiados.
# Isso torna os caminhos consistentes.
WORKDIR /src

# Copia a estrutura de pastas do projeto de infraestrutura
COPY --from=git_clone app/csharpinfraestrutura ./csharpinfraestrutura

# Copia a estrutura de pastas do projeto principal
COPY --from=git_clone app/ApiBlogEmadaio ./ApiBlogEmadaio

# --- DEBUGA: LISTA PASTAS
RUN echo "--- Conteúdo de /src após copiar do app: ---" && \
    ls -l /src && \
    echo "---------------------------------------------------------"

#Estágio 3 - 
WORKDIR /src/ApiBlogEmadaio/Api

# Restaura todos os pacotes NuGet e resolve referências de projeto para a solução principal.
# Isso irá restaurar pacotes para AMBOS os projetos e garantir que a infraestrutura seja construída para a API.
RUN dotnet restore "Api.csproj"

COPY . .

# Constrói o projeto API principal.
# O projeto da biblioteca será construído automaticamente como uma dependência.
RUN dotnet build "Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release

# O WORKDIR permanece /src/ApiBlogEmadaio/Api do estágio 'build'.
RUN dotnet publish "Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]