# Useful commands

Make sure to have the latest version of dotnet tools installed.

`dotnet tool update --global dotnet-ef`

Mark your API project as a startup project.

Set the Infrastructure project as Default project if using Package Manager Console.

How to create migrations.

If using Package Manager Console, navigate to the infrastucture project directory.

`dotnet ef migrations add InitialCreate -s ..\DataWarehouse.Api\DataWarehouse.Api.csproj`

How to run the database update against a running podman container.

`dotnet ef database update -c DataWarehouseDbContext -s ..\DataWarehouse.Api\DataWarehouse.Api.csproj`


To create a database, run the following script 
in SQL Server Management Studio or Azure Data Studio:

USE master; 


IF NOT EXISTS ( SELECT name FROM sys.databases WHERE name = N'DataWarehouse' ) CREATE DATABASE [DataWarehouse]; 
Go