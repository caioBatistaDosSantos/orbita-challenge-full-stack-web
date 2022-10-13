# Boas vindas ao projeto Orbita Challenge Full Stack Web
API e Aplicação que realizam um CRUD, desenvolvidos com `.NET C#` e `Vue.js`.
API coberta por testes integrados desenvolvidos com `xUnit` e `FluentAssertions`.

---

## O que foi desenvolvido:

  - Esta aplicação surgiu para suprir a nescessidade de cadastrar uma pessoa estudante em um banco de dados e realizar um cadastro da mesma, desafios propostos pelo README do projeto.

---

## Sobre a aplicação (arquitetura e tecnologias):

Este repositório contêm um banco de dados `MySQL`, o `backend` e `frontend` da aplicação desenvolvidos com as seguintes tecnologias:

### Back-end:

Localizado em `/orbita-challenge-full-stack-web/back-end/back-end.Web`, o back-end segue o conceito de <strong>API RESTfull</strong>. Desenvolvido com `C# .NET`, com a arquitetura de uma API Web.
  - Dentro da pasta `/Repository` foram desenvolvidos os arquivos de contexto e repositório que configuram e acessam o banco de dados.
  - Na pasta `/Controllers` contém a lógica da requisição da API e regras de negócio
  - Na pasta `/Models` está a minha classe StudentsModel que mapeia a minha entidade *students*

Os <strong>frameworks</strong> utilizados em conjunto com o .NET foram:
  - `ASP.NET CORE`: para facilitar na criação e densenvolvimento da API
  - `EntityFramework`: como ORM, o Entity Framework auxiliou na criação das *migrations* e em todo gerenciamento e relacionamento com o banco de dados

<strong>TESTES INTEGRADOS</strong>:

Para os testes foi criado um segundo projeto, como é o padrão do .NET e está localizado em `/orbita-challenge-full-stack-web/back-end/back-end.Test`.
As tecnologias utilizadas para o desenvolvimento foram:
   - `xUnit`: para criar e desenvolver o projeto de teste
   - `FluentAssertions`: para dar mais legibilidade ao teste
   - `EntityFrameworkCore InMemory`: para realizar o *"mock"* do banco de dados MySQL, armazenando os dados na memória de sua máquina
   - `Swagger`: para documentar a API

### Front-end:

O Front-end é uma aplicação `Vue` e foi essencialmente desenvolvido com o `Vuetify`. Como foi meu primeiro projeto utilizando essas ferramentas, decidi desenvolver reutilizando os componentes de *"forms"* da documentação.

Os <strong>frameworks</strong> utilizados foram:
 - `Vuelidate`: para validar os campos dos *inputs*
 - `Axios`: para solicitar as requisições ao back-end

### Outras tecnologias:

Utilizei o `Docker` para versionar o projeto e o `docker-compose` para conteinizar a aplicação.

---

# Como rodar a Aplicação...

<i>(Obs: esse projeto foi configurado para rodar nas portas: `8080`, `3000`, `3001` e `3306` da sua máquina, então é nescessário que estas portas estejam livres!!!)</i>

## Com o Docker:
  - Para rodar o projeto com o `Docker` você vai precisar ter instalado o GitHub, Docker e docker-compose e basta seguir o passo a passo abaixo:

1. Clone o repositório com o comando:
  - `git@github.com:caioBatistaDosSantos/orbita-challenge-full-stack-web.git`;
    - Entre na pasta do repositório:
      - `cd orbita-challenge-full-stack-web`
2. Suba o container com o comando:
  - `docker-compose up -d --build`

- Apos este passo a aplicação já vai estar rodando dentro do container e você pode utilizá-la nas seguintes URLs:
  - http://localhost:8080 => Aplicação Vue
  - http://localhost:3001/Students => API

---

## Locamente:
  - Para rodar o projeto localmente você vai precisar ter instalado o GitHub, .NET, Node, ter um banco de dados MySQL e basta seguir o passo a passo abaixo:

1. Clone o repositório com o comando:
  - `git@github.com:caioBatistaDosSantos/orbita-challenge-full-stack-web.git`;
    - Entre na pasta do repositório:
      - `cd orbita-challenge-full-stack-web`
2. Entre na pasta do back-end:
  - `cd back-end/back-end.Web`
3. Altere a string de conecção que se encontra no arquivo `Program.cs` e `/Repository/StudentsRepository.cs` com os dados do seu banco de dados local.
4. Instale as dependências:
  - `dotnet restore`
5. Inicie a aplicação back-end:
  - `dotnet run`
6. Entre na pasta front-end:
  - `cd front-end`
7. Instale as dependências:
  - `npm install`
8. Inicie a aplicação fron-end:
  - `npm run serve`

- Apos este passo a aplicação já vai estar rodando localmente e você pode utilizá-la nas seguintes URLs:
  - http://localhost:8080 => Aplicação Vue
  - http://localhost:3001/Students => API
  - https://localhost:3000/swagger => Documentação com o swagger

---

## Para testar...
  - Para realizar os testes clone o repositório seguindo o conteúdo acima, entre na pasta `back-end/back-end.Test` e execute o comando `dotnet test`.

---

## Possíveis melhorias:

Se tivesse mais tempo, gostaria de ralizar as seguistes melhorias:
  - <strong>Refatorar o código do Back-end para respeitar os princípios do SOLID</strong>: facilitando a escalabilidade do projeto e manutenção.
  - <strong>Utilizar o Vuex</strong>: cheguei a instalar e configurar essa biblioteca para gerenciar o estado da aplicação front-end, entretanto ainda não consegui utiliza-la nos componentes. Gostaria de usar essa ferramenta, pois assim poderia componentizar o meu arquivo `StudentsView.vue` e diminuir a complexidade do meu código.
  - <strong>Realizar os teste do front-end</strong>: os testes do front-end estão em produção, mas por ser a primeira vez utilizando o Vue estou com algumas dificuldades para configurar o ambiente de testes e renderizar os componentes.

# Feedback são bem-vindos!!
