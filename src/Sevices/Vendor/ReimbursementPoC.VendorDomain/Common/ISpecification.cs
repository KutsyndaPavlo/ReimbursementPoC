namespace ReimbursementPoC.Vendor.Domain.Common
{
    interface ISpecification<T>
    {
        CheckResult IsSatisfiedBy(T candidate);
    }

    class CheckResult
    {
        public bool IsSatisfied { get; }
        public string FailureReason { get; }
    }
}
