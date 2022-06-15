using Leopotam.Ecs;
using Rts.Ecs.Components.Build;
using Rts.Ecs.Components.Common;
using Rts.Ecs.Components.Resources;
using Rts.Ecs.Components.Unit;
using Rts.Services;
using UnityEngine;

namespace Rts.Ecs.Systems.Unit.Command.States
{
    public class InteractCommand : ACommand
    {
        public BuildingService BuildingService { get; set; }

        public InteractCommand(EcsEntity entity, float interactDistance, EcsEntity target) : base(entity, interactDistance, target)
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
                ref var buildingStatus = ref Target.Get<BuildingStatusComponent>();
                var buildConfig = Target.Get<StructureConfigComponent>().Value;
                var config = _entity.Get<UnitConfigComponent>().Value;
                var animator = _entity.Get<AnimatorComponent>().Value;
                
                if (animator)
                    animator.SetInteger("State", 0);

                if (!_entity.Get<NavMeshAgentComponent>().Value.isStopped)
                    _entity.Get<NavMeshAgentComponent>().Value.isStopped = true;

                // todo to build system
                if (buildingStatus.Progress < 1f && buildingStatus.Status == EBuildingStatus.InProgress)
                {
                    var buildProgress = config.buildSpeed / buildConfig.buildDifficult * Time.deltaTime;
                    buildingStatus.Progress += buildProgress;

                    if (buildingStatus.Progress >= 1f)
                    {
                        buildingStatus.Status = EBuildingStatus.Complete;
                        buildingStatus.Progress = 0f;
                        BuildingService.OnBuildingComplete(Target);
                        Complete();
                    }
                }
                else
                {
                    Complete();
                }
            }
            else
                _entity.Get<NavMeshAgentComponent>().Value.destination =
                    _target.Get<StructureViewComponent>().Value.Transform.position;
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