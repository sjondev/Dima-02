using Dima.Api.Data;
using Dima.Core.Models;
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
builder.Services.AddTransient<Handler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Construindo os pontos de acesso á nossa API.
app.MapPost(
    "/v1/categories", 
    (Request req, Handler handler) => handler.Handle(req))
    .WithName("Categories/Create")
    .WithSummary("Cria uma nova categoria")
    .Produces<Response>();

app.Run();

// REQUEST
public class Request
{
    // Testando transação na v1
    /*public string Title { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int Type { get; set; }
    public decimal Amount { get; set; }
    public long CategoryId { get; set; }
    public string UserId { get; set; } = string.Empty;*/
    
    // Testando categorias na v1
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}


// response
public class Response
{
    // padronizando as respostas
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

// Handler
public class Handler(AppDbContext context)
{
    public Response Handle(Request request)
    {
        // trabalhando com mudanças na categorias
        var category = new Category
        {
            Title = request.Title,
            Description = request.Description
        };
        
        context.Categories.Add(category);
        context.SaveChanges();
        
        return new Response
        {
            Id = category.Id,
            Title = category.Title,
            Description = category.Description
        };
    }
}


