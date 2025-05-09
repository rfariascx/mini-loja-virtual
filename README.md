# AppLojaBackoffice
Aplicação de um Backoffice de um Marketplace com MVC e API RESTful.

# 1. Apresentação
Bem-vindo ao repositório do projeto [AppLojaBackoffice]. Este projeto é uma entrega do MBA DevXpert Full Stack .NET e é referente ao módulo Introdução ao Desenvolvimento ASP.NET Core. O objetivo principal desenvolver uma aplicação de blog que permite aos usuários criar, editar, visualizar e excluir posts e comentários, tanto através de uma interface web utilizando MVC quanto através de uma API RESTful. Descreva livremente mais detalhes do seu projeto aqui.

# Autor
Rafhael Farias


# 2. Proposta do Projeto
O projeto consiste em:
* Aplicação MVC: Interface web para interação com o backoffice de um Marketplace.
* API RESTful: Exposição dos produtos e categorias cadastradas, permitindo manutenção por usuários autenticados e autorizados.
* Autenticação e Autorização: Implementação de controle de acesso, diferenciando usuários anonimos e vendedores logados.
* Acesso a Dados: Implementação de acesso ao banco de dados através de ORM.

# 3. Tecnologias Utilizadas
* Linguagem de Programação: C#
 * Frameworks:
 * ASP.NET Core MVC
 * ASP.NET Core Web API
 * Entity Framework Core
* Banco de Dados: SQL Server
 * Autenticação e Autorização:
 * ASP.NET Core Identity
 * JWT (JSON Web Token) para autenticação na API
 * Front-end:
 * Razor Pages/Views
 * HTML/CSS para estilização básica
* Documentação da API: Swagger

# 4. Estrutura do Projeto
A estrutura do projeto é organizada da seguinte forma:

* src/
* AppLojaBackofficeMvc/ - Projeto MVC - Em construção
* AppLojaBackofficeApi/ - API RESTful - Em construção
* Data/ - Modelos de Dados e Configuração do EF Core
* README.md - Arquivo de Documentação do Projeto
* FEEDBACK.md - Arquivo para Consolidação dos Feedbacks
* .gitignore - Arquivo de Ignoração do Git

# 5. Funcionalidades Implementadas
* CRUD para Produtos e Categorias: Permite criar, editar, visualizar e excluir.
* Autenticação e Autorização: Diferenciação entre usuários anonimos e vendedores logados.
* API RESTful: Exposição de endpoints para operações CRUD via API.
* Documentação da API: Documentação automática dos endpoints da API utilizando Swagger.
* Não foi implementado CRUD para Vendedores. A criação do registro de vendedor é realizar a partir da criação do usuário no Identity.
* Camada Core Reutilizável: As regras de negócio foram concentradas em interfaces para reutilização tanto no projeto MVC quanto na WebAPI.
* Upload de Imagens de Produtos: Funcionalidade implementada nos projetos, com uma interface dedicada para centralizar as regras de negócio relacionadas.


# 6. Como Executar o Projeto
* Pré-requisitos
* .NET SDK 8.0 ou superior
* SQL Server
* Visual Studio 2022 ou superior (ou qualquer IDE de sua preferência)
* Git
* Passos para Execução
* Clone o Repositório: git clone https://github.com/rfariascx/mini-loja-virtual.git
* Configuração do Banco de Dados:
* No arquivo appsettings.json, configure a string de conexão do SQL Server.
* Rode o projeto para que a configuração do Seed crie o banco e popule com os dados básicos - em construção
* Executar a Aplicação MVC:
* cd src/AppLojaBackofficeMvc/
* dotnet run
* Acesse a aplicação em: http://localhost:5136
* Executar a API:
* cd src/AppLojaBackofficeApi/
* dotnet run
* Acesse a aplicação em: http://localhost:5031/swagger/
* Orientações do Seed:  
* Ao rodar o projeto pela primeira vez, em ambiente de desenvolvimento, os dados iniciais (seed) são criados automaticamente. *Isso inclui:
- 3 categorias de produto
- 1 usuário com login:
  - **Email:** vendedordeteste@teste.com
  - **Senha:** Senha@2025
- 1 vendedor vinculado ao usuário
- 3 produtos de exemplo com imagens


# 7. Instruções de Configuração
* JWT para API: As chaves de configuração do JWT estão no appsettings*.json.
* Migrações do Banco de Dados: As migrações são gerenciadas pelo Entity Framework Core. Não é necessário aplicar devido a configuração do Seed de dados.

# 8. Documentação da API
* A documentação da API está disponível através do Swagger. Após iniciar a API, acesse a documentação em:
* http://localhost:5031/swagger/

# 9. Avaliação
* Este projeto é parte de um curso acadêmico e não aceita contribuições externas.
* Para feedbacks ou dúvidas utilize o recurso de Issues
* O arquivo FEEDBACK.md é um resumo das avaliações do instrutor e deverá ser modificado apenas por ele.
