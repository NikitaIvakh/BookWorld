﻿namespace Coupons.Domain.Primitives;

public interface IAuditableEntity
{
    public DateTime CreatedDate { get; set; }

   public DateTime CouponValidityPeriod { get; set; }
}