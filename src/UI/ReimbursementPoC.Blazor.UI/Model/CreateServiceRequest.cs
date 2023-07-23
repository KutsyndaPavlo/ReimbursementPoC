namespace ReimbursementPoC.Blazor.UI.Model
{
    public class CreateServiceRequest
    {
        public Guid ProgramId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }
    }
}
