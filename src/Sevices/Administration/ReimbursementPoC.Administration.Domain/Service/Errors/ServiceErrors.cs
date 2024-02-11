using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Domain.Service.Errors
{
    public static class ServiceErrors
    {
        public static Error CanNotBeDeleted(Guid id) => new("Service.CanNotBeDeleted", $"Service with id {id} can't be deleted");

        public static Error ConcurrentUpdate(Guid id) => new("Service.ConcurrentUpdate", $"Service {id} version is outdated.");

        public static Error NotFound(Guid id) => new("Service.NotFound", $"Service with id {id} doesn't exist");
    }
}
