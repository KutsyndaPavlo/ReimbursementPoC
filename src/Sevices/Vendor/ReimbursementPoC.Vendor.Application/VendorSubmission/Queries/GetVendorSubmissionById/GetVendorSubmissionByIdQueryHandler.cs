using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Vendor.Application.Common.Interfaces;
using ReimbursementPoC.Vendor.Domain;
using ReimbursementPoC.Vendor.Domain.VendorSubmission.Specification;

namespace ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById
{
    public class GetVendorSubmissionByIdQueryHandler
        : IRequestHandler<GetVendorSubmissionByIdQuery, VendorSubmissionDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetVendorSubmissionByIdQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<VendorSubmissionDto> Handle(GetVendorSubmissionByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext
                .VendorSubmissions
                .FirstOrDefaultAsync(new VendorSubmissionByIdSpecification(query.Id).ToExpression());

            if (entity == null)
            {
                throw new VendorSubmissionNotFoundException($"Vendor with id {query.Id} doesn't exist");
            }

            return _mapper.Map<VendorSubmissionDto>(entity);
        }
    }
}
