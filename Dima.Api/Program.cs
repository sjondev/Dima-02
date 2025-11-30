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
    async (CreateCategoryRequest req, ICategoryHandler handler) => await handler.CreateAsync(req))
    .WithName("Categories/Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response<Category?>>();

app.MapPut(
        "/v1/categories/{id:long}",
        async (
            long id,
            UpdateCategoryRequest req,
            ICategoryHandler handler) =>
        {
            req.Id = id;
            return await handler.UpdateAsync(req);
        })
    .WithName("Categories/Update")
    .WithSummary("Atualiza uma categoria")
    .Produces<Response<Category?>>();

app.MapDelete(
        "/v1/categories/{id:long}",
        async (long id, ICategoryHandler handler) =>
        {
            var request = new DeleteCategoryRequest { Id = id };
            return await handler.DeleteAsync(request);
        })
    .WithName("Categories/Delete")
    .WithSummary("Deleta uma categoria")
    .Produces<Response<Category?>>(); 

    app.MapGet(
            "/v1/categories/{id:long}",
            async (long id, ICategoryHandler handler) =>
            {
                var request = new GetCategoryByIdRequest { Id = id, UserId = "teste@jonatas.io"};
                return await handler.GetByIdAsync(request);
            })
        .WithName("Categories/GetById")
        .WithSummary("Pegando uma categoria por id")
        .Produces<Response<Category?>>();

    app.MapGet(
            "/v1/categories",
            async (ICategoryHandler handler) =>
            {
                var request = new GetAllCategoriesRequest { UserId = "teste@jonatas.io"};
                return await handler.GetAllAsync(request);
            })
        .WithName("Categories/Get")
        .WithSummary("Pegando todas  categorias")
        .Produces<Response<List<Category>?>>(); 

app.Run();


