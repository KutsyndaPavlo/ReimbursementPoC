using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Program.Domain.Program;

namespace ReimbursementPoC.Program.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ProgramEntity> Programs { get; }        

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
