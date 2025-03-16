# Useful commands

Make sure to have the latest version of dotnet tools installed.

`dotnet tool update --global dotnet-ef`

Mark your API project as a startup project.

Set the Infrastructure project as Default project if using Package Manager Console.

How to create migrations.

`dotnet ef migrations add InitialCreate -s ..\GameService.Api\GameService.Api.csproj`

How to run the database update against a running podman container.

`dotnet ef database update -c GameContext -s ..\GameService.Api\GameService.Api.csproj`
