using AutoMapper;
using GameService.Application.Commands;
using GameService.Application.Queries;
using GameService.Infrastructure.Entities;
using MediatR;
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

            gameGroup.MapGet("/", async (IMediator mediator, IMapper mapper) =>
            {
                var query = new GetGamesQuery();
                var games = await mediator.Send(query);

                return Results.Ok(mapper.Map<IEnumerable<GameEntity>>(games));
            })
            .WithOpenApi()
            .WithName("GetGamesTest")
            .WithOrder(0);

            gameGroup.MapGet("/{id:guid}", async (Guid id, IMediator mediator, IMapper mapper) =>
            {
                var query = new GetGameByIdQuery(id);
                var game = await mediator.Send(query);

                return game is null ? Results.NotFound() : Results.Ok(game);
            })
            .WithOpenApi()
            .WithName("GetGameById")
            .WithOrder(0);

            gameGroup.MapPost("/", async (CreateGameCommand command, IMediator mediator) =>
            {
                var createdId = await mediator.Send(command);
                return Results.Created($"/games/{createdId}", createdId);
            })
            .WithOpenApi()
            .WithName("CreateGame");

            gameGroup.MapPut("/{id:guid}", async context =>
            {
                var id = context.Request.RouteValues["id"];
                // Logic to update a game by id
                await context.Response.WriteAsync($"Game updated for id: {id}");
            })
            .WithOpenApi()
            .WithName("UpdateGame")
            .WithOrder(1);
            
            gameGroup.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var command = new RemoveGameCommand(id);
                await mediator.Send(command);

                return Results.NoContent();
            })
            .WithOpenApi()
            .WithName("DeleteGame")
            .WithDescription("Deletes a game by id")
            .WithOrder(2);
        }
    }
}
