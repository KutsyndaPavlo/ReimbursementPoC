namespace ReimbursementPoC.Program.Domain.Product
{
    public class ProgramCanNotBeDeletedException : Exception
    {
        public ProgramCanNotBeDeletedException()
        { }

        public ProgramCanNotBeDeletedException(string message)
            : base(message)
        { }

        public ProgramCanNotBeDeletedException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
