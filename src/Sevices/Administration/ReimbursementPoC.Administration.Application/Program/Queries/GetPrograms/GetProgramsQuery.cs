using MediatR;
using ReimbursementPoC.Administration.Application.Common.Model;
using ReimbursementPoC.Administration.Application.Program.Queries.GetProgramById;
using ReimbursementPoC.Administration.Domain.Common;
using System.Runtime.Serialization;

namespace ReimbursementPoC.Administration.Application.Program.Queries.GetPrograms
{
    public class GetProgramsQuery : IRequest<Result<PaginatedList<ProgramDto>>>
    {

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Limit { get; set; }

        [DataMember]
        public int Offset { get; set; }

        [DataMember]
        public string Sort { get; set; }

        public GetProgramsQuery()
        {

        }

        public GetProgramsQuery(string name, int offset, int limit, string sort)
        {
            Name = name;
            Offset = offset;
            Limit = limit;
            Sort = sort;
        }
    }
}
