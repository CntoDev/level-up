#!/bin/bash

psql --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" --file /data/build-db.sql
psql --username "$POSTGRES_USER" --dbname identity --file /data/identity.sql
psql --username "$POSTGRES_USER" --dbname roster --file /data/roster.sql
psql --username "$POSTGRES_USER" --dbname roster --file /data/process.sql
psql --username "$POSTGRES_USER" --dbname roster --file /data/em-history.sql
psql --username "$POSTGRES_USER" --dbname roster --file /data/seed.sql
