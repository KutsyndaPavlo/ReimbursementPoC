﻿using ReimbursementPoC.Administration.Domain.Common;
using System.Linq.Expressions;

namespace ReimbursementPoC.Administration.Domain.Service.Specifications
{
    public class ServiceByIdSpecification : Specification<ServiceEntity>
    {
        private readonly Guid _id;

        public ServiceByIdSpecification(Guid id)
        {
            _id = id;
        }

        public override Expression<Func<ServiceEntity, bool>> ToExpression()
        {
            return (item) => (item.Id == _id);
        }
    }
}
