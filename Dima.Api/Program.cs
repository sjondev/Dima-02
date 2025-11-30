using Dima.Api.Data;
using Dima.Api.Handlers;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// se conectando a base de dados no Docker e utilizando o user-secrets para proteger minhas chaves.
var CnnString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

// configurando um banco de dados com useSqlServer, mais posso ter varios.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(CnnString));

// configurações importantes para documentação do Shashbuckle para o swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});
builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Construindo os pontos de acesso á nossa API.
app.MapPost(
    "/v1/categories", 
    (CreateCategoryRequest req, ICategoryHandler handler) => handler.CreateAsync(req))
    .WithName("Categories/Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category>>();

app.Run();


