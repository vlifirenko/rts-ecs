using System;
using System.Collections.Generic;
using System.Text;
using Leopotam.Ecs;
using Rts.Ecs.Components.Build;
using Rts.Models;
using Rts.Models.Configs;
using Rts.Services;
using Rts.View.Ui;
using Rts.View.Ui.Build;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rts.Ecs.Systems.Structure.Building
{
    public class InitBuildingUiSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private CanvasView _canvasView;
        private BuildConfig _buildConfig;
        private EcsWorld _ecsWorld;
        private GameData _gameData;
        private BuildingService _buildingService;

        private readonly List<UiBuildItemView> _buildItemViews = new List<UiBuildItemView>();

        public void Init()
        {
            _canvasView.UiBuildButtonView.Button.onClick.AddListener(OnBuildButton);
            _canvasView.UiBuildMenuView.BackButton.onClick.AddListener(() =>
            {
                _canvasView.UiBuildButtonView.Show();
                _canvasView.UiBuildMenuView.Hide();
            });
            PopulateBuildItems();
        }

        private void PopulateBuildItems()
        {
            foreach (var buildLevel in _buildConfig.unlockedStructures)
            {
                if (_gameData.MainBaseLevel < buildLevel.level)
                    continue;

                foreach (var buildLevelItem in buildLevel.buildItems)
                {
                    // todo to ui pool
                    var instance = Object.Instantiate(_canvasView.UiBuildMenuView.BuildItemPrefab,
                        _canvasView.UiBuildMenuView.transform);

                    instance.Id = buildLevelItem.id;
                    instance.TitleText.text = buildLevelItem.title;
                    instance.Button.onClick.AddListener(() => OnBuildItem(buildLevelItem.id));

                    var sb = new StringBuilder();
                    sb.Append("Cost:");
                    foreach (var resourceValue in buildLevelItem.cost)
                        sb.Append($"\n{resourceValue.resourceType} {resourceValue.value}");
                    instance.CostText.text = sb.ToString();

                    instance.Show();
                    _buildItemViews.Add(instance);
                }
            }
        }

        private void OnBuildButton()
        {
            foreach (var buildItem in _buildItemViews)
            {
                var config = _buildingService.GetConfigById(buildItem.Id);
                var isCostEnough = true;
                foreach (var resourceValue in config.cost)
                {
                    if (_gameData.Resources[resourceValue.resourceType] < resourceValue.value)
                    {
                        isCostEnough = false;
                        break;
                    }
                }

                buildItem.Button.interactable = isCostEnough;
                buildItem.TitleText.color = isCostEnough ? Color.green : Color.red;
            }

            _canvasView.UiBuildButtonView.Hide();
            _canvasView.UiBuildMenuView.Show();
        }

        private void OnBuildItem(string id)
        {
            var config = _buildingService.GetConfigById(id);
            if (config == null)
                throw new Exception("config == null");

            var startBuild = new SelectBuildPlaceAction
            {
                StructureConfig = config
            };
            _ecsWorld.NewEntity().Get<SelectBuildPlaceAction>() = startBuild;
            _canvasView.UiBuildMenuView.Hide();
        }

        public void Destroy()
        {
            _canvasView.UiBuildButtonView.Button.onClick.RemoveAllListeners();
            foreach (var buildItem in _buildItemViews)
                buildItem.Button.onClick.RemoveAllListeners();
        }
    }
}