using ReimbursementPoC.Administration.Domain.Common;
using ReimbursementPoC.Administration.Domain.Program.Enums;
using ReimbursementPoC.Administration.Domain.Program.Events;
using ReimbursementPoC.Administration.Domain.Program.Rules;
using ReimbursementPoC.Administration.Domain.Service;
using ReimbursementPoC.Administration.Domain.Service.Events;
using ReimbursementPoC.Administration.Domain.Service.Rules;
using ReimbursementPoC.Administration.Domain.ValueObjects;

namespace ReimbursementPoC.Administration.Domain.Program
{
    /// <summary>
    /// Represents a program entity with a name, description, state and date period.
    /// </summary>
    public class ProgramEntity : BaseEntity, IAggregateRoot
    {
        private ProgramEntity()
        {
            // only for EF
        }

        /// <summary>
        /// This constructor initializes the new <see cref="ProgramEntity"/> 
        /// </summary>
        /// <param><c>name</c> The name of a program.</param>
        /// <param><c>description</c> The description of a program.</param>
        private ProgramEntity(string name, string description, int stateId, Period period): base()
        {
            this.Name = name;
            this.Description = description;
            this._stateId = stateId;
            this.Period = period;
            this.IsCanceled = false;

            this._domainEvents.Add(new ProgramCreatedEvent(this));
        }

        /// <summary>
        /// The programs's name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The programs's description.
        /// </summary>
        public string? Description { get; private set; }

        public Period Period { get; private set; }

        public StateType State { get; private set; }

        private int _stateId;

        public bool IsCanceled { get; private set; }

        public List<ServiceEntity> _services;

        public IReadOnlyCollection<ServiceEntity> Services => _services;

        /// <summary>
        /// Creates new program.
        /// </summary>
        /// <param name="name">The programs's namme.</param>
        /// <param name="description">The programs's description.</param>
        /// <param name="stateId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="programService"></param>
        /// <returns>The new <see cref="ProgramEntity"</returns>
        /// <exception cref="BusinessRuleValidationException">
        /// When program is not single per state (<paramref name="stateId"/> ) per period (<paramref name="startDate"/>, <paramref name="endDate"/>  ).
        /// </exception>
        public static ProgramEntity CreateNew(string name,
                                              string description,
                                              int stateId,
                                              DateTime startDate,
                                              DateTime endDate,
                                              IProgramService programService)
        {
            CheckRule(new ProgramMustBeSinglePerStatePerPeriodRule(programService, stateId, startDate, endDate));

            return new ProgramEntity(name, description, stateId, new Period(startDate, endDate));
        }

        public void UpdateProgram(
            string name,
            string? description)
        {
           // CheckRule(new ServiceNameShouldUniquePerProgram(name, this.Services));

            this.Name = name;
            this.Description = description;
            this.LastModified = DateTime.UtcNow;

            this._domainEvents.Add(new ProgramUpdatedEvent(this));
        }

        public void Cancel()
        {
            IsCanceled = true;
            this.LastModified = DateTime.UtcNow;
            this._domainEvents.Add(new ProgramCanceledEvent(this));
        }

        public bool CanBeDeleted()
        {
            return !_services.Any();
        }

        public ServiceEntity CreateService(string name, string? description)
        {
            CheckRule(new ServiceNameShouldUniquePerProgram(name, this.Services));

            var service = ServiceEntity.CreateNew(name, description, this);

            service.AddDomainEvent(new ServiceCreatedEvent(service));

            return service;
        }
    }
}