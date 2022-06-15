using Leopotam.Ecs;
using Rts.Ecs.Components.Unit.Command;

namespace Rts.Ecs.Systems.Unit.Command
{
    public class CommandStopSystem : IEcsRunSystem
    {
        private EcsFilter<CommandComponent, CommandStopAction> _filter;
        
        public void Run()
        {
            foreach (var index in _filter)
            {
                var command = _filter.Get1(index).Command;
                var entity = _filter.GetEntity(index);
                
                command.Stop();
                entity.Del<CommandComponent>();
            }
        }
    }
}