using Rts.Models.Configs;
using Rts.Models.Enums;
using Rts.View.Scene.Structure;

namespace Rts.Factory.Structure
{
    public abstract class AStructureFactory : AGameObjectFactory
    {
        public AStructureView Get(EStructureType type)
        {
            var config = GetConfig(type);
            var instance = CreateGameObjectInstance(config.prefab);
            instance.OriginFactory = this;
            instance.Initialize();
            return instance;
        }

        protected abstract StructureConfig GetConfig(EStructureType type);

        public void Reclaim(AStructureView unitView) => Destroy(unitView.GameObject);
    }
}