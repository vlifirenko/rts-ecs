using System.Text;
using Leopotam.Ecs;
using Rts.Ecs.Components.Build;
using Rts.Ecs.Components.Character;
using Rts.Ecs.Components.Resources;
using Rts.Ecs.Components.Unit;
using Rts.Models;
using Rts.Models.Configs;
using Rts.Utils;
using Rts.View.Ui;
using UnityEngine;

namespace Rts.Ecs.Systems.Input
{
    public class HoverSystem : IEcsRunSystem
    {
        private EcsFilter<HoverComponent, TransformComponent> _filter;
        private EcsFilter<SelectBuildPlaceComponent> _filterBuild;
        private CanvasView _canvasView;
        private SceneData _sceneData;
        private CommonConfig _commonConfig;

        public void Run()
        {
            foreach (var index in _filter)
            {
                if (_filterBuild.GetEntitiesCount() > 0)
                    return;

                var entity = _filter.GetEntity(index);
                var transform = _filter.Get2(index).Value;
                var isPlayer = entity.Get<TeamComponent>().Value == _commonConfig.playerTeam;
                var health = entity.Get<HealthComponent>();
                var rectTransform = _canvasView.UiHoverView.transform as RectTransform;
                var position = _canvasView.Canvas.WorldToCanvasPosition(transform.position, _sceneData.mainCamera);
                rectTransform.anchoredPosition = position + _canvasView.UiHoverView.Offset;

                if (entity.Has<UnitComponent>())
                {
                    var config = entity.Get<UnitConfigComponent>().Value;

                    if (entity.Has<LootComponent>())
                        ShowLoot(entity);
                    else
                    {
                        _canvasView.UiHoverView.NameText.text = config.unitName;
                        _canvasView.UiHoverView.NameText.color = isPlayer ? Color.green : Color.red;
                        if (!_canvasView.UiHoverView.HealthSlider.gameObject.activeSelf)
                            _canvasView.UiHoverView.HealthSlider.gameObject.SetActive(false);
                        _canvasView.UiHoverView.HealthSlider.value = health.CurrentValue / health.MaxValue;
                    }
                }
                else if (entity.Has<BuildingComponent>())
                {
                    var config = entity.Get<StructureConfigComponent>().Value;

                    _canvasView.UiHoverView.NameText.text = config.title;
                    _canvasView.UiHoverView.NameText.color = isPlayer ? Color.green : Color.red;
                    if (!_canvasView.UiHoverView.HealthSlider.gameObject.activeSelf)
                        _canvasView.UiHoverView.HealthSlider.gameObject.SetActive(true);
                    _canvasView.UiHoverView.HealthSlider.value = health.CurrentValue / health.MaxValue;
                }
                else if (entity.Has<MineComponent>())
                {
                    var config = entity.Get<MineConfigComponent>().Value;

                    _canvasView.UiHoverView.NameText.text = config.title;
                    _canvasView.UiHoverView.NameText.color = Color.grey;
                    if (_canvasView.UiHoverView.HealthSlider.gameObject.activeSelf)
                        _canvasView.UiHoverView.HealthSlider.gameObject.SetActive(false);
                } else if (entity.Has<CharacterComponent>())
                {
                    var config = entity.Get<CharacterComponent>().Config;

                    _canvasView.UiHoverView.NameText.text = config.name;
                    _canvasView.UiHoverView.NameText.color = Color.white;
                    if (_canvasView.UiHoverView.HealthSlider.gameObject.activeSelf)
                        _canvasView.UiHoverView.HealthSlider.gameObject.SetActive(false);
                }
            }
        }

        private void ShowLoot(EcsEntity entity)
        {
            var config = entity.Get<UnitConfigComponent>().Value;
            var unitHoverView = _canvasView.UiHoverView;

            var sb = new StringBuilder();
            foreach (var item in config.lootConfig.lootItems)
            {
                if (sb.Length > 0)
                    sb.Append("\n");
                sb.Append($"+{item.value} {item.resourceType}");
            }

            unitHoverView.DescriptionText.text = sb.ToString();
            unitHoverView.NameText.text = "Loot";
        }
    }
}