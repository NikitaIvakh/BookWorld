﻿using Coupons.Application.Coupons.Commands.Create;
using Coupons.Application.Coupons.Commands.Delete;
using Coupons.Application.Coupons.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coupons.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CouponController(ISender sender) : ControllerBase
{
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    [HttpGet("GetCoupon/{id:guid}")]
    public async Task<IActionResult> GetCoupon(Guid id, CancellationToken token)
    {
        var coupon = new GetCouponByIdQuery(id);
        var result = await sender.Send(coupon, token);

        return result.IsSuccess ? Ok(result) : BadRequest($"{result.Error.Code}: {result.Error.Message}");
    }

    [HttpPost("CreateCoupon")]
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