using Rts.Models.Configs;
using Rts.Models.Enums;
using Rts.View.Scene.Unit;

namespace Rts.Factory.Unit
{
    public abstract class AUnitFactory : AGameObjectFactory
    {
        public UnitView Get(EUnitType type)
        {
            var config = GetConfig(type);
            var instance = CreateGameObjectInstance(config.prefab);
            instance.OriginFactory = this;
            instance.Initialize();
            return instance;
        }

        protected abstract UnitConfig GetConfig(EUnitType type);

        public void Reclaim(UnitView unitView) => Destroy(unitView.GameObject);
    }
}