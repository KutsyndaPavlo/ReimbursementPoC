using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Vendor.Application.Common.Interfaces;
using ReimbursementPoC.Vendor.Domain.Common;
using ReimbursementPoC.Vendor.Domain.Vendor;
using System.Reflection;

namespace ReimbursementPoC.Vendor.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly IDomainEventService _domainEventService;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IDomainEventService domainEventService) : base(options)
        {
            _domainEventService = domainEventService;
            Database.EnsureCreated();
        }

        public DbSet<VendorSubmissionEntity> VendorSubmissions => Set<VendorSubmissionEntity>();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var events = ChangeTracker.Entries<BaseEntity>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .ToArray();

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents(events);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private async Task DispatchEvents(DomainEvent[] events)
        {
            foreach (var @event in events)
            {
                @event.IsPublished = true;
                await _domainEventService.Publish(@event);
            }
        }
    }
}
