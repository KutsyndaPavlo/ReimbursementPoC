using AutoMapper;
using MediatR;
using ReimbursementPoC.Program.Application.Common.Interfaces;
using ReimbursementPoC.Program.Application.Services.Queries.GetServiceById;
using ReimbursementPoC.Program.Domain;
using ReimbursementPoC.Program.Domain.Program;

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
            var entity = await _applicationDbContext.Programs.FindAsync(new object[] { command.Id }, cancellationToken);

            if (entity == null)
            {
                throw new ProgramNotFoundException($"Program with id {command.Id} doesn't exist.");
            }

            if (command.LastModified != entity.LastModified)
            {
                throw new ProgramConcurrentUpdateException($"Program {command.Id} version is outdated.");
            }

            entity.UpdateProgram(
                command.Name,
                command.Description,
                command.State,
                command.StartDate,
                command.EndDate,
                _ProgramUniquenessChecker);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ServiceDto>(entity);

            return dto;
        }
    }
}
