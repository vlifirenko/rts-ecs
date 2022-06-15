using Leopotam.Ecs;
using Rts.Ecs.Components.Resources;
using Rts.Ecs.Components.Unit;
using Rts.Ecs.Components.Unit.Command;
using Rts.Models.Configs;
using Rts.Models.Enums;
using Rts.View.Scene;
using UnityEngine;

namespace Rts.Services
{
    public class CommandService
    {
        private readonly CommonConfig _commonConfig;

        public CommandService(CommonConfig commonConfig)
        {
            _commonConfig = commonConfig;
        }

        public static void MoveToPoint(EcsEntity selectedEntity, Vector3 position)
        {
            selectedEntity.Get<CommandStartAction>() = new CommandStartAction
            {
                Command = ECommand.MoveToPoint,
                Position = position
            };
        }

        public void Interact(EcsEntity selectedEntity, IInteractable interactable)
        {
            var interactEntity = interactable.Entity;
            if (interactEntity.Get<TeamComponent>().Value == _commonConfig.playerTeam)
                return;

            if (interactEntity.Has<UnitComponent>())
            {
                selectedEntity.Get<CommandStartAction>() = new CommandStartAction
                {
                    Command = ECommand.MoveToPoint,
                    Target = interactEntity
                };
            }
            else if (interactEntity.Has<MineComponent>())
            {
                selectedEntity.Get<CommandStartAction>() = new CommandStartAction
                {
                    Command = ECommand.Mine,
                    Target = interactEntity
                };
            }
        }

        public static void Build(EcsEntity selectedEntity, EcsEntity buildTarget)
        {
            selectedEntity.Get<CommandStartAction>() = new CommandStartAction
            {
                Command = ECommand.Build,
                Target = buildTarget
            };
        }    
    }
}