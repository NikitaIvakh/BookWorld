using Coupons.Domain.Entities;
using Coupons.Domain.Primitives;
using Coupons.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Polly;
using Quartz;

namespace Coupons.Persistence.BackgroundJobs;

[DisallowConcurrentExecution]
public sealed class ProcessedOnUtcOutboxMessageJob(ApplicationDbContext dbContext, IPublisher publisher) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        var outboxMessages = await dbContext
            .Set<OutboxMessage>()
            .Where(key => key.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync(CancellationToken.None);

        foreach (var message in outboxMessages)
        {
            var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Content, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            if (domainEvent is null)
                continue;

            const int retryCount = 5;
            var policy = Policy.Handle<Exception>().WaitAndRetryAsync(retryCount, attempt => TimeSpan.FromMilliseconds(50 * attempt));
            await policy.ExecuteAndCaptureAsync(async () =>
            {
                try
                {
                    await publisher.Publish(domainEvent, context.CancellationToken);
                    message.ProcessedOnUtc = DateTime.UtcNow;
                }

                catch (Exception ex)
                {
                    message.Error = ex.Message;
                }
            });
        }

        await dbContext.SaveChangesAsync();
    }
}