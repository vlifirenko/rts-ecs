using Leopotam.Ecs;
using Rts.Ecs.Components.Resources;
using Rts.Ecs.Components.Unit;
using Rts.Ecs.Components.Unit.Command;
using Rts.Utils;
using UnityEngine;

namespace Rts.Ecs.Systems.Resources
{
    public class MiningSystem : IEcsRunSystem
    {
        private EcsFilter<MiningComponent> _filter;
        private EcsWorld _ecsWorld;
        
        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var mine = ref _filter.Get1(index).Mine.Get<MineComponent>();
                var mineView = _filter.Get1(index).Mine.Get<MineViewComponent>().Value;
                var config = _filter.GetEntity(index).Get<UnitConfigComponent>().Value;

                if (mine.CurrentValue > 0f)
                {
                    var mining = Time.deltaTime * config.miningSpeed;
                    if (mine.CurrentValue < mining)
                        mining = mine.CurrentValue;
                    mine.CurrentValue -= mining;

                    mineView.Model.localScale = Vector3.one * (mine.CurrentValue / mine.MaxValue);

                    _ecsWorld.UpdateResource(EResourceOperation.Inc, mine.ResourceType, mining);
                }
                else
                {
                    var command = _filter.GetEntity(index).Get<CommandComponent>().Command;
                    command.Complete();
                }
            }
        }
    }
}