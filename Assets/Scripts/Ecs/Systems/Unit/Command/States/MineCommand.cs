using Leopotam.Ecs;
using Rts.Ecs.Components.Common;
using Rts.Ecs.Components.Resources;
using Rts.Ecs.Components.Unit;

namespace Rts.Ecs.Systems.Unit.Command.States
{
    public class MineCommand : ACommand
    {
        public MineCommand(EcsEntity entity, float interactDistance, EcsEntity target) : base(entity, interactDistance,
            target)
        {
        }

        public override void Start()
        {
            base.Start();

            var animator = _entity.Get<AnimatorComponent>().Value;
            if (animator)
                animator.SetInteger("State", 1);

            if (_entity.Get<NavMeshAgentComponent>().Value.isStopped)
                _entity.Get<NavMeshAgentComponent>().Value.isStopped = false;
        }

        public override void Update()
        {
            base.Update();

            if (IsInteractDistance())
            {
                var animator = _entity.Get<AnimatorComponent>().Value;
                if (animator)
                    animator.SetInteger("State", 0);
                if (!_entity.Get<NavMeshAgentComponent>().Value.isStopped)
                    _entity.Get<NavMeshAgentComponent>().Value.isStopped = true;

                if (!_entity.Has<MiningComponent>())
                {
                    _entity.Get<MiningComponent>() = new MiningComponent
                    {
                        Mine = Target
                    };
                }
            }
            else
                _entity.Get<NavMeshAgentComponent>().Value.destination = _target.Get<TransformComponent>().Value.position;
        }

        public override void Stop()
        {
            if (_entity.Has<MiningComponent>())
                _entity.Del<MiningComponent>();
            
            var animator = _entity.Get<AnimatorComponent>().Value;
            if (animator)
                animator.SetInteger("State", 0);
            _entity.Get<NavMeshAgentComponent>().Value.isStopped = true;
        }
    }
}