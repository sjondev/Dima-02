using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;

namespace Dima.Api.Endpoints.Orders;

public class PayOrderEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/{number}/pay", HandleAsync)
            .WithName("Orders: Pay an order")
            .WithSummary("Marca um pedido como pago")
            .WithDescription("Marca um pedido como pago")
            .WithOrder(3)
            .Produces<Response<Order?>>();

    private static async Task<IResult> HandleAsync(
        IOrderHandler handler,
        string number,
        PayOrderRequest request,
        ClaimsPrincipal user)
    {
        request.OrderNumber = number;
        request.UserId = user.Identity!.Name ?? string.Empty;

        var result = await handler.PayOrderAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result)
            : TypedResults.BadRequest(result);
    }
}