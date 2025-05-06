# About

This is a solution demonstrating an approach to DDD with EventSourcing. It's a purely hobby project created in order to test various things out.
I might come back to it in order to add some new features or try out other technologies and/or patterns.
I solely focus on the backend part of the application, with no GUI other than Scalar.

The GameService application follows a Domain-Driven Design (DDD) approach, utilizing Event Sourcing. 
The application is built using ASP.NET Core, with a focus on clean architecture principles.
It uses PostgreSQL as the database for storing streams of events of the aggregates. It also uses Marten's projections to generate a local readmodel (for the GameService's sake).

On the other hand, the DataWarehouse application is designed to act as a Read Model for the system. It reacts to integration events published by the GameService application to build a readmodel in mssql.
# Useful commands

## Make sure to have the latest version of dotnet tools installed.

`dotnet tool update --global dotnet-ef`

## Migrations
Mark your API project as a startup project.

If using Package Manager Console - set the Infrastructure project as Default project and navigate to the infrastucture project directory.

### How to create migrations.

Navigate to the infrastucture project directory and run the following command.

`dotnet ef migrations add InitialCreate -s ..\DataWarehouse.Api\DataWarehouse.Api.csproj`

### How to run the database update against a running podman container.
To create a database in mssql (if you haven't done so yet), run the following script in SQL Server Management Studio or Azure Data Studio:

```sql
USE master; 


IF NOT EXISTS ( SELECT name FROM sys.databases WHERE name = N'DataWarehouse' ) CREATE DATABASE [DataWarehouse]; 
Go
```
To apply migrations, use the following command:

`dotnet ef database update -c DataWarehouseDbContext -s ..\DataWarehouse.Api\DataWarehouse.Api.csproj`

### Containers
I decided to use Podman for contenraization. It's free in comparison to Docker Desktop, and since I'm using it professionally I thought it would be a good idea to use it for my hobby projects as well.

I use containers to run RabbitMQ, PostgreSQL, and SQL Server.

- RabbitMQ: Message broker for communication between services. I use it with the management plugin enabled so I can use the GUI to see the messages and exchanges/queues.
- PostgreSQL: Event storage for GameService used by Marten.  
- SQL Server: Read model storage for DataWarehouse.  
- pgAdmin: GUI for managing PostgreSQL.

There's also pgAdmin in the docker-compose file, cause it let's me navigate through the database without installing any additional software on my machine.

## How to run the containers
Make sure you have Podman installed and running on your machine.

I use this command to run the containers when in the root directory of the solution:

`podman compose up --build`

