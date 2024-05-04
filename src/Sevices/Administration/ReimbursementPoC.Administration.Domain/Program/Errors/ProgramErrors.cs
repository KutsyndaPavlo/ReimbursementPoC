using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Domain.Program.Errors
{
    public static class ProgramErrors
    {
        public static string CanNotBeDeletedCode = "Program.CanNotBeDeleted";
        public static string ConcurrentUpdateCode = "Program.ConcurrentUpdate";
        public static string NotFoundCode = "Program.ConcurrentUpdate";

        public static Error CanNotBeDeleted(Guid id) => new(CanNotBeDeletedCode, $"Program with id {id} can't be deleted");

        public static Error ConcurrentUpdate(Guid id) => new(ConcurrentUpdateCode, $"Program {id} version is outdated.");
        
        public static Error NotFound(Guid id) => new(NotFoundCode, $"Program with id {id} doesn't exist");
    }
}
