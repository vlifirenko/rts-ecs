using Leopotam.Ecs;
using Rts.Ecs.Components.Build;
using Rts.Ecs.Components.Unit;
using Rts.Ecs.Components.Unit.Command;
using Rts.Services;

namespace Rts.Ecs.Systems.Structure.Building
{
    public class SendBuildersSystem : IEcsRunSystem
    {
        private EcsFilter<SendBuildersAction> _filter;
        private EcsFilter<BuilderComponent>.Exclude<CommandComponent> _builderFilter;
        private CommandService _commandService;

        public void Run()
        {
            foreach (var index in _filter)
            {
                var action = _filter.Get1(index);

                foreach (var index2 in _builderFilter)
                {
                    var entity = _builderFilter.GetEntity(index2);
                    CommandService.Build(entity, action.Building);
                }
            }
        }
    }
}