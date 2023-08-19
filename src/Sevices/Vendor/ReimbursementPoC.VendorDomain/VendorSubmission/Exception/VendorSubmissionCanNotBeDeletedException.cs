namespace ReimbursementPoC.Vendor.Domain.Product
{
    public class VendorSubmissionCanNotBeDeletedException : Exception
    {
        public VendorSubmissionCanNotBeDeletedException()
        { }

        public VendorSubmissionCanNotBeDeletedException(string message)
            : base(message)
        { }

        public VendorSubmissionCanNotBeDeletedException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
