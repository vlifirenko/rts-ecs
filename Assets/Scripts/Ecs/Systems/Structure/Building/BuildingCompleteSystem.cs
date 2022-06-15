using Leopotam.Ecs;
using Rts.Ecs.Components.Build;
using Rts.Ecs.Components.Unit;
using Rts.Ecs.Components.Unit.Command;
using Rts.Services;

namespace Rts.Ecs.Systems.Structure.Building
{
    public class BuildingCompleteSystem : IEcsRunSystem
    {
        private EcsFilter<BuildingCompleteAction> _filter;
        private EcsFilter<CommandComponent, BuilderComponent> _builderFilter;
        private CommandService _commandService;

        public void Run()
        {
            foreach (var index in _filter)
            {
                var action = _filter.Get1(index);

                foreach (var index2 in _builderFilter)
                {
                    if (_builderFilter.Get1(index2).Command.Target == action.Building)
                        _builderFilter.Get1(index2).Command.Complete();
                }
            }
        }
    }
}