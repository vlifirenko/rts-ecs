using Rts.Models;
using Rts.Models.Configs;
using Rts.Models.Enums;

namespace Rts.Ecs.Components.Resources
{
    public struct MineComponent
    {
        public EResourceType ResourceType;
        public float CurrentValue;
        public float MaxValue;
    }
}