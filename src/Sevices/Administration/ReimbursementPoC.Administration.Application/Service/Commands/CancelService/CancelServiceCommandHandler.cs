using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Administration.Domain.Service.Exeption;
using ReimbursementPoC.Administration.Domain.Service.Specifications;

namespace ReimbursementPoC.Administration.Application.Services.Commands.DeactivateService
{
    public class CancelServiceCommandHandler : IRequestHandler<CancelServiceCommand, ServiceDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public CancelServiceCommandHandler(IApplicationDbContext applicationDbContext,
                                              IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ServiceDto> Handle(CancelServiceCommand command, CancellationToken cancellationToken)
        {

            var service = await _applicationDbContext.Services.FirstOrDefaultAsync(new ServiceByIdSpecification(command.Id).ToExpression());

            if (service == null)
            {
                throw new ServiceNotFoundException($"Service with id {command.Id} doesn't exist");
            }

            service.Cancel();

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
