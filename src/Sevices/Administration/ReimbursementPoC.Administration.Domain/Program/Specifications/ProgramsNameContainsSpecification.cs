using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Program;
using System.Linq.Expressions;

namespace ReimbursementPoC.Administration.Domain.Product.Spefifications
{
    public class ProgramsNameContainsSpecification : Specification<ProgramEntity>
    {
        private readonly string _name;

        public ProgramsNameContainsSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<ProgramEntity, bool>> ToExpression()
        {
            return (item) => (item.Name.Contains(_name));
        }
    }
}
