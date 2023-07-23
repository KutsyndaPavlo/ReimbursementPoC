using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Domain.Service.Exeption;
using ReimbursementPoC.Administration.Domain.Service.Specifications;

namespace ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById
{
    public class GetServiceByIdQueryHandler
        : IRequestHandler<GetServiceByIdQuery, ServiceDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetServiceByIdQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ServiceDto> Handle(GetServiceByIdQuery query, CancellationToken cancellationToken)
        {
            //ToDo add specification
            var service = await _applicationDbContext.Services.Include(x => x.Program).Where(x=> !x.IsCanceled && !x.Program.IsCanceled).FirstOrDefaultAsync(new ServiceByIdSpecification(query.Id).ToExpression());

            if (service == null)
            {
                throw new ServiceNotFoundException($"Service with id {query.Id} doesn't exist");
            }

            return _mapper.Map<ServiceDto>(service);
        }
    }
}
