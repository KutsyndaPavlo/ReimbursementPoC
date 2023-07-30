using ReimbursementPoC.Administration.Domain.Common;
using System.Linq.Expressions;

namespace ReimbursementPoC.Administration.Domain.Service.Specifications
{
    public class ServiceNameEqualsSpecification : Specification<ServiceEntity>
    {
        private readonly string _name;

        public ServiceNameEqualsSpecification(string name)
        {
            _name = name;
        }

        public override Expression<Func<ServiceEntity, bool>> ToExpression()
        {
            return (item) => (item.Name == _name);
        }
    }
}
