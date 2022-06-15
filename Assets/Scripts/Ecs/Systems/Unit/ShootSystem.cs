using System;
using Leopotam.Ecs;
using Rts.Ecs.Components.Unit;
using Rts.Ecs.Components.Unit.Command;
using UniRx;
using UnityEngine;
using ObservableExtensions = UniRx.ObservableExtensions;
using TimeSpan = System.TimeSpan;

namespace Rts.Ecs.Systems.Unit
{
    public class ShootSystem : IEcsRunSystem, IDisposable
    {
        private EcsFilter<UnitComponent, ShootComponent>.Exclude<DeadComponent> _filter;
        private CompositeDisposable _disposable = new CompositeDisposable();

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var shoot = ref _filter.Get2(index);
                if (shoot.IsReadyToShoot)
                {
                    var targetEntity = _filter.GetEntity(index).Get<CommandComponent>().Command.Target;
                    var config = _filter.GetEntity(index).Get<UnitConfigComponent>().Value;
                    var unitView = _filter.GetEntity(index).Get<UnitViewComponent>().UnitView;

                    var damage = new DamageComponent
                    {
                        Progress = 0f,
                        ProgressMax = config.attackDuration,
                        DamageValue = config.attackDamage
                    };

                    targetEntity.Get<DamageComponent>() = damage;
                    shoot.Cooldown = 0;
                    shoot.IsReadyToShoot = false;

                    ObservableExtensions.Subscribe(
                            Observable.Timer(TimeSpan.FromSeconds(config.vfxShootPreDelay)),
                            _ =>
                            {
                                unitView.ShootVfx.gameObject.SetActive(true);
                                ObservableExtensions.Subscribe(
                                        Observable.Timer(TimeSpan.FromSeconds(config.vfxShootDuration)),
                                        _ => unitView.ShootVfx.gameObject.SetActive(false))
                                    .AddTo(_disposable);
                            })
                        .AddTo(_disposable);
                }
                else
                {
                    shoot.Cooldown += Time.deltaTime;
                    if (shoot.IsPreDelay && shoot.Cooldown >= shoot.PreDelay)
                    {
                        shoot.IsPreDelay = false;
                        shoot.Cooldown = 0;
                        shoot.IsReadyToShoot = true;
                    }
                    else if (!shoot.IsPreDelay && shoot.Cooldown >= shoot.CooldownMax)
                        shoot.IsReadyToShoot = true;
                }
            }
        }

        public void Dispose() => _disposable.Dispose();
    }
}