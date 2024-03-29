﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Program;
using ReimbursementPoC.Administration.Domain.Program.Errors;
using ReimbursementPoC.Administration.Domain.Program.Specification;

namespace ReimbursementPoC.Administration.Application.Program.Commands.UpdateProgram
{
    internal class UpdateProgramCommandHandler : IRequestHandler<UpdateProgramCommand, Result<ProgramDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;
        private readonly IProgramService _ProgramUniquenessChecker;

        public UpdateProgramCommandHandler(
            IApplicationDbContext applicationDbContext,
            IMapper mapper,
            IProgramService ProgramUniquenessChecker)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _ProgramUniquenessChecker = ProgramUniquenessChecker;
        }

        public async Task<Result<ProgramDto>> Handle(UpdateProgramCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Programs.FindAsync(new object[] { command.Id }, cancellationToken);

            if (entity == null)
            {
                return Result<ProgramDto>.Failure(ProgramErrors.NotFound(command.Id));
            }

            if (command.LastModified.Ticks != entity.LastModified.Ticks)
            {
                return Result<ProgramDto>.Failure(ProgramErrors.ConcurrentUpdate(command.Id));
            }

            entity.UpdateProgram(
                command.Name,
                command.Description);

            _applicationDbContext.Programs.Update(entity);
            //return await _demoContext.Countries
            //                    .Where(x =>
            //                    x.Id == countryEntity.Id)
            //                    .ExecuteUpdateAsync(s =>
            //                            s.SetProperty(p => p.Description, countryEntity.Description)
            //                            .SetProperty(p => p.FlagUri, countryEntity.FlagUri)
            //                            .SetProperty(p => p.Name, countryEntity.Name));

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var result = await _applicationDbContext
                .Programs
                .Include(x => x.State)
                .FirstOrDefaultAsync(new ProgramByIdSpecification(entity.Id).ToExpression());

            var dto = _mapper.Map<ProgramDto>(result);

            return Result<ProgramDto>.Success(dto);
        }
    }
}
