using Leopotam.Ecs;
using Rts.Ecs.Components.Resources;
using Rts.Models;
using Rts.Models.Configs;
using Rts.Utils;

namespace Rts.Ecs.Systems.Common
{
    public class InitGameDataSystem : IEcsInitSystem
    {
        private GameData _gameData;
        private EcsWorld _ecsWorld;
        private CommonConfig _commonConfig;

        public void Init()
        {
            foreach (var resourceValue in _commonConfig.startResources)
            {
                _ecsWorld.UpdateResource(EResourceOperation.Set, resourceValue.resourceType, resourceValue.value);
            }
        }
    }
}