using Coupons.Application.Coupons.Commands.Create;
using Coupons.Application.Coupons.Commands.Delete;
using Coupons.Application.Coupons.Queries.GetById;
using Coupons.Application.Coupons.Queries.GetCoupons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CouponController(ISender sender) : ControllerBase
{
    [HttpGet(nameof(GetCoupons))]
    public async Task<IActionResult> GetCoupons(string? searchCode, string? sortColumn, string? sortType, int page, int pageSize, CancellationToken token)
    {
        var coupons = new GetCouponsQuery(searchCode, sortColumn, sortType, page, pageSize);
        var result = await sender.Send(coupons, token);

        return result.IsSuccess ? Ok(result) : BadRequest($"{result.Error.Code}: {result.Error.Message}");
    }

    [HttpGet("GetCoupon/{id:guid}")]
    public async Task<IActionResult> GetCoupon(Guid id, CancellationToken token)
    {
        var coupon = new GetCouponByIdQuery(id);
        var result = await sender.Send(coupon, token);

        return result.IsSuccess ? Ok(result) : BadRequest($"{result.Error.Code}: {result.Error.Message}");
    }

    [HttpPost(nameof(CreateCoupon))]
    public async Task<IActionResult> CreateCoupon([FromQuery] CreateCouponCommand couponCommand, CancellationToken token)
    {
        var command = new CreateCouponCommand(couponCommand.CouponCode, couponCommand.DiscountAmount,
            couponCommand.MinAmount, couponCommand.CouponValidityPeriod);

        var result = await sender.Send(command, token);
        return result.IsSuccess ? Ok(result.Value) : BadRequest($"{result.Error.Code}: {result.Error.Message}");
    }

    [HttpDelete("DeleteCoupon/{id:guid}")]
    public async Task<IActionResult> DeleteCoupon(Guid id, CancellationToken token)
    {
        var coupon = new DeleteCouponCommand(id);
        var result = await sender.Send(coupon, token);

        return result.IsSuccess ? Ok(result.Value) : BadRequest($"{result.Error.Code}: {result.Error.Message}");
    }
}