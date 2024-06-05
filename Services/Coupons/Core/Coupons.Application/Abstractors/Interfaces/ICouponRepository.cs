﻿using Coupons.Domain.Entities;

namespace Coupons.Application.Abstractors.Interfaces;

public interface ICouponRepository
{
    IQueryable<Coupon> GetCoupons();

    Task<bool> IsUniqueCouponCode(Coupon coupon);

    Task<Coupon> CreateCoupon(Coupon coupon, CancellationToken token = default);

    Task<Coupon> DeleteCoupon(Coupon coupon, CancellationToken token = default);
}