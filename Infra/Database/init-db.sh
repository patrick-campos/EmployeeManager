#!/bin/bash
set -e

echo "Criando usuário da aplicação e estrutura do banco..."

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL

-- Criação do usuário da aplicação
CREATE USER ${APP_DB_USER} WITH PASSWORD '${APP_DB_PASSWORD}';

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- Criação da tabela Position
CREATE TABLE IF NOT EXISTS Position (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(150) NOT NULL
);

-- Criação da tabela DocumentType
CREATE TABLE IF NOT EXISTS DocumentType (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(150) NOT NULL
);

-- Criação da tabela Employee
CREATE TABLE IF NOT EXISTS Employee (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    firstname VARCHAR(100) NOT NULL,
    lastname VARCHAR(100) NOT NULL,
    mail VARCHAR(150) UNIQUE NOT NULL,
    documentnumber VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    position_id UUID REFERENCES Position(id) ON DELETE SET NULL,
    document_type_id UUID REFERENCES DocumentType(id) ON DELETE SET NULL
);

-- Segurança
REVOKE ALL ON DATABASE ${POSTGRES_DB} FROM PUBLIC;
REVOKE ALL ON SCHEMA public FROM PUBLIC;

GRANT CONNECT ON DATABASE ${POSTGRES_DB} TO ${APP_DB_USER};
GRANT USAGE ON SCHEMA public TO ${APP_DB_USER};

GRANT SELECT, INSERT, UPDATE, DELETE
ON ALL TABLES IN SCHEMA public
TO ${APP_DB_USER};

ALTER DEFAULT PRIVILEGES IN SCHEMA public
GRANT SELECT, INSERT, UPDATE, DELETE
ON TABLES TO ${APP_DB_USER};

EOSQL

echo "Banco inicializado com sucesso."
