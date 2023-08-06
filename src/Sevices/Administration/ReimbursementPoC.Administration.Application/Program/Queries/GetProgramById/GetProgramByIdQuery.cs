using MediatR;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById
{
    public class GetProgramByIdQuery : IRequest<ProgramDto>
    {
        public GetProgramByIdQuery(Guid id)
        {
            Id = id;
        }

        [DataMember]
        public Guid Id { get; set; }
    }
}
