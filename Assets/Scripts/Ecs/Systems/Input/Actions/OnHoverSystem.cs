using Leopotam.Ecs;
using Rts.Ecs.Components.Build;
using Rts.Ecs.Components.Unit;
using Rts.Ecs.Components.Unit.Action;
using Rts.View.Ui;

namespace Rts.Ecs.Systems.Input.Actions
{
    public class OnHoverSystem : IEcsRunSystem
    {
        private EcsFilter<HoverAction> _filter;
        private EcsFilter<SelectBuildPlaceComponent> _filterBuild;
        private CanvasView _canvasView;

        public void Run()
        {
            foreach (var index in _filter)
            {
                if (_filterBuild.GetEntitiesCount() > 0)
                    return;
                
                _canvasView.UiHoverView.Show();
                
                var isHasLoot = _filter.GetEntity(index).Has<LootComponent>();
                _canvasView.UiHoverView.DescriptionText.gameObject.SetActive(isHasLoot);
            }
        }
    }
}