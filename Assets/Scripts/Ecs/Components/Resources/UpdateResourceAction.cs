using Rts.Models;

namespace Rts.Ecs.Components.Resources
{
    public struct UpdateResourceAction
    {
        public EResourceOperation Operation;
        public ResourceValue Value;
    }

    public enum EResourceOperation
    {
        Inc,
        Dec,
        Set
    }
}