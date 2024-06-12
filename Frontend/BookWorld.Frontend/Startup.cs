using BookWorld.Frontend.Services.Coupons;

namespace BookWorld.Frontend;

public static class Startup
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterMicroscopesUrl(services, configuration);
    }

    private static void RegisterMicroscopesUrl(IServiceCollection service, IConfiguration configuration)
    {
        var gatewayService = configuration["Services:Gateway"];

        service.AddHttpClient<ICouponClient, CouponClient>(key => key.BaseAddress = new Uri(gatewayService!));
    }
}