using Contracts.Common.Events;
using Contracts.Common.Interfaces;
using Contracts.Domains.Interfaces;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Entities;
using Serilog;
using System.Reflection;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderDbContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private List<BaseEvent> _baseEvents;

        public OrderDbContext(DbContextOptions<OrderDbContext> options, IMediator mediator, ILogger logger) : base(options)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public DbSet<Order> Orders { get; set; }

        private void SetBaseEventsBeforeSaveChanges()
        {
            var domainEntities = ChangeTracker.Entries<IEventEntity>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents().Any())
                .ToList();

            _baseEvents = domainEntities
                .SelectMany(e => e.DomainEvents())
                .ToList();

            domainEntities.ForEach(e => e.ClearDomainEvent());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetBaseEventsBeforeSaveChanges();

            var modified = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified ||
                    e.State == EntityState.Added ||
                    e.State == EntityState.Deleted);

            foreach (var item in modified)
            {
                switch (item.State)
                {
                    case EntityState.Added:
                        if (item.Entity is IDateTracking addedEntity)
                        {
                            addedEntity.CreatedDate = DateTime.UtcNow;
                            item.State = EntityState.Added;
                        }
                        break;
                    case EntityState.Modified:
                        Entry(item.Entity).Property("Id").IsModified = false;
                        if (item.Entity is IDateTracking modifiedEntity)
                        {
                            modifiedEntity.LastModifiedDate = DateTime.UtcNow;
                            item.State = EntityState.Modified;
                        }
                        break;
                    default:
                        break;
                }
            }

            var result = base.SaveChangesAsync(cancellationToken);
            _mediator.DispatchDomainEventAsync(_baseEvents, _logger);

            return result;
        }
    }
}
