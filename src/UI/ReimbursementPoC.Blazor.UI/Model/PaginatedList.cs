namespace ReimbursementPoC.Blazor.UI.Model
{
    public class PaginatedList<T>
    {
        public IEnumerable<T> Items { get; set; }

        public Page Page { get; set; }
    }
}
