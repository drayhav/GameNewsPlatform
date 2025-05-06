using Common.Stuff.Mediator;
using GameService.Api.Requests;
using GameService.Application.Commands;
using GameService.Application.Queries;
using GameService.Domain.Aggregates;
using GameService.Infrastructure.Entities;
using Microsoft.OpenApi.Models;

namespace GameService.Api.Endpoints
{
    public static class GameEndpoints
    {
        public static void MapGameEndpoints(this WebApplication app)
        {
            var gameGroup = app
                .MapGroup("games")
                .WithTags("games")
                .WithOpenApi(o =>
                {
                    o.Servers.Add(new OpenApiServer()
                    {
                        Url = "https://localhost:8081"
                    });

                    return o;
                });

            gameGroup.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var query = new GetGameByIdQuery(id);
                var game = await mediator.Send<GetGameByIdQuery, Game>(query);
                var gameEntity = GameEntity.FromGame(game);

                return game is null ? Results.NotFound() : Results.Ok(gameEntity);
            })
            .WithOpenApi()
            .WithName("GetGameById");

            gameGroup.MapPost("/", async (CreateGameRequest request, IMediator mediator) =>
            {
                var command = request.ToCommand();
                var createdId = await mediator.Send<CreateGameCommand, Guid>(command);
                return Results.Created($"/games/{createdId}", createdId);
            })
            .WithOpenApi()
            .WithName("CreateGame");

            gameGroup.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var command = new RemoveGameCommand(id);
                await mediator.Send<RemoveGameCommand, bool>(command);

                return Results.NoContent();
            })
            .WithOpenApi()
            .WithName("DeleteGame")
            .WithDescription("Deletes a game by id");
        }
    }
}