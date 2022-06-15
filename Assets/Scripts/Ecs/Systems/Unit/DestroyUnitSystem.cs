using Leopotam.Ecs;
using Rts.Ecs.Components.Unit;

namespace Rts.Ecs.Systems.Unit
{
    public class DestroyUnitSystem : IEcsRunSystem
    {
        private EcsFilter<UnitComponent, DestroyComponent> _filter;
        
        public void Run()
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                
                entity.Get<UnitViewComponent>().UnitView.SetVisible(false);
                entity.Destroy();
            }
        }
    }
}