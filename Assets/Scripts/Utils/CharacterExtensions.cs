using Leopotam.Ecs;
using Rts.Ecs.Components.Character;
using Rts.Ecs.Components.Common;
using Rts.Ecs.Components.Unit;
using Rts.View.Scene.Character;
using UnityEngine.AI;

namespace Rts.Utils
{
    public static class CharacterExtensions
    {
        public static EcsEntity CreateCharacterEntity(this EcsWorld world, ACharacterView view)
        {
            var entity = world.NewEntity();
            view.Entity = entity;

            entity.Get<CharacterComponent>() = new CharacterComponent
            {
                Config = view.Config,
                View = view
            };
            entity.Get<TransformComponent>().Value = view.Transform;
            entity.Get<HealthComponent>() = new HealthComponent
            {
                CurrentValue = view.Config.health,
                MaxValue = view.Config.health
            };
            entity.Get<NavMeshAgentComponent>().Value = view.NavMeshAgent;
            entity.Get<AnimatorComponent>().Value = view.Animator;

            return entity;
        }
    }
}