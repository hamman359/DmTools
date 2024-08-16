using DmTools.Persistence;
using DmTools.Persistence.Outbox;

using KJWT.SharedKernel.DomainEvents;
using KJWT.SharedKernel.Messaging;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace DmTools.Infrastructure.Idempotence;

public sealed class IdempotentDomainEventHandler<TDomainEvent>
    : IDomainEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    private readonly INotificationHandler<TDomainEvent> _decorated;
    private readonly ApplicationDbContext _dbContext;

    public IdempotentDomainEventHandler(
        INotificationHandler<TDomainEvent> decorated,
        ApplicationDbContext dbContext)
    {
        _decorated = decorated;
        _dbContext = dbContext;
    }

    public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
    {
        string consumer = _decorated.GetType().Name;

        if (await _dbContext.Set<OutboxMessageConsumer>()
                .AnyAsync(
                    outboxMessageConsumer =>
                        outboxMessageConsumer.Id == notification.Id &&
                        outboxMessageConsumer.Name == consumer,
                    cancellationToken))
        {
            return;
        }

        await _decorated.Handle(notification, cancellationToken);

        await _dbContext.Set<OutboxMessageConsumer>()
            .AddAsync(
            new OutboxMessageConsumer
            {
                Id = notification.Id,
                Name = consumer
            },
            cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}