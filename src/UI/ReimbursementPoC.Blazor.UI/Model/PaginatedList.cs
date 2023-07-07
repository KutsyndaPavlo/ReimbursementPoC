namespace ReimbursementPoC.Blazor.UI.Model
{
    public class PaginatedList<T>
    {
        public IEnumerable<T> Items { get; set; }

        public Page Page { get; set; }
    }

    public class Page
    {
        public int Offset { get; set; }

        public int Limit { get; set; }

        public int Count { get; set; }

        public long Total { get; set; }

    }

    public class VendorSubmissionDto
    {
        public Guid Id { get; set; }

        public Guid VendorId { get; set; }

        public Guid ServiceId { get; set; }
    }

    public class CreateVendorSubmissionRequest
    {
        public Guid VendorId { get; set; }

        public Guid ServiceId { get; set; }
    }
}
