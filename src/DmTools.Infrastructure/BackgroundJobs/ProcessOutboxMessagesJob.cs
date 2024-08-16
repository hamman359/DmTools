using DmTools.Persistence;
using DmTools.Persistence.Outbox;

using KJWT.SharedKernel.DomainEvents;

using MediatR;

using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using Polly;
using Polly.Retry;

using Quartz;

namespace DmTools.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IPublisher _publisher;

    public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher)
    {
        _dbContext = dbContext;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        //TODO Add exception Handling
        List<OutboxMessage> messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage outboxMessage in messages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(
                    outboxMessage.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

            if (domainEvent is null)
            {
                continue;
            }

            ResiliencePipeline pipeline = new ResiliencePipelineBuilder()
                .AddRetry(new RetryStrategyOptions
                {
                    ShouldHandle = new PredicateBuilder().Handle<Exception>(),
                    Delay = TimeSpan.FromSeconds(1),
                    MaxRetryAttempts = 3,
                    BackoffType = DelayBackoffType.Exponential
                })
                .Build();

            await pipeline.ExecuteAsync(
                async token =>
                        await _publisher.Publish<IDomainEvent>(
                            domainEvent,
                            context.CancellationToken));

            //outboxMessage.Error = pipelineResult.FinalException?.ToString();
            outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
    }
}