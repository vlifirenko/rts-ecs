using Leopotam.Ecs;
using Rts.Ecs.Components.Build;
using Rts.Ecs.Components.Character;
using Rts.Ecs.Components.Unit;
using Rts.Ecs.Components.Unit.Action;
using Rts.Services;
using Rts.View.Ui;

namespace Rts.Ecs.Systems.Input.Actions
{
    public class OnRemoveSelectSystem : IEcsRunSystem
    {
        private EcsFilter<RemoveSelectAction> _filter;
        private EcsFilter<SelectedComponent> _filterSelected;
        private CanvasView _canvasView;
        private SelectService _selectService;

        public void Run()
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                if (entity.Has<UnitViewComponent>())
                    _selectService.RemoveSelectUnit(entity);
                else if (entity.Has<StructureViewComponent>()) 
                    _selectService.RemoveSelectStructure(entity);
                else if (entity.Has<CharacterComponent>()) 
                    _selectService.RemoveSelectCharacter(entity);

                if (_filterSelected.GetEntitiesCount() == 0)
                    _canvasView.UISelectView.Hide();
            }
        }
    }
}