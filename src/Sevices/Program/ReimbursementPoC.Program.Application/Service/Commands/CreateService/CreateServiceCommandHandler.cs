using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Program.Domain;
using ReimbursementPoC.Program.Domain.Program.Specification;

namespace ReimbursementPoC.Program.Application.Services.Commands.CreateService
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, ServiceDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public CreateServiceCommandHandler(IApplicationDbContext applicationDbContext, 
                                           IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ServiceDto> Handle(CreateServiceCommand command, CancellationToken cancellationToken)
        {
            var program = await _applicationDbContext.Programs.Include("_services").FirstOrDefaultAsync(new ProgramByIdSpecification(command.ProgramId).ToExpression());

            if (program == null)
            {
                throw new ProgramNotFoundException($"Program with id {command.ProgramId} doesn't exist");
            }

            var service = program.CreateService(command.Name, command.Description);

            await _applicationDbContext.Services.AddAsync(service);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ServiceDto>(service);
        }
    }
}
