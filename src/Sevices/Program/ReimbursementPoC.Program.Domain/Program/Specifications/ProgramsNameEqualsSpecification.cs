using ReimbursementPoC.Program.Domain.Common;
using ReimbursementPoC.Program.Domain.Program;
using System.Linq.Expressions;

namespace ReimbursementPoC.Program.Domain.Product.Spefifications
{
    public class ProgramsNameEqualsSpecification : Specification<ProgramEntity>
    {
        private readonly string _name;

        public ProgramsNameEqualsSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<ProgramEntity, bool>> ToExpression()
        {
            return (item) => (item.Name == _name);
        }
    }
}
