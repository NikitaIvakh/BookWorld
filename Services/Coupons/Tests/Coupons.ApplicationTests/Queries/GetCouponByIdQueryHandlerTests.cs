using Coupons.Application.Abstractors.Interfaces;
using Coupons.Application.Coupons.Queries.GetById;
using Coupons.Domain.Entities;
using Coupons.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Coupons.ApplicationTests.Queries;

public sealed class GetCouponByIdQueryHandlerTests
{
    private readonly Mock<ICouponRepository> _couponRepositoryMock = new();

    [Fact]
    public async Task Handle_Should_ReturnCouponResponse()
    {
        // Arrange
        const string code = "51OFF";
        var id = Guid.Parse("7574F5CF-56DC-44E3-9FE9-1BC1A6E6E6AD");
        var couponCode = CouponCode.Create(code).Value;
        var coupon = Coupon.Create(id, couponCode, 20, 89, DateTime.UtcNow.AddDays(1));

        _couponRepositoryMock.Setup(key => key.GetCouponAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(coupon.Value);

        var command = new GetCouponByIdQuery(id);
        var handler = new GetCouponByIdQueryHandler(_couponRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
        result.Value.CouponCode.Should().Be("51OFF");
    }

    [Fact]
    public async Task Handle_Should_ReturnErrorForCouponNotFound()
    {
        // Arrange
        const string code = "51OFF";
        var id = Guid.Parse("7574F5CF-56DC-44E3-9FE9-1BC1A6E6E6AD");
        var couponCode = CouponCode.Create(code).Value;
        var coupon = Coupon.Create(Guid.Parse("7574F5CF-56DC-44E3-9FE9-1BC1A6E6E6AD"), couponCode, 20, 89, DateTime.UtcNow.AddDays(1));

        _couponRepositoryMock.Setup(key => key.GetCouponAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<Coupon>());

        var command = new GetCouponByIdQuery(id);
        var handler = new GetCouponByIdQueryHandler(_couponRepositoryMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
    }
}