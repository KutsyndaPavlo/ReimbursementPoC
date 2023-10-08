using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Customer.Domain.Customer;

namespace ReimbursementPoC.Customer.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<CustomerSubmissionEntity> CustomerSubmissions { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
