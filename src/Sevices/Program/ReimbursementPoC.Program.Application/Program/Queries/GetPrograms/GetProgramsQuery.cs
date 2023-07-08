using MediatR;
using ReimbursementPoC.Program.Application.Common.Model;
using ReimbursementPoC.Program.Application.Program.Queries.GetProgramById;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Program.Application.Program.Queries.GetPrograms
{
    public class GetProgramsQuery : IRequest<PaginatedList<ProgramDto>>
    {

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Limit { get; set; }


        [DataMember]
        public int Offset { get; set; }

        public GetProgramsQuery()
        {

        }

        public GetProgramsQuery(string name, int offset, int limit)
        {
            Name = name;
            Offset = offset;
            Limit = limit;
        }
    }
}
