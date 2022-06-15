using Leopotam.Ecs;
using Rts.Models;
using Rts.Models.Configs;
using Rts.Utils;

namespace Rts.Ecs.Systems.Resources
{
    public class InitMinesSystem : IEcsInitSystem
    {
        private CommonConfig _commonConfig;
        private SceneData _sceneData;
        private EcsWorld _world;

        public void Init()
        {
            foreach (var mineView in _sceneData.mineContainer.Items)
                _world.CreateMineEntity(mineView);
        }
    }
}