using Coupons.Application.Abstractors.Interfaces;
using Coupons.Application.Coupons.Commands;
using Coupons.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Coupons.ApplicationTests.Commands;

public class CreateCouponCommandHandlerTests
{
    private readonly Mock<ICouponRepository> _couponRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult()
    {
        // Arrange
        const string code = "50FF";
        var couponCode = CouponCode.Create(code).Value;
        var handler = new CreateCouponCommandHandler(_couponRepositoryMock.Object, _unitOfWorkMock.Object);
        var command = new CreateCouponCommand(couponCode.Value, 20m, 45m, DateTime.UtcNow);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }
}