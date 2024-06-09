using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Coupons.Persistence.DependencyInjection;

public static class DependencyInjection
{
    public static void ConfigurePersistenceServices(this IServiceCollection services)
    {
        RegisterServices(services);
        RegisterQuartz(services);
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.Scan(selector => selector
            .FromAssemblies(Infrastructure.DependencyInjection.AssemblyReference.Assembly, AssemblyReference.Assembly)
            .AddClasses(false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsImplementedInterfaces()
            .WithScopedLifetime()
        );
    }

    private static void RegisterQuartz(IServiceCollection services)
    {

    }
}