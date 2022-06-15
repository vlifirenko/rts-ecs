using Leopotam.Ecs;
using Rts.Ecs.Components.Unit;
using Rts.Models;
using Rts.Models.Configs;
using Rts.Utils;
using UnityEngine.AI;

namespace Rts.Ecs.Systems.Unit
{
    public class InitUnitSystem : IEcsInitSystem
    {
        private CommonConfig _commonConfig;
        private SceneData _sceneData;
        private EcsWorld _world;

        public void Init()
        {
            foreach (var unitView in _sceneData.unitContainer.Items)
                _world.CreateUnitEntity(unitView);
        }
    }
}