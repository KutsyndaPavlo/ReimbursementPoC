using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Domain.Program.Errors
{
    public static class ProgramErrors
    {
        public static Error CanNotBeDeleted(Guid id) => new("Program.CanNotBeDeleted", $"Program with id {id} can't be deleted");

        public static Error ConcurrentUpdate(Guid id) => new("Program.ConcurrentUpdate", $"Program {id} version is outdated.");
        
        public static Error NotFound(Guid id) => new("Program.NotFound", $"Program with id {id} doesn't exist");
    }
}
