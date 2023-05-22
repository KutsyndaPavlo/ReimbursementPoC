using ReimbursementPoC.Program.Domain.Common;
using ReimbursementPoC.Program.Domain.Product.Events;
using ReimbursementPoC.Program.Domain.Product.Rules;
using ReimbursementPoC.Program.Domain.Program.Enums;
using ReimbursementPoC.Program.Domain.Service;
using ReimbursementPoC.Program.Domain.ValueObjects;

namespace ReimbursementPoC.Program.Domain.Program
{
    public class ProgramEntity : BaseEntity, IAggregateRoot
    {
        private ProgramEntity()
        {

        }

        private ProgramEntity(string name, string description, Period period, StateType state) : base()
        {
            this.Name = name;
            this.Description = description;
            this.State = state;
            this.Period = period;
            this.IsActive = true;

            this._domainEvents.Add(new ProgramCreatedEvent(this));
        }

        public string Name { get; private set; }

        public string? Description { get; private set; }

        public Period Period { get; private set; }

        public StateType State { get; private set; }

        public bool IsActive { get; private set; }

        public List<ServiceEntity> _services;

        public IReadOnlyCollection<ServiceEntity> Services => _services;

        public static ProgramEntity CreateNew(string name,
                                              string description,
                                              DateTime startDate,
                                              DateTime endDate,
                                              string state,
                                              IProgramService programUniquenessChecker)
        {
            CheckRule(new ProgramNameMustBeUniqueRule(programUniquenessChecker, name));

            return new ProgramEntity(name, description, new Period(startDate, endDate), StateType.FromDisplayName<StateType>(state));
        }

        public void UpdateProgram(
            string name,
            string? description,
            string state,
            DateTime startDate,
            DateTime endDate,
            IProgramService programService)
        {
            CheckRule(new ProgramNameMustBeUniqueRule(programService, name));

            this.Name = name;
            this.Description = description;
            this.State = StateType.FromDisplayName<StateType>(state);
            this.Period = new Period(startDate, endDate);
            this.LastModified = DateTime.UtcNow;

            //this._domainEvents.Add(new ProductUpdatedEvent(this));
        }

        public void DeActivate()
        {
            IsActive = false;
            this.LastModified = DateTime.UtcNow;
            //this._domainEvents.Add(new ProductDeactivatedEvent(this));
        }

        public bool CanBeDeleted(IProgramService productService)
        {
            return true;
            //return !productService.HistoricalProposals(this).Any();
        }

        public ServiceEntity AddService(string name, string? description)
        {
            //CheckRule(new OnlyActiveSellerCanProvidePoposalRule(seller));
            //CheckRule(new ProposalShouldBeProvidedToActiveProductRule(product));
            //CheckRule(new ProposalPriceMustBeGreaterThanZeroRule(price));
            //CheckRule(new CurrencyOfProposalPriceMustBeUahOrUsd(currency));

            var service = ServiceEntity.CreateNew(name, description, this);
            this._services.Add(service);

            //CheckRule(new ShouldBeOnlyOneProposalPerDayRule(proposalService, entity));

            //entity.AddDomainEvent(new ProposalCreatedEvent(entity, product, seller));

            return service;
        }
    }
}