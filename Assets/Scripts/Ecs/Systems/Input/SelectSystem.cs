using Leopotam.Ecs;
using Rts.Ecs.Components.Build;
using Rts.Ecs.Components.Unit;
using Rts.Models;
using Rts.Models.Configs;
using Rts.View.Ui;

namespace Rts.Ecs.Systems.Input
{
    public class SelectSystem : IEcsRunSystem
    {
        private EcsFilter<SelectedComponent> _filter;
        private CanvasView _canvasView;
        private SceneData _sceneData;
        private CommonConfig _commonConfig;

        public void Run()
        {
            foreach (var index in _filter)
            {
                var health = _filter.GetEntity(index).Get<HealthComponent>();
                _canvasView.UISelectView.HealthSlider.value = health.CurrentValue / health.MaxValue;

                // unit selected
                if (_filter.GetEntity(index).Has<UnitComponent>())
                {
                    var upgrade = _filter.GetEntity(index).Get<UpgradeComponent>();

                    _canvasView.UISelectView.LevelText.text = $"Level: {upgrade.Level}";
                    _canvasView.UISelectView.ExpSlider.value = upgrade.Experience / upgrade.ExperienceMax;
                }
                // building selected
                else if (_filter.GetEntity(index).Has<BuildingComponent>())
                {
                }
            }
        }
    }
}