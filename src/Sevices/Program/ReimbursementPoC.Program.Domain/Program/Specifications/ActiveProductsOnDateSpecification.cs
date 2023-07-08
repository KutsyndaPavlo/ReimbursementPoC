//using ReimbursementPoC.Program.Domain.Common;
//using ReimbursementPoC.Program.Domain.Entities;
//using System.Linq.Expressions;

//namespace ReimbursementPoC.Program.Domain.Product.Specification
//{
//    public class ActiveProductsOnDateSpecification : Specification<ProposalEntity>
//    {
//        private readonly DateTime _date;

//        public ActiveProductsOnDateSpecification(DateTime date)
//        {
//            _date = date;
//        }

//        public override Expression<Func<ProposalEntity, bool>> ToExpression()
//        {
//            return (item) => (item.Date == _date && item.IsActive);
//        }
//    }
//}