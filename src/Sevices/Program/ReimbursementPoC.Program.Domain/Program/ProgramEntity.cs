using ReimbursementPoC.Program.Domain.Common;
using ReimbursementPoC.Program.Domain.Product.Rules;
using ReimbursementPoC.Program.Domain.Program.Enums;
using ReimbursementPoC.Program.Domain.ValueObjects;

namespace ReimbursementPoC.Program.Domain.Program
{
    public class ProgramEntity : BaseEntity, IAggregateRoot
    {
        private ProgramEntity(string name, string description, Period period, StateType state) : base()
        {
            this.Name = name;
            this.Description = description;
            this.State = state;
            this.IsActive = true;

            //this._domainEvents.Add(new ProductCreatedEvent(this));
        }

        public string Name { get; private set; }

        public string? Description { get; private set; }

        public Period Period { get; private set; }

        public StateType State { get; private set; }

        public bool IsActive { get; private set; }

        public static ProgramEntity CreateNew(string name,
                                              string description,
                                              DateTime startDate,
                                              DateTime endDate,
                                              string state,
                                              IProgramService productUniquenessChecker)
        {
            CheckRule(new ProgramNameMustBeUniqueRule(productUniquenessChecker, name));

            return new ProgramEntity(name, description, new Period(startDate, endDate), StateType.FromDisplayName<StateType>(state));
        }

        public void UpdateProgram(
            string name,
            string? code,
            string? description,
            IProgramService productService)
        {
            //CheckRule(new ProductNameMustBeUniqueRule(productService, name));

            //this.Name = name;
            //this.Code = code;
            //this.Description = description;
            //this.LastModified = DateTime.UtcNow;

            //this._domainEvents.Add(new ProductUpdatedEvent(this));
        }

        public void DeActivate()
        {
            //IsActive = false;
            //this.LastModified = DateTime.UtcNow;
            //this._domainEvents.Add(new ProductDeactivatedEvent(this));
        }

        public bool CanBeDeleted(IProgramService productService)
        {
            return true;
            //return !productService.HistoricalProposals(this).Any();
        }

        //public IReadOnlyCollection<ProposalEntity?> ActiveProposals(IProgramService productService)
        //{
        //    return productService.LatestProposals(this).ToList().AsReadOnly();
        //}
    }
}