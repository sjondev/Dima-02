using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers;

public class VoucherHandler(AppDbContext context) : IVoucherHandler
{
    public async Task<Response<Voucher?>> GetByNumberVoucherAsync(GetVoucherByNumberRequest request)
    {
        try
        {
            var voucher = await context
                .Vouchers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Number == request.Number && x.IsActive == true);

            return voucher is null 
                    ? new Response<Voucher?>(null, 404, "Voucher not found")
                    : new Response<Voucher?>(voucher);

        }
        catch
        {
            return new Response<Voucher?>(null, 500, "Voucher not fount");
        }
    }
}