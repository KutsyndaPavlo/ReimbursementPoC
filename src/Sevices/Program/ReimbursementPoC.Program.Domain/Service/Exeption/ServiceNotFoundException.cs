namespace ReimbursementPoC.Program.Domain.Service.Exeption
{
    public class ServiceNotFoundException : Exception
    {
        public ServiceNotFoundException()
        { }

        public ServiceNotFoundException(string message)
            : base(message)
        { }

        public ServiceNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
