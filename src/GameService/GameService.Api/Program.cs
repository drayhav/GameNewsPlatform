using GameService.Api.Endpoints;
using GameService.Api.Middleware;
using GameService.Application;
using GameService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddOpenApi();
        builder.Services.AddEndpointsApiExplorer();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<GameContext>(options =>
            options.UseNpgsql(connectionString)
            //options.UseInMemoryDatabase("GameService")
        );

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
                options.AddServer("https://localhost:8081");
            });
        }

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.MapGameEndpoints();
        app.UseCors("AllowAll");

        app.Run();
    }
}