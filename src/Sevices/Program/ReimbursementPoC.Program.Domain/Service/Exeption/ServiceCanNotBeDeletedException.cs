namespace ReimbursementPoC.Program.Domain.Service.Exeption
{
    public class ServiceCanNotBeDeletedException : Exception
    {
        public ServiceCanNotBeDeletedException()
        { }

        public ServiceCanNotBeDeletedException(string message)
            : base(message)
        { }

        public ServiceCanNotBeDeletedException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
