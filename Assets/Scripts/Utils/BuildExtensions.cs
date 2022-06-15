using Leopotam.Ecs;
using Rts.Ecs.Components.Build;
using Rts.Ecs.Components.Unit;
using Rts.Models.Configs;
using Rts.View.Scene.Structure;

namespace Rts.Utils
{
    public static class BuildExtensions
    {
        public static EcsEntity CreateStructureEntity(this EcsWorld world, AStructureView view)
        {
            var entity = world.NewEntity();
            view.Entity = entity;

            entity.Get<BuildingComponent>().Type = view.Config.type;
            entity.Get<TeamComponent>().Value = view.Team;
            entity.Get<StructureConfigComponent>().Value = view.Config;
            entity.Get<TransformComponent>().Value = view.Transform;
            entity.Get<StructureViewComponent>().Value = view;
            entity.Get<HealthComponent>() = new HealthComponent
            {
                CurrentValue = view.Config.health,
                MaxValue = view.Config.health
            };

            return entity;
        }

        public static EcsEntity CreateStructureEntity(this EcsWorld world, StructureConfig config, CommonConfig commonConfig)
        {
            var entity = world.NewEntity();

            entity.Get<BuildingComponent>().Type = config.type;
            entity.Get<TeamComponent>().Value = commonConfig.playerTeam;
            entity.Get<StructureConfigComponent>().Value = config;

            entity.Get<HealthComponent>() = new HealthComponent
            {
                CurrentValue = config.health,
                MaxValue = config.health
            };

            return entity;
        }
    }
}