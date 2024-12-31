using Contracts.Common.Events;
using Contracts.Common.Interfaces;
using Infrastructure.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Infrastructure.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task DispatchDomainEventAsync(this IMediator mediator, List<BaseEvent> domainEvents, ILogger logger)
        {
            //var domainEntities = context.ChangeTracker.Entries<IEventEntity>()
            //    .Select(e => e.Entity)
            //    .Where(e => e.DomainEvents().Any())
            //    .ToList();

            //var domainEvents = domainEntities
            //    .SelectMany(e => e.DomainEvents())
            //    .ToList();

            //domainEntities.ForEach(e => e.ClearDomainEvent());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
                var data = new SerializeService().Serialize(domainEvent);
                logger.Information($"A domain event has been published!\nEvent: {domainEvent.GetType().Name}\nData: {data}");
            }
        }
    }
}
