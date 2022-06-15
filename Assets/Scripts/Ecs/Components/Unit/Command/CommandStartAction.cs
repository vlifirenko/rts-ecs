using Leopotam.Ecs;
using Rts.Models.Enums;
using UnityEngine;

namespace Rts.Ecs.Components.Unit.Command
{
    public struct CommandStartAction
    {
        public ECommand Command;
        public Vector3 Position;
        public EcsEntity Target;
    }
}