using System;
using Leopotam.Ecs;
using Rts.Ecs.Components.Character;
using Rts.Ecs.Components.Unit;
using Rts.Ecs.Components.Unit.Command;
using Rts.Ecs.Systems.Unit.Command.States;
using Rts.Models.Enums;
using Rts.Services;

namespace Rts.Ecs.Systems.Unit.Command
{
    public class CommandStartSystem : IEcsRunSystem
    {
        private EcsFilter<CommandStartAction> _filter;
        private EcsWorld _ecsWorld;
        private BuildingService _buildingService;

        public void Run()
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                if (entity.Has<CommandComponent>())
                    entity.Get<CommandComponent>().Command.Stop();

                ACommand command;
                var action = _filter.Get1(index);

                // todo refactoring
                if (entity.Has<UnitConfigComponent>())
                {
                    var config = entity.Get<UnitConfigComponent>().Value;
                    switch (action.Command)
                    {
                        case ECommand.MoveToPoint:
                            command = new MoveCommand(entity, config.stopDistance, action.Position);
                            break;
                        case ECommand.AttackUnit:
                            command = new AttackCommand(entity, config.attackDistance, action.Target);
                            break;
                        case ECommand.PickUp:
                            command = new PickupCommand(entity, config.pickupDistance, action.Target);
                            break;
                        case ECommand.Mine:
                            command = new MineCommand(entity, config.mineDistance, action.Target);
                            break;
                        case ECommand.Build:
                            command = new BuildCommand(entity, config.buildDistance, action.Target);
                            ((BuildCommand) command).BuildingService = _buildingService;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else if (entity.Has<CharacterComponent>())
                {
                    var config = entity.Get<CharacterComponent>().Config;
                    switch (action.Command)
                    {
                        case ECommand.MoveToPoint:
                            command = new MoveCommand(entity, config.stopDistance, action.Position);
                            break;
                        case ECommand.PickUp:
                            command = new PickupCommand(entity, config.pickupDistance, action.Target);
                            break;
                        case ECommand.Interact:
                            command = new InteractCommand(entity, config.interactDistance, action.Target);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                else
                    throw new Exception("entity hasn't config");

                entity.Get<CommandComponent>() = new CommandComponent
                {
                    Command = command
                };
                command.Start();
            }
        }
    }
}