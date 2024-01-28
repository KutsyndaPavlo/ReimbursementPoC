namespace ReimbursementPoC.Vendor.Domain
{
    public class VendorSubmissionNotFoundException : Exception
    {
        public VendorSubmissionNotFoundException()
        { }

        public VendorSubmissionNotFoundException(string message)
            : base(message)
        { }

        public VendorSubmissionNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}

