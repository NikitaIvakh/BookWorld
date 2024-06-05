using Coupons.Application.Coupons.Commands;
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

    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    [HttpPost("CreateCoupon")]
    public async Task<IActionResult> CreateCoupon([FromQuery] CreateCouponCommand couponCommand, CancellationToken token)
    {
        var command = new CreateCouponCommand(couponCommand.CouponCode, couponCommand.DiscountAmount,
            couponCommand.MinAmount, couponCommand.CouponValidityPeriod);

        var result = await sender.Send(command, token);
        return result.IsSuccess ? Ok(result.Value) : BadRequest($"{result.Error.Code}: {result.Error.Message}");
    }

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}