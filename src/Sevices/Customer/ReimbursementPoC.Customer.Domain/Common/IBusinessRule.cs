namespace ReimbursementPoC.Customer.Domain.Common
{
    public interface IBusinessRule
    {
        bool IsBroken();

        string Message { get; }
    }
}
