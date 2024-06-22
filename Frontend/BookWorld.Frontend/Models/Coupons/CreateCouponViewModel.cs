namespace BookWorld.Frontend.Models.Coupons;

public sealed class CreateCouponViewModel
{
    public string CouponCode { get; private set; }

    public decimal DiscountAmount { get; private set; }

    public decimal MinAmount { get; private set; }
}