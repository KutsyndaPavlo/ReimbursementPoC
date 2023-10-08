using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Domain.Program.Rules
{
    public class ProgramMustBeSinglePerStatePerPeriodRule : IBusinessRule
    {
        private readonly IProgramService _programService;
        private readonly int _stateId;
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;

        public ProgramMustBeSinglePerStatePerPeriodRule(
            IProgramService productUniquenessChecker,
            int stateId,
            DateTime startDate,
            DateTime endDate)
        {
            _programService = productUniquenessChecker;
            _stateId = stateId;
            _startDate = startDate;
            _endDate = endDate;
        }

        public bool IsBroken() => !_programService.IsSinglePerStatePerPeriod(_stateId, _startDate, _endDate);

        public string Message => "Program already exists.";
    }
}
