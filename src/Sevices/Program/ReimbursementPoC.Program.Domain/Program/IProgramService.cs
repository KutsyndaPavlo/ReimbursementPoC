namespace ReimbursementPoC.Program.Domain.Program
{
    public interface IProgramService
    {
        bool IsUniqueName(string name);

        //IEnumerable<ProposalEntity> LatestProposals(ProgramEntity product);

        //IEnumerable<ProposalEntity> HistoricalProposals(ProgramEntity product, int offset = 0, int limit = 1);
    }
}
