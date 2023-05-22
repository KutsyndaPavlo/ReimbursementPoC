using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Administration.Domain.Product.Specification;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Program.Domain;
using ReimbursementPoC.Program.Domain.Program;

namespace ReimbursementPoC.Program.Application.Services.Commands.CreateService
{
    public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, ServiceDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IProgramService _ProgramUniquenessChecker;

        public CreateServiceCommandHandler(IApplicationDbContext applicationDbContext, 
                                           IMapper mapper, 
                                           IProgramService ProgramUniquenessChecker)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _ProgramUniquenessChecker = ProgramUniquenessChecker;
        }

        public async Task<ServiceDto> Handle(CreateServiceCommand command, CancellationToken cancellationToken)
        {
            var program = await _applicationDbContext.Programs.Include("_services").FirstOrDefaultAsync(new ProgramByIdSpecification(command.ProgramId).ToExpression());

            if (program == null)
            {
                throw new ProgramNotFoundException($"Program with id {command.ProgramId} doesn't exist");
            }

            program.AddService(command.Name, command.Description);

            _applicationDbContext.Programs.Update(program);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            //var dto = _mapper.Map<ServiceDto>(entity);

            //return dto;

            return null;
        }
    }
}
