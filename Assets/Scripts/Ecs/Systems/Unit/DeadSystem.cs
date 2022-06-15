using Leopotam.Ecs;
using Rts.Ecs.Components.Unit;
using UnityEngine;

namespace Rts.Ecs.Systems.Unit
{
    public class DeadSystem : IEcsRunSystem
    {
        private EcsFilter<UnitComponent, DeadComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                var unitView = _filter.GetEntity(index).Get<UnitViewComponent>();
                var config = _filter.GetEntity(index).Get<UnitConfigComponent>().Value;
                ref var dead = ref _filter.Get2(index);

                if (unitView.UnitView.Model.activeSelf)
                    unitView.UnitView.Model.SetActive(false);
                if (unitView.UnitView.Target.activeSelf)
                    unitView.UnitView.Target.SetActive(false);

                if (!dead.IsVfxPlayed)
                {
                    unitView.UnitView.Loot.SetActive(true);
                    // todo vfx factory
                    var instance = Object.Instantiate(config.vfxDestroyPrefab);
                    Object.Destroy(instance, config.vfxDestroyDuration);
                    dead.IsVfxPlayed = true;
                }
            }
        }
    }
}