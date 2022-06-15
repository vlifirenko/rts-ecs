using Leopotam.Ecs;
using Rts.Models.Enums;

namespace Rts.Ecs.Components.Build
{
    public struct BuildingComponent : IEcsIgnoreInFilter
    {
        public EStructureType Type;
    }
}