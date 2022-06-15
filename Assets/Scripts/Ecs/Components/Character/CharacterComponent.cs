using Rts.Models;
using Rts.Models.Configs;
using Rts.View.Scene.Character;

namespace Rts.Ecs.Components.Character
{
    public struct CharacterComponent
    {
        public CharacterConfig Config;
        public ACharacterView View;
        public CharacterStats Stats;
    }
}