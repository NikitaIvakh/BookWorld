using Coupons.Application.Abstractors.Interfaces;
using Coupons.Infrastructure.Interceptors;
using Coupons.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Coupons.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureRepositories(services);
        AddInterceptors(services);
        ConfigureDatabase(services, configuration);
        ApplyMigrations(services);
    }

    private static void ConfigureRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICouponRepository, CouponRepository>();
    }

    private static void AddInterceptors(IServiceCollection services)
    {
        services.AddScoped<CouponAddCreateDateInterceptor>();
    }

    private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var couponCreateDateInterceptor = sp.GetService<CouponAddCreateDateInterceptor>();
            var convertDomainEventToOutboxMessageInterceptor = sp.GetService<ConvertDomainEventToOutboxMessageInterceptor>();
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).AddInterceptors(couponCreateDateInterceptor!, convertDomainEventToOutboxMessageInterceptor!);
        });
    }

    private static void ApplyMigrations(this IServiceCollection services)
    {
        var builder = services.BuildServiceProvider();
        var scope = builder.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (db.Database.GetPendingMigrations().Any())
        {
            db.Database.Migrate();
        }
    }
}