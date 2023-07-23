using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Domain.Service.Rules
{
    public class ServiceNameShouldUniquePerProgram : IBusinessRule
    {
        private readonly string _newServiceName;
        private readonly IEnumerable<ServiceEntity> _existingServices;

        public ServiceNameShouldUniquePerProgram(string newService, IEnumerable<ServiceEntity> existingServices)
        {
            _newServiceName = newService;
            _existingServices = existingServices;
        }

        public bool IsBroken() => _existingServices.Any(x=> x.Name == _newServiceName);

        //public bool IsBroken() => _existingServices.Any(new ServiceNameEqualsSpecification(_newService).ToExpression());

        public string Message => "Service with the same name already exist within the program.";
    }
}
