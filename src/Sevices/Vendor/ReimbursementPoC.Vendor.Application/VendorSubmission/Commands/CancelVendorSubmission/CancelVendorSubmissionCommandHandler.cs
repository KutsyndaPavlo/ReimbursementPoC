using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Vendor.Application.Common.Interfaces;
using ReimbursementPoC.Vendor.Application.Vendor.Commands.CancelVendorSubmission;
using ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById;
using ReimbursementPoC.Vendor.Domain;
using ReimbursementPoC.Vendor.Domain.VendorSubmission.Specification;

namespace ReimbursementPoC.Vendor.Application.Vendor.Commands.DeactivateVendor
{
    public class CancelVendorSubmissionCommandHandler : IRequestHandler<CancelVendorSubmissionCommand, VendorSubmissionDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public CancelVendorSubmissionCommandHandler(IApplicationDbContext applicationDbContext, 
                                                    IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<VendorSubmissionDto> Handle(CancelVendorSubmissionCommand command, CancellationToken cancellationToken)
        {
            // ToDo check if vendor exists

            var entity = await _applicationDbContext.VendorSubmissions.FirstOrDefaultAsync(new VendorSubmissionByIdSpecification(command.SubmissionId).ToExpression());

            if (entity == null)
            {
                throw new VendorSubmissionNotFoundException($"Vendor submission with id {command.SubmissionId} doesn't exist.");
            }

            entity.Cancel();

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<VendorSubmissionDto>(entity);

            return dto;
        }
    }
}
