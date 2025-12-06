using Dima.Api.Common.Api;
using Dima.Api.Endpoints.Categories;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;

namespace Dima.Api.Endpoints;

public static class Endpoint
{
    // Extension method (Método de extenção) - estudar depois.
    public static void MapEndpoints(this WebApplication app)
    {
        // criando um grupo de rotas aqui!
        var endpoints = app.MapGroup("");
        endpoints.MapGroup("v1/categories")
            .WithTags("Categories")
            .MapEndpoint<CreateCategoryEndpoint>()
            .MapEndpoint<UpdateCategoryEndpoint>()
            .MapEndpoint<DeleteCategoryEndpoint>()
            .MapEndpoint<GetCategoryByIdEndpoint>()
            .MapEndpoint<GetAllCategoryEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
        where TEndpoint : IEndpoint
    {
        TEndpoint.Map(app);
        return app;
    }
}