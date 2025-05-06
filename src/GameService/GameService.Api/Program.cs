using Common.Stuff.Mediator;
using GameService.Api.Endpoints;
using GameService.Api.Middleware;
using GameService.Application;
using GameService.Application.Converters;
using GameService.Domain.Events;
using GameService.Infrastructure;
using GameService.Infrastructure.Projections;
using Marten;
using Marten.Events.Projections;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<GameContext>(options =>
    options.UseNpgsql(connectionString)
//options.UseInMemoryDatabase("GameService")
);

builder.Services.AddMarten(options =>
{
    // Establish the connection string to your Marten database
    options.Connection(builder.Configuration.GetConnectionString("Marten")!);

    options.Events.AddEventType(typeof(GameCreated));
    options.Events.AddEventType(typeof(ReviewAdded));

    options.Events.StreamIdentity = Marten.Events.StreamIdentity.AsGuid;

    // Specify that we want to use STJ as our serializer
    options.UseSystemTextJsonForSerialization();

    options.AutoCreateSchemaObjects = AutoCreate.All;

    //options.Projections.Add<GameRatingInfoProjection>(ProjectionLifecycle.Inline);

}).UseLightweightSessions();

builder.Services.AddScoped<IMediator, Mediator>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new GenreJsonConverter());
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        //options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.Http2);
        // Use this for docker /podman compability.
        // options.AddServer("https://localhost:8081");
    });
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.MapGameEndpoints();
app.UseCors("AllowAll");

app.Run();