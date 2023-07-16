using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Program;

namespace ReimbursementPoC.Administration.Domain.Product.Rules
{
    public class ProgramNameMustBeUniqueRule : IBusinessRule
    {
        private readonly IProgramService _programUniquenessChecker;

        private readonly string _name;

        public ProgramNameMustBeUniqueRule(
            IProgramService productUniquenessChecker,
            string name)
        {
            _programUniquenessChecker = productUniquenessChecker;
            _name = name;
        }

        public bool IsBroken() => !_programUniquenessChecker.IsUniqueName(_name);

        public string Message => "Program with this name already exists.";
    }
}
