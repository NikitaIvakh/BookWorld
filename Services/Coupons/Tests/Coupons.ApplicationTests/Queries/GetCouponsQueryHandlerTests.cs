using Coupons.Application.Abstractors.Interfaces;
using Coupons.Application.Coupons.Queries.GetCoupons;
using Coupons.Domain.Entities;
using Coupons.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Coupons.ApplicationTests.Queries;

public sealed class GetCouponsQueryHandlerTests
{
    private readonly Mock<ICouponRepository> _couponRepository = new();

    [Fact]
    public async Task Handle_Should_ReturnCouponCollection_Success()
    {
        // Arrange
        var id1 = Guid.Parse("E2C29D3F-7CC2-4FDE-87FF-C2BFA8A3812B");
        var id2 = Guid.Parse("E2C29D3F-7CC2-4FDE-87FF-C2BFA8A3812B");

        const string code1 = "5OFF";
        const string code2 = "10OFF";

        var couponCode1 = CouponCode.Create(code1).Value;
        var couponCode2 = CouponCode.Create(code2).Value;

        var coupon1 = Coupon.Create(id1, couponCode1, 20, 90, DateTime.UtcNow.AddDays(1));
        var coupon2 = Coupon.Create(id2, couponCode2, 10, 50, DateTime.UtcNow.AddDays(1));

        var coupons = new List<Coupon> { coupon1.Value, coupon2.Value };

        _couponRepository.Setup(key => key.GetCouponsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(coupons);

        var query = new GetCouponsQuery(code1, "couponcode", "asc", 1, 2);
        var handler = new GetCouponsQueryHandler(_couponRepository.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_CallGetCouponsFromRepository_Once()
    {
        // Arrange
        var id1 = Guid.Parse("E2C29D3F-7CC2-4FDE-87FF-C2BFA8A3812B");
        var id2 = Guid.Parse("E2C29D3F-7CC2-4FDE-87FF-C2BFA8A3812B");

        const string code1 = "5OFF";
        const string code2 = "10OFF";

        var couponCode1 = CouponCode.Create(code1).Value;
        var couponCode2 = CouponCode.Create(code2).Value;

        var coupon1 = Coupon.Create(id1, couponCode1, 20, 90, DateTime.UtcNow.AddDays(1));
        var coupon2 = Coupon.Create(id2, couponCode2, 10, 50, DateTime.UtcNow.AddDays(1));

        var coupons = new List<Coupon> { coupon1.Value, coupon2.Value };

        _couponRepository.Setup(key => key.GetCouponsAsync(It.IsAny<CancellationToken>())).ReturnsAsync(coupons);

        var query = new GetCouponsQuery(code2, "couponcode", "desc", 1, 2);
        var handler = new GetCouponsQueryHandler(_couponRepository.Object);

        // Act
        await handler.Handle(query, default);

        // Assert
        _couponRepository.Verify(key => key.GetCouponsAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}