using Coupons.Application.Abstractors.Interfaces;
using Coupons.Application.Coupons.Commands.Delete;
using Coupons.Domain.Entities;
using Coupons.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace Coupons.ApplicationTests.Commands;

public sealed class DeleteCouponCommandHandlerTests
{
    private readonly Mock<ICouponRepository> _couponRepository = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    [Fact]
    public async Task Handle_Should_ReturnSuccessResultFirDeleteCoupon()
    {
        // Arrange
        const string code = "5OFF";
        var id = Guid.Parse("1AB86449-45E0-4905-9301-F93A68A8D027");
        var couponCode = CouponCode.Create(code).Value;
        var coupon = Coupon.Create(id, couponCode, 20, 90, DateTime.UtcNow.AddDays(1));

        _couponRepository.Setup(key => key.GetCouponAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(coupon.Value);

        var command = new DeleteCouponCommand(id);
        var handler = new DeleteCouponCommandHandler(_couponRepository.Object, _unitOfWork.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_CallDeleteCouponMethodFromRepository_Once()
    {
        // Arrange
        const string code = "5OFF";
        var id = Guid.Parse("1AB86449-45E0-4905-9301-F93A68A8D027");
        var couponCode = CouponCode.Create(code).Value;
        var coupon = Coupon.Create(id, couponCode, 20, 90, DateTime.UtcNow.AddDays(1));

        _couponRepository.Setup(key => key.GetCouponAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(coupon.Value);

        var command = new DeleteCouponCommand(id);
        var handler = new DeleteCouponCommandHandler(_couponRepository.Object, _unitOfWork.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        _couponRepository.Verify(key => key.DeleteCoupon(It.IsAny<Coupon>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_CallSaveChangesAsyncFromUnitOfWork_Once()
    {
        // Arrange
        const string code = "5OFF";
        var id = Guid.Parse("1AB86449-45E0-4905-9301-F93A68A8D027");
        var couponCode = CouponCode.Create(code).Value;
        var coupon = Coupon.Create(id, couponCode, 20, 90, DateTime.UtcNow.AddDays(1));

        _couponRepository.Setup(key => key.GetCouponAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(coupon.Value);

        var command = new DeleteCouponCommand(id);
        var handler = new DeleteCouponCommandHandler(_couponRepository.Object, _unitOfWork.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        _unitOfWork.Verify(key => key.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_ReturnNotFoundCouponError()
    {
        // Arrange
        const string code = "5OFF";
        var id = Guid.Parse("1AB86449-45E0-4905-9301-F93A68A8D027");
        var couponCode = CouponCode.Create(code).Value;
        var coupon = Coupon.Create(id, couponCode, 20, 90, DateTime.UtcNow.AddDays(1));

        _couponRepository.Setup(key => key.GetCouponAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<Coupon>());

        var command = new DeleteCouponCommand(id);
        var handler = new DeleteCouponCommandHandler(_couponRepository.Object, _unitOfWork.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_NotCallDeleteMethodFromRepository_WhereNotFoundCouponError()
    {
        // Arrange
        const string code = "5OFF";
        var id = Guid.Parse("1AB86449-45E0-4905-9301-F93A68A8D027");
        var couponCode = CouponCode.Create(code).Value;
        var coupon = Coupon.Create(id, couponCode, 20, 90, DateTime.UtcNow.AddDays(1));

        _couponRepository.Setup(key => key.GetCouponAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<Coupon>());

        var command = new DeleteCouponCommand(id);
        var handler = new DeleteCouponCommandHandler(_couponRepository.Object, _unitOfWork.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        _couponRepository.Verify(key => key.DeleteCoupon(It.IsAny<Coupon>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_Should_NotCallSaveChangesAsyncMethodFromUnitOfWork_WhereNotFoundCouponError()
    {
        // Arrange
        const string code = "5OFF";
        var id = Guid.Parse("1AB86449-45E0-4905-9301-F93A68A8D027");
        var couponCode = CouponCode.Create(code).Value;
        var coupon = Coupon.Create(id, couponCode, 20, 90, DateTime.UtcNow.AddDays(1));

        _couponRepository.Setup(key => key.GetCouponAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(It.IsAny<Coupon>());

        var command = new DeleteCouponCommand(id);
        var handler = new DeleteCouponCommandHandler(_couponRepository.Object, _unitOfWork.Object);

        // Act
        await handler.Handle(command, default);

        // Assert
        _unitOfWork.Verify(key => key.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}