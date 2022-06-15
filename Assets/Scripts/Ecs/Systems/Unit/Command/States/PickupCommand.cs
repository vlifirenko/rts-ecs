using Leopotam.Ecs;
using Rts.Ecs.Components.Common;
using Rts.Ecs.Components.Unit;
using Rts.Ecs.Components.Unit.Action;
using UnityEngine;

namespace Rts.Ecs.Systems.Unit.Command.States
{
    public class PickupCommand : ACommand
    {
        public PickupCommand(EcsEntity entity, float interactDistance, EcsEntity target) : base(entity, interactDistance,
            target)
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

            if (IsInteractDistance())
            {
                var pickUp = new PickupActionComponent
                {
                    LootConfig = Target.Get<UnitConfigComponent>().Value.lootConfig
                };
                _entity.Get<PickupActionComponent>() = pickUp;
                Target.Get<DestroyComponent>();

                Complete();
            }
            else
                _entity.Get<NavMeshAgentComponent>().Value.destination = _position;
        }

        public override void Stop()
        {
            var animator = _entity.Get<AnimatorComponent>().Value;
            if (animator)
                animator.SetInteger("State", 0);
            _entity.Get<NavMeshAgentComponent>().Value.isStopped = true;
        }
    }
}