using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Vendor.Application.Common.Interfaces;
using ReimbursementPoC.Vendor.Application.Vendor.Queries.GetVendorById;
using ReimbursementPoC.Vendor.Domain.Vendor;
using ReimbursementPoC.Vendor.Domain.VendorSubmission.DomainServices;
using ReimbursementPoC.Vendor.Domain.VendorSubmission.Specification;

namespace ReimbursementPoC.Vendor.Application.Vendor.Commands.CreateVendor
{
    public class CreateVendorSubmissionCommandHandler : IRequestHandler<CreateVendorSubmissionCommand, VendorSubmissionDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IVendorSubmissionService _vendorSubmissionService;

        public CreateVendorSubmissionCommandHandler(IApplicationDbContext applicationDbContext,
                                                    IMapper mapper,
                                                    IVendorSubmissionService vendorSubmissionService)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _vendorSubmissionService = vendorSubmissionService;
        }

        public async Task<VendorSubmissionDto> Handle(CreateVendorSubmissionCommand command, CancellationToken cancellationToken)
        {
            var entity = VendorSubmissionEntity.CreateNew(
                command.VendorId,
                command.VendorName,
                command.ServiceId,
                command.ServiceFullName,
                command.Description,
                _vendorSubmissionService);

            _applicationDbContext.VendorSubmissions.Add(entity);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var result = await _applicationDbContext
                .VendorSubmissions
                .FirstOrDefaultAsync(new VendorSubmissionByIdSpecification(entity.Id).ToExpression());

            var dto = _mapper.Map<VendorSubmissionDto>(result);

            return dto;
        }
    }
}
