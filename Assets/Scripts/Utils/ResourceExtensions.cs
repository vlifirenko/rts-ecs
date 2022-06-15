using Leopotam.Ecs;
using Rts.Ecs.Components;
using Rts.Ecs.Components.Resources;
using Rts.Ecs.Components.Unit;
using Rts.Models;
using Rts.Models.Enums;
using Rts.View.Scene.Resource;

namespace Rts.Utils
{
    public static class ResourceExtensions
    {
        public static void UpdateResource(this EcsWorld ecsWorld, EResourceOperation operationType,
            EResourceType resourceType, float value)
        {
            ecsWorld.NewEntity().Get<UpdateResourceAction>() = new UpdateResourceAction
            {
                Operation = operationType,
                Value = new ResourceValue(resourceType, value)
            };
        }

        public static EcsEntity CreateMineEntity(this EcsWorld ecsWorld, MineView mineView)
        {
            var entity = ecsWorld.NewEntity();
            entity.Get<MineComponent>() = new MineComponent
            {
                CurrentValue = mineView.Config.value.value,
                MaxValue = mineView.Config.value.value,
                ResourceType = mineView.Config.value.resourceType
            };
            entity.Get<MineConfigComponent>().Value = mineView.Config;
            entity.Get<MineViewComponent>().Value = mineView;
            entity.Get<TransformComponent>().Value = mineView.Transform;

            mineView.Entity = entity;

            return entity;
        }
    }
}