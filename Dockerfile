FROM postgres
COPY renter-api/Renter/migrate.sql /docker-entrypoint-initdb.d/
