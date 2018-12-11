# Base de datos
En este hackathon-ish se utilizó el gestor de base de datos PostgreSQL. En este directorio se encuentran archivos relacionados con la base de datos como el *archivo importable* de la base de datos y documentación adicional para entender mejor su funcionamiento.

## ¿Cómo importar la base de datos?
Para importar la base de datos a PostgreSQL es necesario crear la base de datos desde PostgreSQL con los comandos:
```shell
# Entrar al CLI de PostgreSQL.
psql -U postgres -W

# Crear base de datos hackathonishbd.
CREATE DATABASE hackathonishbd;

# Salir del CLI de PostgreSQL.
\q
```

El siguiente paso es dirigirse al directorio del *archivo importable*. Una vez en este directorio se utiliza el siguiente comando para importar la base de datos:
```shell
# Importar archivo dbexport.pgsql a la base de datos hackathonishbd.
psql -U postgres -W hackathonishbd < "dbexport.pgsql"

```

## Comandos útiles del CLI de PostgreSQL
```SQL
# Cambiar contraseña de usuario.
\password

# Listar bases de datos.
\l

# Conectarse a una base de datos.
\c [basededatos]

# Mostrar objetos en una base de datos.
\d

# Mostrar tablas en una base de datos.
\dt

# Describir campos de una tabla.
\dt [tabla]

# Salir del CLI
\q
```
