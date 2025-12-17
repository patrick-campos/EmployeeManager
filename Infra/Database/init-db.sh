#!/bin/bash
set -e

echo "Criando usuário da aplicação e estrutura do banco..."

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL

-- Criação do usuário da aplicação
CREATE USER ${APP_DB_USER} WITH PASSWORD '${APP_DB_PASSWORD}';

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE IF NOT EXISTS position (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(150) NOT NULL
);

CREATE TABLE IF NOT EXISTS documentType (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(150) NOT NULL
);


CREATE TABLE IF NOT EXISTS employee (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    firstname VARCHAR(100) NOT NULL,
    lastname VARCHAR(100) NOT NULL,
    mail VARCHAR(150) UNIQUE NOT NULL,
    documentnumber VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    position_id UUID NOT NULL REFERENCES position(id) ON DELETE RESTRICT,
    document_type_id UUID NOT NULL REFERENCES documentType(id) ON DELETE RESTRICT
);

INSERT INTO position (name)
SELECT 'director'
WHERE NOT EXISTS (SELECT 1 FROM position WHERE name = 'director');

INSERT INTO position (name)
SELECT 'normal'
WHERE NOT EXISTS (SELECT 1 FROM position WHERE name = 'normal');

INSERT INTO position (name)
SELECT 'manager'
WHERE NOT EXISTS (SELECT 1 FROM position WHERE name = 'manager');

-- DocumentType
INSERT INTO documenttype (name)
SELECT 'cpf'
WHERE NOT EXISTS (SELECT 1 FROM documenttype WHERE name = 'cpf');

INSERT INTO documenttype (name)
SELECT 'identification number'
WHERE NOT EXISTS (SELECT 1 FROM documenttype WHERE name = 'identification number');

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
