using Leopotam.Ecs;
using Rts.Ecs.Components.Build;
using Rts.Ecs.Components.Character;
using Rts.Ecs.Components.Unit;
using Rts.Ecs.Components.Unit.Action;
using Rts.Services;
using Rts.View.Ui;

namespace Rts.Ecs.Systems.Input.Actions
{
    public class OnSelectSystem : IEcsRunSystem
    {
        private EcsFilter<SelectAction> _filter;
        private CanvasView _canvasView;
        private SelectService _selectService;

        public void Run()
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);

                entity.Get<SelectedComponent>();

                if (entity.Has<UnitViewComponent>())
                    _selectService.SelectUnit(entity);
                else if (entity.Has<StructureViewComponent>())
                    _selectService.SelectStructure(entity);
                else if (entity.Has<CharacterComponent>())
                    _selectService.SelectCharacter(entity);

                _canvasView.UISelectView.Show();
            }
        }
    }
}