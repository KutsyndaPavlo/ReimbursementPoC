using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Program;
using ReimbursementPoC.Administration.Domain.Program.Enums;
using ReimbursementPoC.Administration.Domain.Service;
using System.Reflection;

namespace ReimbursementPoC.Administration.Infrastructure.Persistence
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

        public DbSet<ProgramEntity> Programs => Set<ProgramEntity>();
        public DbSet<ServiceEntity> Services => Set<ServiceEntity>();

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

            Seed(builder);

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

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured && !string.IsNullOrWhiteSpace(_connectionString))
        //    {
        //        optionsBuilder.UseSqlServer(
        //            _connectionString,
        //            x => x.MigrationsHistoryTable("__MigrationHistory", "efcore_3_0"));
        //    }
        //}

        private void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StateType>().HasData(
                StateType.Alaska,
                StateType.Alabama);
        }
    }
}
