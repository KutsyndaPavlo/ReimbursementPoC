using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Domain.Program;
using ReimbursementPoC.Administration.Domain.Service;

namespace ReimbursementPoC.Administration.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ProgramEntity> Programs { get; }

        DbSet<ServiceEntity> Services { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
