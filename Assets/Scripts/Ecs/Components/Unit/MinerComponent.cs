using System.Collections.Generic;
using Rts.Models.Enums;

namespace Rts.Ecs.Components.Unit
{
    public struct MinerComponent
    {
        public Dictionary<EResourceType, float> Resource;
    }
}