using Leopotam.Ecs;
using Rts.Ecs.Components.Common;
using Rts.Ecs.Components.Unit;
using Rts.Ecs.Systems.Unit;
using Rts.View.Scene.Unit;
using UnityEngine.AI;

namespace Rts.Utils
{
    public static class UnitExtensions
    {
        public static EcsEntity CreateUnitEntity(this EcsWorld ecsWorld, UnitView unitView)
        {
            var entity = ecsWorld.NewEntity();
            unitView.Entity = entity;

            entity.Get<UnitComponent>();
            entity.Get<TeamComponent>().Value = unitView.Team;
            entity.Get<UnitTypeComponent>().Value = unitView.Config.type;
            entity.Get<TransformComponent>().Value = unitView.transform;
            entity.Get<UnitConfigComponent>().Value = unitView.Config;
            entity.Get<NavMeshAgentComponent>().Value = unitView.GetComponent<NavMeshAgent>();
            entity.Get<AnimatorComponent>().Value = unitView.Animator;

            entity.Get<HealthComponent>() = new HealthComponent
            {
                CurrentValue = unitView.Config.health,
                MaxValue = unitView.Config.health,
            };
            entity.Get<UpgradeComponent>() = new UpgradeComponent
            {
                Experience = 0,
                ExperienceMax = unitView.Config.experienceMax,
                Level = 1
            };
            entity.Get<DefenceComponent>() = new DefenceComponent
            {
                Value = unitView.Config.defence
            };

            entity.Get<UnitViewComponent>().UnitView = unitView;
            entity.Get<UnitViewComponent>().Selection = unitView.Selection;
            entity.Get<UnitViewComponent>().ShootVfx = unitView.ShootVfx;
            entity.Get<UnitViewComponent>().DamageVfx = unitView.DamageVfx;

            if (unitView.Config.canMining)
                entity.Get<MinerComponent>();
            if (unitView.Config.canBuild)
                entity.Get<BuilderComponent>();

            return entity;
        }
    }
}