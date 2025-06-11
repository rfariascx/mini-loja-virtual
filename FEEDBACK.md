# Feedback - Avaliação Geral

## Front End

### Navegação
  * Pontos positivos:
    - Projeto MVC bem estruturado com navegação funcional e views para produtos, categorias e autenticação.

  * Pontos negativos:
    - Nenhum.

### Design
  - Interface clara, objetiva e bem estruturada para fins administrativos.

### Funcionalidade
  * Pontos positivos:
    - CRUD de produtos e categorias implementado na API e MVC.
    - Identity implementado corretamente em ambas as camadas com JWT e Cookies.
    - Seed de dados com SQLite e migrations automáticas funcionando.
    - A criação do vendedor junto ao usuário do Identity está corretamente implementada.

  * Pontos negativos:
    - O projeto API depende do projeto MVC, o que compromete a independência de camadas.
    - Código de negócio e lógica de acesso ao banco estão duplicados nas duas camadas, sem uma camada `Core` para centralização.
  
## Back End

### Arquitetura
  * Pontos positivos:
    - Projetos organizados com separação entre API e MVC.
    - Uso de boas práticas como DI, autenticação e configuração modular.

  * Pontos negativos:
    - Violação da separação de responsabilidades: a API depende diretamente do MVC.
    - Ausência de camada `Core` resultando em duplicação de lógica.

### Funcionalidade
  * Pontos positivos:
    - Funcionalidades principais entregues conforme o escopo: autenticação, CRUD e relacionamento vendedor-usuário.

  * Pontos negativos:
    - Duplicação de código e dependência entre camadas afetam a escalabilidade e manutenção do sistema.

### Modelagem
  * Pontos positivos:
    - Entidades bem estruturadas e organizadas.
    - Modelagem aderente aos requisitos do domínio.

  * Pontos negativos:
    - Nenhum.

## Projeto

### Organização
  * Pontos positivos:
    - Estrutura em `src`, `.sln` na raiz e documentação presente.
    - Código limpo e comentado.

  * Pontos negativos:
    - Forte acoplamento entre projetos, faltando modularização por meio de uma camada compartilhada.

### Documentação
  * Pontos positivos:
    - `README.md` e `FEEDBACK.md` estão presentes e com informações úteis.
    - Swagger ativo na API.

  * Pontos negativos:
    - Nenhum.

### Instalação
  * Pontos positivos:
    - SQLite funcional, seed automático, migrations automatizadas.

  * Pontos negativos:
    - Nenhum.

---

# 📊 Matriz de Avaliação de Projetos

| **Critério**                   | **Peso** | **Nota** | **Resultado Ponderado**                  |
|-------------------------------|----------|----------|------------------------------------------|
| **Funcionalidade**            | 30%      | 10       | 3,0                                      |
| **Qualidade do Código**       | 20%      | 8        | 1,6                                      |
| **Eficiência e Desempenho**   | 20%      | 6        | 1,2                                      |
| **Inovação e Diferenciais**   | 10%      | 7        | 0,7                                      |
| **Documentação e Organização**| 10%      | 10       | 1,0                                      |
| **Resolução de Feedbacks**    | 10%      | 5        | 0,5                                      |
| **Total**                     | 100%     | -        | **8,0**                                  |

## 🎯 **Nota Final: 8 / 10**
