using Leopotam.Ecs;
using Rts.Ecs.Components.Unit.Action;
using Rts.View.Ui;

namespace Rts.Ecs.Systems.Input.Actions
{
    public class OnRemoveHoverActionSystem : IEcsRunSystem
    {
        private EcsFilter<RemoveHoverAction> _filter;
        private CanvasView _canvasView;

        public void Run()
        {
            foreach (var index in _filter)
            {
                _canvasView.UiHoverView.Hide();
            }
        }
    }
}