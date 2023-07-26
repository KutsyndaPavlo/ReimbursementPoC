namespace ReimbursementPoC.Blazor.UI.Model
{
    public class VendorSubmissionDto
    {
        public Guid Id { get; set; }

        public Guid VendorId { get; set; }

        public string VendorName { get; set; } = "Vendor name";// ToDo

        public Guid ServiceId { get; set; }

        public string ServiceFullName { get; set; }

        public string Description { get; set; }

        public bool IsCanceled { get; set; }
    }
}
