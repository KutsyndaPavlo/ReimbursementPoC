using ReimbursementPoC.Program.Domain.Common;
using ReimbursementPoC.Program.Domain.Product.Rules;
using ReimbursementPoC.Program.Domain.Program.Enums;
using ReimbursementPoC.Program.Domain.Program.Events;
using ReimbursementPoC.Program.Domain.Service;
using ReimbursementPoC.Program.Domain.Service.Events;
using ReimbursementPoC.Program.Domain.ValueObjects;

namespace ReimbursementPoC.Program.Domain.Program
{
    public class ProgramEntity : BaseEntity, IAggregateRoot
    {
        private ProgramEntity()
        {

        }

        private ProgramEntity(string name, string description, int stateId, Period period): base()
        {
            this.Name = name;
            this.Description = description;
            this._stateId = stateId;
            this.Period = period;
            this.IsActive = true;

            this._domainEvents.Add(new ProgramCreatedEvent(this));
        }

        public string Name { get; private set; }

        public string? Description { get; private set; }

        public Period Period { get; private set; }

        public StateType State { get; private set; }

        private int _stateId;

        public bool IsActive { get; private set; }

        public List<ServiceEntity> _services;

        public IReadOnlyCollection<ServiceEntity> Services => _services;

        public static ProgramEntity CreateNew(string name,
                                              string description,
                                              int stateId,
                                              DateTime startDate,
                                              DateTime endDate,
                                              IProgramService programUniquenessChecker)
        {
            CheckRule(new ProgramNameMustBeUniqueRule(programUniquenessChecker, name));

            return new ProgramEntity(name, description, stateId, new Period(startDate, endDate));
        }

        public void UpdateProgram(
            string name,
            string? description,
            int stateId,
            DateTime startDate,
            DateTime endDate,
            IProgramService programService)
        {
            //CheckRule(new ProgramNameMustBeUniqueRule(programService, name));

            this.Name = name;
            this.Description = description;
            this._stateId = _stateId;
            this.Period = new Period(startDate, endDate);
            this.LastModified = DateTime.UtcNow;

            this._domainEvents.Add(new ProgramUpdatedEvent(this));
        }

        public void DeActivate()
        {
            IsActive = false;
            this.LastModified = DateTime.UtcNow;
            this._domainEvents.Add(new ProgramDeactivatedEvent(this));
        }

        public bool CanBeDeleted(IProgramService productService)
        {
            return true;
            //return !productService.HistoricalProposals(this).Any();
        }

        public ServiceEntity CreateService(string name, string? description)
        {
            //CheckRule(new OnlyActiveSellerCanProvidePoposalRule(seller));
            //CheckRule(new ProposalShouldBeProvidedToActiveProductRule(product));
            //CheckRule(new ProposalPriceMustBeGreaterThanZeroRule(price));
            //CheckRule(new CurrencyOfProposalPriceMustBeUahOrUsd(currency));

            var service = ServiceEntity.CreateNew(name, description, this);

            //CheckRule(new ShouldBeOnlyOneProposalPerDayRule(proposalService, entity));

            service.AddDomainEvent(new ServiceCreatedEvent(service));

            return service;
        }
    }
}