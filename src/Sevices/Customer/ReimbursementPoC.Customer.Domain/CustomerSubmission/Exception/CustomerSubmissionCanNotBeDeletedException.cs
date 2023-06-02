namespace ReimbursementPoC.Customer.Domain.Product
{
    public class CustomerSubmissionCanNotBeDeletedException : Exception
    {
        public CustomerSubmissionCanNotBeDeletedException()
        { }

        public CustomerSubmissionCanNotBeDeletedException(string message)
            : base(message)
        { }

        public CustomerSubmissionCanNotBeDeletedException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
