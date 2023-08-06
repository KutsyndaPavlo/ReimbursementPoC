using ReimbursementPoC.Administration.Domain.Common;

namespace ReimbursementPoC.Administration.Domain.Program.Enums
{
    public class StateType : Enumeration
    {
        public static StateType Alabama = new(1, nameof(Alabama));
        public static StateType Alaska = new(2, nameof(Alaska));

        public StateType(int id, string name)
            : base(id, name)
        {
        }
    }
}
