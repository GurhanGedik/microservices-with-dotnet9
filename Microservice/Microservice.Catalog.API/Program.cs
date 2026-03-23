using Microservice.Catalog.API.Features.Categories;
using Microservice.Catalog.API.Options;
using Microservice.Catalog.API.Repositories;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddOptionsExt();

builder.Services.AddDatabaseServiceExt();

var app = builder.Build();

app.AddCategoryGroupEndpointExt();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.Run();

