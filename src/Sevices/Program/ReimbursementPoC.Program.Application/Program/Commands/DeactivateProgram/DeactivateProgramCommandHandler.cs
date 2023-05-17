using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Administration.Domain.Product.Specification;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Program.Domain;

namespace ReimbursementPoC.Program.Application.Program.Commands.DeactivateProgram
{
    public class DeactivateProgramCommandHandler : IRequestHandler<DeactivateProgramCommand, ProgramDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public DeactivateProgramCommandHandler(IApplicationDbContext applicationDbContext, 
                                              IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ProgramDto> Handle(DeactivateProgramCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Programs.FirstOrDefaultAsync(new ProgramByIdSpecification(command.Id).ToExpression());


            if (entity == null)
            {
                throw new ProgramNotFoundException($"Program with id {command.Id} doesn't exist.");
            }

            if (command.LastModified != entity.LastModified)
            {
                throw new ProgramConcurrentUpdateException($"Program {command.Id} version is outdated.");
            }

            entity.DeActivate();

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProgramDto>(entity);

            return dto;
        }
    }
}
