using Coupons.API.Abstractors;
using Coupons.API.Contracts;
using Coupons.Application.Coupons.Commands.Create;
using Coupons.Application.Coupons.Commands.Delete;
using Coupons.Application.Coupons.Queries.GetById;
using Coupons.Application.Coupons.Queries.GetCoupons;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CouponController(ISender sender) : ApiController(sender)
{
    private readonly ISender _sender = sender;

    [HttpGet(nameof(GetCoupons))]
    public async Task<IActionResult> GetCoupons(string? searchCode, string? sortColumn, string? sortType, int page, int pageSize, CancellationToken token)
    {
        var coupons = new GetCouponsQuery(searchCode, sortColumn, sortType, page, pageSize);
        var result = await _sender.Send(coupons, token);

        return result.IsSuccess ? Ok(result) : NotFound(result.Error);
    }

    [HttpGet("GetCoupon/{id:guid}")]
    public async Task<IActionResult> GetCoupon(Guid id, CancellationToken token)
    {
        var coupon = new GetCouponByIdQuery(id);
        var result = await _sender.Send(coupon, token);

        return result.IsSuccess ? Ok(result) : NotFound(result.Error);
    }

    [HttpPost(nameof(CreateCoupon))]
    public async Task<IActionResult> CreateCoupon([FromBody] CreateCouponRequest request, CancellationToken token)
    {
        var command = new CreateCouponCommand
            (
                request.CouponCode,
                request.DiscountAmount,
                request.MinAmount,
                request.CouponValidityPeriod
            );

        var result = await _sender.Send(command, token);
        return result.IsFailure ? HandleFailure(result) : CreatedAtAction(nameof(GetCoupon), new { Id = result.Value }, result.Value);
    }

    [HttpDelete("DeleteCoupon/{id:guid}")]
    public async Task<IActionResult> DeleteCoupon(Guid id, CancellationToken token)
    {
        var coupon = new DeleteCouponCommand(id);
        var result = await _sender.Send(coupon, token);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }
}