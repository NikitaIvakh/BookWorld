using Coupons.Domain.Primitives;
using Coupons.Persistence.BackgroundJobs;
using Coupons.Persistence.Idempotent;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Simpl;
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

        services.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));
    }

    private static void RegisterQuartz(IServiceCollection services)
    {
        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessedOnUtcOutboxMessageJob));

            configure.AddJob<ProcessedOnUtcOutboxMessageJob>(jobKey)
                .AddTrigger(trigger => trigger.ForJob(jobKey).WithSimpleSchedule(scedule => scedule.WithIntervalInSeconds(10).RepeatForever()));

            configure.UseJobFactory<MicrosoftDependencyInjectionJobFactory>();
        });

        services.AddQuartzHostedService();
    }
}