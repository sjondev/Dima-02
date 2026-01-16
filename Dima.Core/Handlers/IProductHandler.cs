using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;

namespace Dima.Core.Handlers;

public interface IProductHandler
{
    Task<PagedResponse<List<Product>?>> GetAllProductAsync (GetAllProductsRequest request);
    Task<Response<Product?>> GetBySlugProductAsync (GetProductBySlugRequest request);
}