using Coupons.Application.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Coupons.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void ConfigureApplicationServices(this IServiceCollection services)
    {
        RegisterServices(services);
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly, includeInternalTypes: true);
        services.AddMediatR(config => config.RegisterServicesFromAssemblies([AssemblyReference.Assembly]));
    }
}