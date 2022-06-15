using Leopotam.Ecs;
using Rts.Ecs.Components.Resources;
using Rts.Ecs.Components.Unit;
using Rts.Ecs.Components.Unit.Action;
using Rts.Utils;

namespace Rts.Ecs.Systems.Unit
{
    public class OnPickupSystem : IEcsRunSystem
    {
        private EcsFilter<UnitComponent, PickupActionComponent> _filter;
        private EcsWorld _ecsWorld;

        public void Run()
        {
            foreach (var index in _filter)
            {
                var pickup = _filter.Get2(index);
                foreach (var item in pickup.LootConfig.lootItems)
                    _ecsWorld.UpdateResource(EResourceOperation.Inc, item.resourceType, item.value);
            }
        }
    }
}