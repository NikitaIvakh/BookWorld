using Coupons.Application.Abstractors.Interfaces;
using Coupons.Application.Coupons.Commands.Create;
using Coupons.Domain.Entities;
using Coupons.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Coupons.ApplicationTests.Commands;

public sealed class CreateCouponCommandHandlerTests
{
    private readonly Mock<ICouponRepository> _couponRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult()
    {
        // Arrange
        const string code = "50FF";
        var couponCode = CouponCode.Create(code).Value;
        _couponRepositoryMock.Setup(key => key.IsUniqueCouponCode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var command = new CreateCouponCommand(couponCode.Value, 20m, 45m, DateTime.UtcNow.AddDays(1));
        var handler = new CreateCouponCommandHandler(_couponRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeFalse();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWorkForCreate_Once()
    {
        // Arrange
        const string code = "50FF";
        var couponCode = CouponCode.Create(code).Value;
        _couponRepositoryMock.Setup(key => key.IsUniqueCouponCode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var command = new CreateCouponCommand(couponCode.Value, 20m, 45m, DateTime.UtcNow.AddDays(1));
        var handler = new CreateCouponCommandHandler(_couponRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        _couponRepositoryMock.Verify(key => key.CreateCoupon(It.IsAny<Coupon>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWorkForSaveChanges_Once()
    {
        // Arrange
        const string code = "50FF";
        var couponCode = CouponCode.Create(code).Value;
        _couponRepositoryMock.Setup(key => key.IsUniqueCouponCode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var command = new CreateCouponCommand(couponCode.Value, 20m, 45m, DateTime.UtcNow.AddDays(1));
        var handler = new CreateCouponCommandHandler(_couponRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(key => key.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_NotCreateCoupon_Failure()
    {
        // Arrange
        const string code = "50FF";
        var couponCode = CouponCode.Create(code).Value;
        _couponRepositoryMock.Setup(key => key.IsUniqueCouponCode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var command = new CreateCouponCommand(couponCode.Value, 220m, 45m, DateTime.UtcNow.AddDays(1));
        var handler = new CreateCouponCommandHandler(_couponRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_NotCallCreateCoupon_Never()
    {
        // Arrange
        const string code = "50FF";
        var couponCode = CouponCode.Create(code).Value;
        _couponRepositoryMock.Setup(key => key.IsUniqueCouponCode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var command = new CreateCouponCommand(couponCode.Value, 220m, 45m, DateTime.UtcNow.AddDays(1));
        var handler = new CreateCouponCommandHandler(_couponRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        _couponRepositoryMock.Verify(key => key.CreateCoupon(It.IsAny<Coupon>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_Should_NotCallUnitOfWork_Never()
    {
        // Arrange
        const string code = "50FF";
        var couponCode = CouponCode.Create(code).Value;
        _couponRepositoryMock.Setup(key => key.IsUniqueCouponCode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var command = new CreateCouponCommand(couponCode.Value, 220m, 45m, DateTime.UtcNow.AddDays(1));
        var handler = new CreateCouponCommandHandler(_couponRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(key => key.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessForUniqueCouponCode()
    {
        // Arrange
        const string code = "50FF";
        var couponCode = CouponCode.Create(code).Value;
        _couponRepositoryMock.Setup(key => key.IsUniqueCouponCode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(true);
        var command = new CreateCouponCommand(couponCode.Value, 20, 50, DateTime.UtcNow.AddDays(1));
        var handler = new CreateCouponCommandHandler(_couponRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureForUniqueCouponCode()
    {
        // Arrange
        const string code = "5OFF";
        var couponCode = CouponCode.Create(code).Value;
        _couponRepositoryMock.Setup(key => key.IsUniqueCouponCode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        var command = new CreateCouponCommand(couponCode.Value, 20, 70, DateTime.UtcNow.AddDays(1));
        var handler = new CreateCouponCommandHandler(_couponRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_NotCallCreateMethodWithNotUniqueCouponCode()
    {
        // Arrange
        const string code = "5OFF";
        var couponCode = CouponCode.Create(code).Value;
        _couponRepositoryMock.Setup(key => key.IsUniqueCouponCode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        var command = new CreateCouponCommand(couponCode.Value, 20, 70, DateTime.UtcNow.AddDays(1));
        var handler = new CreateCouponCommandHandler(_couponRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        _couponRepositoryMock.Verify(key => key.CreateCoupon(It.IsAny<Coupon>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_Should_NotCallSaveChangesAsyncWithNotUniqueCouponCode()
    {
        // Arrange
        const string code = "45OFF";
        var couponCode = CouponCode.Create(code).Value;
        _couponRepositoryMock.Setup(key => key.IsUniqueCouponCode(It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(false);
        var command = new CreateCouponCommand(couponCode.Value, 20, 89, DateTime.UtcNow.AddDays(1));
        var handler = new CreateCouponCommandHandler(_couponRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(key => key.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}