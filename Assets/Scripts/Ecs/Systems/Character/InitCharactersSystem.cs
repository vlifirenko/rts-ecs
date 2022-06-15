using Leopotam.Ecs;
using Rts.Models;
using Rts.Models.Configs;
using Rts.Utils;

namespace Rts.Ecs.Systems.Character
{
    public class InitCharactersSystem: IEcsInitSystem
    {
        private CommonConfig _commonConfig;
        private SceneData _sceneData;
        private EcsWorld _world;

        public void Init()
        {
            foreach (var item in _sceneData.characterContainer.Items)
                _world.CreateCharacterEntity(item);
        }
    }
}