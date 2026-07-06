# Controle de Gastos Residenciais

## Objetivo do projeto

Criar um sistema que cadastra pessoas e transações vinculadas a uma pessoa e, com base nisso, calcular o total de receita, despesas e o saldo líquido.
O gerenciamento dessas informações é realizado através de uma API REST e de uma interface web desenvolvida com React.

## Tecnologias utilizadas

### Parte do Backend

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- Swagger/OpenAPI

### Parte do Frontend

- React
- TypeScript
- Vite
- Axios
- CSS

## Arquitetura

O backend foi organizado em camadas da seguinte forma:

- **Controllers:** responsáveis por determinar como será feita a chamada da API e como serão retornadas as respostas.
- **Services:** responsáveis por estabelecer as regras de negócio.
- **Repositories:** realizam o acesso ao banco de dados.
- **Banco de dados:** acesso realizado por meio do Entity Framework Core.

## Funcionalidades

### Pessoas

- Cadastro de pessoas
- Listagem de pessoas
- Exclusão de pessoas
- Validação de idade
- Impedimento de cadastro de nomes duplicados

**OBS.: a razão para não permitir o cadastro de pessoas com o mesmo nome é evitar confusão durante a seleção de uma pessoa na criação de uma transação.**

### Transações

- Cadastro de receitas
- Cadastro de despesas
- Listagem de transações
- Exclusão de transações

### Regras de negócio

- Pessoas menorrd de 18 anos podem ter apenas transações do tipo despesa vinculadas a elas.
- Não é permitido cadastrar duas pessoas com o mesmo nome.
- Exclusão automática das transações quando uma pessoa é removida.

# Passo a passo de como executar

## Backend

Primeiro, entre na pasta da API:

```bash
cd ResidentialExpenseControl.Api
```

Instale as dependências:

```bash
dotnet restore
```

Caso seja necessário criar o banco:

```bash
dotnet ef database update
```

Execute a aplicação:

```bash
dotnet run
```

A API ficará disponível em:

```
http://localhost:5299
```

Swagger:

```
http://localhost:5299/swagger
```

## Frontend

Entre na pasta do projeto React:

```bash
cd ResidentialExpenseControl.Front
```

Instale as dependências:

```bash
npm install
```

Execute a aplicação:

```bash
npm run dev
```

O frontend ficará disponível em:

```
http://localhost:5173
```

## Como é feita a comunicação entre Frontend e Backend?

A comunicação é realizada utilizando Axios.

A base URL da API é esta aqui:

```
http://localhost:5299/api
```

## Documentação da API

A documentação completa da API está disponível através do Swagger.

Além disso, os endpoints possuem comentários XML para facilitar a consulta da documentação.

## Recurso adicional

Decidi criar um botão de exclusão para cada transação criada. Isso facilita o gerenciamento das transações e permite remover rapidamente registros criados por engano ou contendo erros de digitação.