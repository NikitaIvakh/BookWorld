using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Coupons.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        RegisterServices(services);
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblies([Assembly.GetExecutingAssembly()]));
    }
}