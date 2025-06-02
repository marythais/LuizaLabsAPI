# API de Produtos - LuizaLabs

Este projeto é uma API .NET com armazenamento usando **LiteDB** (banco de dados NoSQL embutido).

## Tecnologias
- .NET 7
- ASP.NET Core
- LiteDB
- Swagger 

## Como rodar o projeto

1. Clone o repositório:
   - git clone https://github.com/marythais/LuizaLabsAPI

2. Baixe o LiteDB:
   - Acesse o GitHub e baixe o arquivo executável do LiteDB (https://github.com/litedb-org/LiteDB.Studio/releases)

3. Instale o .net:
   - Acesse o site oficial do .NET (https://dotnet.microsoft.com/download) e baixe a versão mais recente do .NET 7 SDK.

4. Instalar via terminal o LiteDB:
   - abrir o terminar e colar: dotnet add package LiteDB

5. Rodar o projeto como IIS Express

6. Abrir o LiteDB Studio e conecte-se ao arquivo `Produtos.db` localizado na pasta do projeto.


A API Produto/popular tem o intuito de preencher o arquivo produtos.db com produtos aleatórios para que possa realizar as chamadas com uma massa previamente criada.
