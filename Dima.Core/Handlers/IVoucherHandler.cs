using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;

namespace Dima.Core.Handlers;

public interface IVoucherHandler 
{
    Task<Response<Voucher?>> GetByNumberVoucherAsync(GetVoucherByNumberRequest request);
}