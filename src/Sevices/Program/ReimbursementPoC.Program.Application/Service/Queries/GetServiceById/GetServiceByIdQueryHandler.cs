using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Administration.Domain.Product.Specification;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Domain;

namespace ReimbursementPoC.Program.Application.Services.Queries.GetServiceById
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
            var entity = await _applicationDbContext.Programs.FirstOrDefaultAsync(new ProgramByIdSpecification(query.Id).ToExpression());

            if (entity == null)
            {
                throw new ProgramNotFoundException($"Program with id {query.Id} doesn't exist");
            }

            return _mapper.Map<ServiceDto>(entity);
        }
    }
}
