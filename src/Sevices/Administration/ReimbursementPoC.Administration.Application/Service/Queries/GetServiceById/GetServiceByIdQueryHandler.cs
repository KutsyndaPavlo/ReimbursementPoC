using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Service.Errors;
using ReimbursementPoC.Administration.Domain.Service.Specifications;

namespace ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById
{
    public class GetServiceByIdQueryHandler
        : IRequestHandler<GetServiceByIdQuery, Result<ServiceDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetServiceByIdQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<Result<ServiceDto>> Handle(GetServiceByIdQuery query, CancellationToken cancellationToken)
        {
            //ToDo add specification
            var service = await _applicationDbContext.Services.Include(x => x.Program).FirstOrDefaultAsync(new ServiceByIdSpecification(query.Id).ToExpression());

            if (service == null)
            {
                Result<ServiceDto>.Failure(ServiceErrors.NotFound(query.Id));
            }

            return Result<ServiceDto>.Success(_mapper.Map<ServiceDto>(service));
        }
    }
}
