using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;

namespace Dima.Core.Handlers;

public interface IOrderHandler
{
    Task<Response<Order?>> CancelOrderAsync(CancelOrderRequest request);
    Task<Response<Order?>> CreateOrderAsync(CreateOrderRequest request);
    Task<Response<Order?>> PayOrderAsync(PayOrderRequest request);
    Task<Response<Order?>> RefundOrderAsync(RefundOrderRequest request);
    Task<PagedResponse<List<Order>?>> GetAllOrderAsync(GetAllOrdersRequest request);
    Task<Response<Order?>> GetNumberOrderAsync(GetOrderByNumberRequest request);
}