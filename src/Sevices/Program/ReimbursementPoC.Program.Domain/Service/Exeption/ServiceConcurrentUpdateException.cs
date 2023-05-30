namespace ReimbursementPoC.Program.Domain.Service
{
    public class ServiceConcurrentUpdateException : Exception
    {
        public ServiceConcurrentUpdateException()
        { }

        public ServiceConcurrentUpdateException(string message)
            : base(message)
        { }

        public ServiceConcurrentUpdateException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}