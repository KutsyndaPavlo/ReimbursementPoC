namespace ReimbursementPoC.Administration.Domain.Program
{
    public interface IProgramService
    {
        bool IsSinglePerStatePerPeriod(
            int stateId,
            DateTime startDate,
            DateTime endDate);

        //IEnumerable<ProposalEntity> LatestProposals(ProgramEntity product);

        //IEnumerable<ProposalEntity> HistoricalProposals(ProgramEntity product, int offset = 0, int limit = 1);
    }
}
