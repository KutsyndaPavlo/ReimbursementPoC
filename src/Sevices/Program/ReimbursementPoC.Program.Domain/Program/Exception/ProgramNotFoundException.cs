namespace ReimbursementPoC.Program.Domain
{
    public class ProgramNotFoundException : Exception
    {
        public ProgramNotFoundException()
        { }

        public ProgramNotFoundException(string message)
            : base(message)
        { }

        public ProgramNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

