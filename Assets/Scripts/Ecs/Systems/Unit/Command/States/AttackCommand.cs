using Leopotam.Ecs;
using Rts.Ecs.Components.Common;
using Rts.Ecs.Components.Unit;

namespace Rts.Ecs.Systems.Unit.Command.States
{
    public class AttackCommand : ACommand
    {
        public AttackCommand(EcsEntity entity, float interactDistance, EcsEntity target) : base(entity, interactDistance, target)
        {
        }

        public override void Start()
        {
            base.Start();

            var animator = _entity.Get<AnimatorComponent>().Value;
            if (animator)
                animator.SetInteger("State", 1);
            _entity.Get<NavMeshAgentComponent>().Value.isStopped = false;
        }

        public override void Update()
        {
            base.Update();

            if (Target.Has<DeadComponent>())
            {
                Complete();
                return;
            }

            var unitView = _entity.Get<UnitViewComponent>();
            var animator = _entity.Get<AnimatorComponent>().Value;
            var targetTransform = Target.Get<UnitViewComponent>().UnitView.Transform;
            if (IsInteractDistance())
            {
                // attack
                if (!_entity.Has<ShootComponent>())
                {
                    var config = _entity.Get<UnitConfigComponent>().Value;

                    _entity.Get<ShootComponent>() = new ShootComponent
                    {
                        PreDelay = config.attackPreDelay,
                        CooldownMax = config.attackDelay,
                        IsPreDelay = true
                    };

                    animator.SetInteger("State", 2);
                }

                unitView.UnitView.Transform.LookAt(targetTransform);
                if (!_entity.Get<NavMeshAgentComponent>().Value.isStopped)
                    _entity.Get<NavMeshAgentComponent>().Value.isStopped = true;
            }
            else
            {
                // chase
                if (_entity.Has<ShootComponent>())
                    _entity.Del<ShootComponent>();
                if (animator.GetInteger("State") != 1)
                    animator.SetInteger("State", 1);
                _entity.Get<NavMeshAgentComponent>().Value.destination = targetTransform.position;
                _entity.Get<NavMeshAgentComponent>().Value.isStopped = false;
            }
        }

        public override void Stop()
        {
            base.Stop();

            var animator = _entity.Get<AnimatorComponent>().Value;
            if (animator)
                animator.SetInteger("State", 0);
            _entity.Get<NavMeshAgentComponent>().Value.isStopped = true;
        }
    }
}