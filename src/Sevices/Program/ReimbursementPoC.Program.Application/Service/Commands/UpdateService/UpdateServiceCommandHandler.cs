using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Program.Domain;
using ReimbursementPoC.Program.Domain.Program;
using ReimbursementPoC.Program.Domain.Service;
using ReimbursementPoC.Program.Domain.Service.Exeption;
using ReimbursementPoC.Program.Domain.Service.Specifications;

namespace ReimbursementPoC.Program.Application.Services.Commands.UpdateService
{
    internal class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, ServiceDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IProgramService _ProgramUniquenessChecker;

        public UpdateServiceCommandHandler(
            IApplicationDbContext applicationDbContext,
            IMapper mapper,
            IProgramService ProgramUniquenessChecker)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _ProgramUniquenessChecker = ProgramUniquenessChecker;
        }

        public async Task<ServiceDto> Handle(UpdateServiceCommand command, CancellationToken cancellationToken)
        {
            var service = await _applicationDbContext.Services.FirstOrDefaultAsync(new ServiceByIdSpecification(command.Id).ToExpression());

            if (service == null)
            {
                throw new ServiceNotFoundException($"Service with id {command.Id} doesn't exist");
            }

            if (command.LastModified.Ticks != service.LastModified.Ticks)
            {
                throw new ServiceConcurrentUpdateException($"Service {command.Id} version is outdated.");
            }

            service.Update(
                command.Name,
                command.Description);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ServiceDto>(service);

            return dto;
        }
    }
}
