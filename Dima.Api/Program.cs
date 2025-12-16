using System.Security.Claims;
using Dima.Api.Data;
using Dima.Api.Endpoints;
using Dima.Api.Handlers;
using Dima.Api.Models;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// configurações importantes para documentação do Shashbuckle para o swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});

// Essa parte permite saber quem é
builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();


// Essa parte permite o que podemos fazer ou seja se podemos trabalhar com Clamis,Roles, etc..
builder.Services.AddAuthorization();

// se conectando a base de dados no Docker e utilizando o user-secrets para proteger minhas chaves.
var CnnString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

// configurando um banco de dados com useSqlServer, mais posso ter varios.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(CnnString));

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();
builder.Services.AddIdentityCore<User>()
    .AddRoles<IdentityRole<long>>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddApiEndpoints();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => new { message = "OK" });
app.MapEndpoints(); // metodo que acabei de criar na padronização dos endpoints

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapPost("/logout", async (
        SignInManager<User> signInManager) =>
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    })
    .RequireAuthorization();

app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapGet("/roles", (
        ClaimsPrincipal user) =>
    {
        if(user.Identity is null || !user.Identity.IsAuthenticated) 
            return Results.Unauthorized();
        
        // var identity = user.Identity as ClaimsIdentity;
        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity.FindAll(identity.RoleClaimType).Select(role => new
        {
            role.Issuer,
            role.OriginalIssuer,
            role.Type,
            role.Value,
            role.ValueType
        });
        
        return TypedResults.Json(roles);
    })
    .RequireAuthorization();

app.Run();


