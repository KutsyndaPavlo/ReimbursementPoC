﻿using MediatR;
using ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById;

namespace ReimbursementPoC.Administration.Application.Program.Commands.DeactivateProgram
{
    public class CancelProgramCommand : IRequest<ProgramDto>
    {
        public Guid Id { get; set; }
    }
}