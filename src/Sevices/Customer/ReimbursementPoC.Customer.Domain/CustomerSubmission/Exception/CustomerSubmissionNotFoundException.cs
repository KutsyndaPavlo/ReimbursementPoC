namespace ReimbursementPoC.Customer.Domain
{
    public class CustomerSubmissionNotFoundException : Exception
    {
        public CustomerSubmissionNotFoundException()
        { }

        public CustomerSubmissionNotFoundException(string message)
            : base(message)
        { }

        public CustomerSubmissionNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

