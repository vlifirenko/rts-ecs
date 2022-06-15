using System;
using Leopotam.Ecs;

namespace Rts.Models.Item
{
    [Serializable]
    public class Item
    {
        public string id;
        public string name;
        public EItemType type;
        public EcsEntity? owner;
    }
}