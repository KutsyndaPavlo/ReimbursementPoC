﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReimbursementPoC.Administration.Application.Common.Interfaces;
using ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Administration.Domain;
using ReimbursementPoC.Administration.Domain.Program.Specification;

namespace ReimbursementPoC.Administration.Application.Program.Commands.DeactivateProgram
{
    public class CancelProgramCommandHandler : IRequestHandler<CancelProgramCommand, ProgramDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public CancelProgramCommandHandler(IApplicationDbContext applicationDbContext, 
                                              IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<ProgramDto> Handle(CancelProgramCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Programs.FirstOrDefaultAsync(new ProgramByIdSpecification(command.Id).ToExpression());


            if (entity == null)
            {
                throw new ProgramNotFoundException($"Program with id {command.Id} doesn't exist.");
            }

            entity.Cancel();

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<ProgramDto>(entity);

            return dto;
        }
    }
}
