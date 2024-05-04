using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Domain.Service.Errors
{
    public static class ServiceErrors
    {
        public static string CanNotBeDeletedCode = "Service.CanNotBeDeleted";
        public static string ConcurrentUpdateCode = "Service.ConcurrentUpdate";
        public static string NotFoundCode = "Service.CanNotBeDeleted";

        public static Error CanNotBeDeleted(Guid id) => new(CanNotBeDeletedCode, $"Service with id {id} can't be deleted");

        public static Error ConcurrentUpdate(Guid id) => new(ConcurrentUpdateCode, $"Service {id} version is outdated.");

        public static Error NotFound(Guid id) => new(NotFoundCode, $"Service with id {id} doesn't exist");
    }
}
