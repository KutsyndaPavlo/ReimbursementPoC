using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Domain.ValueObjects
{
    public class Period : ValueObject
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Period() { }

        public Period(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return StartDate;
            yield return EndDate;
        }
    }
}
