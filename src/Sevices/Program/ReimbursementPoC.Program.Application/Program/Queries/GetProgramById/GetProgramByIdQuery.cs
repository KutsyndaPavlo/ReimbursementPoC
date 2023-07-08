﻿using MediatR;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Program.Application.Program.Queries.GetProgramById
{
    public class GetProgramByIdQuery : IRequest<ProgramFullDto>
    {
        public GetProgramByIdQuery(Guid id)
        {
            Id = id;
        }

        [DataMember]
        public Guid Id { get; set; }
    }
}
