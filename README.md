                                Teste Técnico UEX Asp Net PL

			   


Este projeto é um teste técnico desenvolvido para a empresa UEX, utilizando as tecnologias TypeScript, NextJS, C# com .NET e PostgreSQL. O projeto consiste em um aplicativo web que gerencia uma lista de contatos e oferece a funcionalidade de visualizar a localização dos contatos no mapa utilizando a API do Google Maps.
Gostaria de agradecer à UEX pela oportunidade de realizar este projeto.
Requisitos
Back-End em ASP.NET.
Banco de Dados Relacional (PostgreSQL).
Funcionalidades de Cadastro e Login.
Persistência de Dados.
Exclusão de conta própria.
Gerenciamento da lista de contatos.
Pesquisa de endereços para facilitar o cadastro de contatos.
Pins no mapa representando a localização dos contatos.
Assistência de preenchimento para facilitar o cadastro.
Todos os requisitos foram atendidos, mas devido ao curto prazo de dois dias, algumas funcionalidades ficaram de fora, como:
Edição e exclusão de dados de contatos.
Testes técnicos profundos.
Refatoração de código mais detalhada.
Pontos a corrigir:
O mapa às vezes fica em fica em loading infinito sendo necessário um refresh na pagina, necessário pesquisar mais afundo para entender esse comportamento.
 Melhora na mensagem de tratamento de erro na página de login/registro
Repaginada no visual simplista.
PIN vermelho no mapa ocasionalmente.
Links do Projeto
Front-End GitHub: Link do Front-End
Back-End GitHub: Link do Back-End
Tecnologias Utilizadas
Front-End
TypeScript
React
NextJS
Material Design V3
MUI
Google Maps API
Back-End
C#
.NET
BCrypt
JWT (JSON Web Token)
Moq
Npgsql
Swashbuckle
xUnit
Tutorial de Instalação - Front-End

Instale as dependências:
bash
Copy code
npm install

Inicie o servidor:
bash
Copy code
npm start
Acesse o projeto através do seguinte link:
URL padrão: http://localhost:3000/
Variáveis de Ambiente (ENVs):
NEXT_PUBLIC_GOOGLE_MAPS_API_KEY: sua chave da API do Google Maps.
NEXT_PUBLIC_API_URL: URL da API do back-end.
Exemplo de configuração:
bash
Copy code
NEXT_PUBLIC_GOOGLE_MAPS_API_KEY=SuaChaveAPI
NEXT_PUBLIC_API_URL=http://localhost:5160
Tutorial de Instalação - Back-End
Certifique-se de ter o PostgreSQL instalado na sua máquina.

Instale o pacote Npgsql para .NET:
bash
dotnet add package Npgsql

Instale o Entity Framework Core:
bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
Configure a string de conexão no appsettings.json:
json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Username=postgres;Password=suaSenha;Database=nomeDoBanco"
  }
}
Crie a migration:
bash
dotnet ef migrations add InitialCreate
Aplique a migration para criar as tabelas no banco de dados:
bash
dotnet ef database update
Inicie o projeto:
bash
dotnet run
A API estará disponível em:
URL padrão: http://localhost:5160
Caso necessário, acesse o Swagger utilizando a URL fornecida.

                                      Jornada do Usuário



Após a tela de boas-vindas, o usuário será direcionado para a página de cadastro.
Requisitos para o e-mail: O e-mail deve estar em formato válido (ex: exemplo@dominio.com).
Requisitos para a senha: A senha deve ter pelo menos 8 caracteres, incluindo:
1 letra maiúscula.
1 letra minúscula.
1 número.
1 caractere especial (ex: @, #, $, %).
Exemplo de senha utilizada para testes: 1245678Aa!
Rota para o registro de usuário:
URL: https://localhost:5160/api/user/register
Objeto de exemplo:
json
{ 
  "fullName": "João Silva",
  "email": "joao.silva@example.com",
  "password": "Senha@1234"
}


Após o cadastro, o usuário será redirecionado para a página de login, onde poderá inserir seu e-mail e senha.
Rota para login:
URL: https://localhost:5160/api/user/login
Objeto de exemplo:
json
{
  "email": "teste@gmail.com",
  "password": "1245678Aa!"
}


Após o login bem-sucedido, o usuário será redirecionado para a área principal do aplicativo, onde poderá visualizar no mapa os contatos cadastrados. A área é protegida e só pode ser acessada por usuários autenticados.
Rota para adicionar contatos:
URL: https://localhost:5160/api/contact
Formato do objeto de contato:


  "Name": "João Silva",               // Nome completo
  "Cpf": "987.654.321-00",             // CPF fictício (formato: XXX.XXX.XXX-XX)
  "Phone": "+55 11 99876-5432",        // Telefone fictício (formato internacional)
  "Street": "Avenida das Palmeiras",   // Rua fictícia
  "Number": "123",                     // Número da residência fictício
  "Neighborhood": "Jardim das Flores", // Bairro fictício
  "City": "São Paulo",                 // Cidade fictícia
  "State": "SP",                       // Estado fictício
  "Latitude": "-23.550520",            // Latitude fictícia
  "Longitude": "-46.633308"            // Longitude fictícia

}

O usuário também pode excluir sua conta ou excluir contatos específicos.
Rota para excluir a conta:
URL: http://localhost:5160/api/user/delete-account
Rota para excluir um contato:
URL: https://localhost:5160/api/contact/ID (onde ID é o identificador do contrato a ser excluído).

