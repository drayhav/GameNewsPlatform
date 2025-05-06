using DataWarehouse.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Reflection;
using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthorization();
builder.Services.AddDbContext<DataWarehouseDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DataWarehouseConnection"),
        b => b.MigrationsAssembly(Assembly.GetAssembly(typeof(DataWarehouseDbContext))!));
});

builder.Services.AddWolverine(options => 
{
    options.UseRabbitMq(rabbit =>
    {
        rabbit.HostName = builder.Configuration["RabbitMQ:HostName"]!;
        rabbit.UserName = builder.Configuration["RabbitMQ:UserName"]!;
        rabbit.Password = builder.Configuration["RabbitMQ:Password"]!;
    })
    .UseConventionalRouting()
    .AutoProvision();

    options.ApplicationAssembly = typeof(DataWarehouseDbContext).Assembly;
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapGet("/", async (DataWarehouseDbContext dbContext) =>
{
    var games = await dbContext.Games.ToListAsync();
    return Results.Ok(games);
})
.WithName("GetGames");

app.Run();