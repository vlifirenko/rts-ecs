using Leopotam.Ecs;
using Rts.Ecs.Components.Unit.Command;

namespace Rts.Ecs.Systems.Unit.Command
{
    public class CommandUpdateSystem : IEcsRunSystem
    {
        private EcsFilter<CommandComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                var command = _filter.Get1(index).Command;
                command.Update();
            }
        }
    }
}