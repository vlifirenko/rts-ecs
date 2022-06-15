using System;
using Rts.Models.Enums;

namespace Rts.Models
{
    [Serializable]
    public class ResourceValue
    {
        public EResourceType resourceType;
        public float value;

        public ResourceValue(EResourceType resourceType, float value)
        {
            this.resourceType = resourceType;
            this.value = value;
        }
    }
}