using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Program.Domain;
using ReimbursementPoC.Program.Domain.Program.Specification;
using ReimbursementPoC.Program.Domain.Service.Exeption;
using ReimbursementPoC.Program.Domain.Service.Specifications;

namespace ReimbursementPoC.Program.Application.Services.Commands.DeactivateService
{
    public class DeactivateServiceCommandHandler : IRequestHandler<DeactivateServiceCommand, ServiceDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public DeactivateServiceCommandHandler(IApplicationDbContext applicationDbContext,
                                              IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ServiceDto> Handle(DeactivateServiceCommand command, CancellationToken cancellationToken)
        {

            var service = await _applicationDbContext.Services.FirstOrDefaultAsync(new ServiceByIdSpecification(command.Id).ToExpression());

            if (service == null)
            {
                throw new ServiceNotFoundException($"Program with id {command.Id} doesn't exist");
            }

            service.Deactivate();

            //if (!entity.CanBeDeleted(_ProgramService))
            //{
            //    throw new ProgramCanNotBeDeletedException($"Program with id {command.Id} can't be deleted");
            //}

            _applicationDbContext.Services.Update(service);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ServiceDto>(service);

            return dto;
        }
    }
}
