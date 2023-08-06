namespace ReimbursementPoC.Administration.Domain
{
    public class ProgramConcurrentUpdateException : Exception
    {
        public ProgramConcurrentUpdateException()
        { }

        public ProgramConcurrentUpdateException(string message)
            : base(message)
        { }

        public ProgramConcurrentUpdateException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
