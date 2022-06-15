using Leopotam.Ecs;
using Rts.Models;
using Rts.Models.Configs;
using Rts.Utils;

namespace Rts.Ecs.Systems.Structure
{
    public class InitStructuresSystem : IEcsInitSystem
    {
        private CommonConfig _commonConfig;
        private SceneData _sceneData;
        private EcsWorld _world;

        public void Init()
        {
            foreach (var buildView in _sceneData.structuresContainer.Items)
                _world.CreateStructureEntity(buildView);
        }
    }
}