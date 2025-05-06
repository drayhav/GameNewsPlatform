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