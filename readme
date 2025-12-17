# Employee Manager

## Descrição do Projeto

Este projeto é um sistema de gerenciamento de funcionários, composto por:

- **Frontend**: Aplicação React/Vite com Node 24.
- **Backend**: API em .NET 8 estruturada com **Arquitetura Hexagonal** (Ports & Adapters), separando responsabilidades de domínio, aplicação e infraestrutura.
- **Banco de Dados**: Container externo (PostgreSQL/MySQL) utilizado via Docker Compose.
- **Documentação de API**: Integrada via **Swagger**, com todas as rotas mapeadas e testáveis diretamente no navegador.

O objetivo do projeto é demonstrar boas práticas de arquitetura, Docker, comunicação entre serviços e configuração de ambiente para execução em diferentes máquinas.


**Detalhes da organização:**

- **Backend**
  - `Api/`: Ponto de entrada da aplicação, controllers e endpoints.
  - `Domain/`: Lógica de negócio, entidades e regras de validação.
  - `Infra/`: Acesso a banco de dados, repositórios e configurações.
- **Frontend**
  - React/Vite configurado para desenvolvimento rápido, hot reload e variáveis de ambiente.
- **Infra/Dockerfiles**
  - Multi-stage builds para otimizar tamanho das imagens.
  - Configuração para execução isolada via Docker Compose.
- **docker-compose.yml**
  - Orquestra os serviços (frontend, backend e banco externo).
  - Permite rodar o projeto completo com apenas um comando.

---

## Pré-requisitos

- [Docker](https://www.docker.com/) instalado
- [Docker Compose](https://docs.docker.com/compose/) instalado
- (Opcional) Node.js 24 para executar o frontend localmente sem Docker
- (Opcional) .NET 8 SDK para rodar o backend localmente sem Docker

---

## Configuração do ambiente

As aplicações utilizam variáveis de ambiente para configurar URLs e credenciais:

- **Frontend**
  - `VITE_API_URL`: URL da API backend (ex.: `http://backend:8080` dentro do Docker ou `http://localhost:8080` em dev local)
- **Backend**
  - `ConnectionStrings__DefaultConnection`: Conexão com o banco de dados

> O Docker Compose já define as variáveis necessárias para execução dentro dos containers.

---

## Como Executar

1. Clone o projeto:

2. Inicie os containers via Docker Compose:

    docker compose up --build


3. Acesse o frontend:

    Se estiver usando VM: http://<IP-da-VM>:5173

    Se estiver rodando local Docker: http://localhost:5173

4. O backend estará disponível dentro da network Docker em:

    http://backend:8080

    Para acessar via host, use http://localhost:8080 se a porta estiver mapeada.