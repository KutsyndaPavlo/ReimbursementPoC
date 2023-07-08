using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Program.Domain.Program;
using ReimbursementPoC.Program.Domain.Service;

namespace ReimbursementPoC.Program.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ProgramEntity> Programs { get; }

        DbSet<ServiceEntity> Services { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
