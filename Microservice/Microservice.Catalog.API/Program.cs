using Microservice.Catalog.API;
using Microservice.Catalog.API.Features.Categories;
using Microservice.Catalog.API.Options;
using Microservice.Catalog.API.Repositories;
using Microservice.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 

builder.Services.AddOptionsExt();

builder.Services.AddDatabaseServiceExt();

builder.Services.AddCommonServicesExt(typeof(CatalogAssembly));

var app = builder.Build();

app.AddCategoryGroupEndpointExt();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();

