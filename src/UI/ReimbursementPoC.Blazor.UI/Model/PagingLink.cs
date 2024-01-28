namespace ReimbursementPoC.Blazor.UI.Model
{
    public class PagingLink
    {
        public string Text { get; set; }
        public int Page { get; set; }
        public int Offset { get; set; }
        public bool Active { get; set; }
    }
}
