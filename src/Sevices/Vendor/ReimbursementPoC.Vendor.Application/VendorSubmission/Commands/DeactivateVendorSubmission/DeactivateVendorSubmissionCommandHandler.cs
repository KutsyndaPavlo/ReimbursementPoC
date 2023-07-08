using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Vendor.Application.Common.Interfaces;
using ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById;
using ReimbursementPoC.Vendor.Domain;
using ReimbursementPoC.Vendor.Domain.VendorSubmission.Specification;

namespace ReimbursementPoC.Vendor.Application.Vendor.Commands.DeactivateVendor
{
    public class DeactivateVendorSubmissionCommandHandler : IRequestHandler<DeactivateVendorSubmissionCommand, VendorSubmissionDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public DeactivateVendorSubmissionCommandHandler(IApplicationDbContext applicationDbContext, 
                                              IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<VendorSubmissionDto> Handle(DeactivateVendorSubmissionCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.VendorSubmissions.FirstOrDefaultAsync(new VendorSubmissionByIdSpecification(command.Id).ToExpression());


            if (entity == null)
            {
                throw new VendorSubmissionNotFoundException($"Vendor with id {command.Id} doesn't exist.");
            }

            entity.DeActivate();

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<VendorSubmissionDto>(entity);

            return dto;
        }
    }
}
