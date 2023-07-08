using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Vendor.Domain.Vendor;

namespace ReimbursementPoC.Vendor.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<VendorSubmissionEntity> VendorSubmissions { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
