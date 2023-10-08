namespace ReimbursementPoC.Administration.Domain.Common
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
