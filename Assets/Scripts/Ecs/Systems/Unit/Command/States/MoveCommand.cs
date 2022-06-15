using Leopotam.Ecs;
using Rts.Ecs.Components.Common;
using Rts.Ecs.Components.Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Rts.Ecs.Systems.Unit.Command.States
{
    public class MoveCommand : ACommand
    {
        public MoveCommand(EcsEntity entity, float interactDistance, Vector3 position) : base(entity, interactDistance, position)
        {
        }

        public override void Start()
        {
            base.Start();

            var animator = _entity.Get<AnimatorComponent>().Value;
            if (animator)
                animator.SetInteger("State", 1);

            var navMeshAgent = _entity.Get<NavMeshAgentComponent>().Value;
            if (navMeshAgent.isStopped)
                navMeshAgent.isStopped = false;
        }

        public override void Update()
        {
            base.Update();

            if (IsInteractDistance())
                Complete();
            else
                _entity.Get<NavMeshAgentComponent>().Value.SetDestination(_position);
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