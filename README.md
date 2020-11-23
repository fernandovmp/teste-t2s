# CRUD de containers

## Tecnologias usadas

-   Backend
    -   [ASP NET Core](https://docs.microsoft.com/en-us/aspnet/core/): Framwork cross-platform para construir aplicações web
    -   [Sql Server](https://www.microsoft.com/pt-br/sql-server): Banco de dados relacional da Microsoft
    -   [Dapper](https://dapper-tutorial.net/dapper): Micro ORM
    -   [FluentValidation](https://fluentvalidation.net/): Biblioteca para construir validações
    -   [Entity Framework](https://docs.microsoft.com/pt-br/ef/): ORM com suporte a consultas LINQ
    -   [AutoMapper](https://automapper.readthedocs.io/en/latest/index.html): Biblioteca de mapeamento de propriedades de objetos
    -   [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore): Ferramenta para documentação Swagger
-   Frontend
    -   [Angular](https://angular.io/): Framework para construção de SPA's
    -   [NG-ZORRO Ant Design](https://ng.ant.design/): Componentes que seguem as guidelines do [Ant Design](https://ant.design/docs/spec/introduce)

## Como rodar o projeto

### Usando docker

Requisitos

-   [Docker](https://docs.docker.com/get-docker/)
-   [Docker Compose](https://docs.docker.com/compose/install/)

Ao executar o seguinte comando, o banco de dados, o servidor e a aplicação web ficaram disponiveis

```
docker-compose up -d
```

A API ficará disponivel em `http://localhost:5000`
A aplicação Angular ficará disponivel em `http://localhost:4200`
> *A aplicação angular fará requisições sempre ao endereço `http://localhost:5000`


Para interromper a execução, execute:

```
docker-compose down
```

### Sem docker

Requires

-   [.NET 5.0 SDK](https://dotnet.microsoft.com/download)
-   [NodeJS](https://nodejs.org/en/)
-   [Angular](https://angular.io/guide/setup-local)
-   [Sql Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

**Back-end**

Restaure as dependencias

```
dotnet restore
```

Gere o __build__ da solução
```
dotnet build
```

Preencha as seguintes propriedades no arquivo [appsettings.json](./src/TesteT2S.WebApi/appsettings.json)

```json
  "ConnectionStrings": {
    "SqlServerConnection": "{coloque a string de conexão aqui}"
  },
  "CorsOptions": {
    "PolicyName": "AppCors",
    "AllowedOrigin": "http://localhost:4200"
  },
```

E então execute o projeto com o seguinte comando

```
dotnet run --project src/KanbanBoard.WebApi
```

**Front-end**

Instale as dependencias do projeto

```bash
$ cd src/TesteT2S.Frontend
$ npm install
```

Em seguida, basta executar
```
ng serve --open
```

Dessa forma a aplicação será aberta no navegador
