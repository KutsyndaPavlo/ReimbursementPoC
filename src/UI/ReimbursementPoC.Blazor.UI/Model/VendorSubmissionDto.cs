namespace ReimbursementPoC.Blazor.UI.Model
{
    public class VendorSubmissionDto
    {
        public Guid Id { get; set; }

        public Guid VendorId { get; set; }

        public Guid ServiceId { get; set; }

        public string ServiceFullName { get; set; }

        public bool IsCanceled { get; set; }
    }
}
