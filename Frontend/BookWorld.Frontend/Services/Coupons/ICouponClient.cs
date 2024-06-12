namespace BookWorld.Frontend.Services.Coupons;

public partial interface ICouponClient
{
    public HttpClient HttpClient { get; }
}