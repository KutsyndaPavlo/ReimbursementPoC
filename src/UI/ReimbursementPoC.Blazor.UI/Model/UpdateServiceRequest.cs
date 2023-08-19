namespace ReimbursementPoC.Blazor.UI.Model
{
    public class UpdateServiceRequest
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public DateTime LastModified { get; set; }
    }
}
