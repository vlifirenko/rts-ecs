using Leopotam.Ecs;
using Rts.Ecs.Components.Unit;
using UnityEngine;

namespace Rts.Ecs.Systems.Unit
{
    public class DamageSystem : IEcsRunSystem
    {
        private EcsFilter<UnitComponent, DamageComponent, HealthComponent>.Exclude<DeadComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var damage = ref _filter.Get2(index);
                var view = _filter.GetEntity(index).Get<UnitViewComponent>();

                // update hp
                if (!damage.IsHealthUpdated)
                {
                    ref var health = ref _filter.Get3(index);
                    health.CurrentValue -= damage.DamageValue;
                    damage.IsHealthUpdated = true;
                    // dead
                    if (health.CurrentValue <= 0)
                    {
                        _filter.GetEntity(index).Get<DeadComponent>();
                        _filter.GetEntity(index).Get<LootComponent>();
                        _filter.GetEntity(index).Del<DamageComponent>();
                        if (view.DamageVfx.activeSelf)
                            view.DamageVfx.SetActive(false);
                        return;
                    }
                }

                damage.Progress += Time.deltaTime;
                if (damage.Progress >= damage.ProgressMax)
                {
                    _filter.GetEntity(index).Del<DamageComponent>();
                    return;
                }

                // vfx
                var configuration = _filter.GetEntity(index).Get<UnitConfigComponent>();
                if (damage.Progress > configuration.Value.damageVfxPreDelay && !damage.IsVfxPlayed)
                {
                    //todo fix
                    view.DamageVfx.GetComponent<ParticleSystem>().Play();
                    damage.IsVfxPlayed = true;
                }
            }
        }
    }
}