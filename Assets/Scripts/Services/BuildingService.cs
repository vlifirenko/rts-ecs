using System;
using Leopotam.Ecs;
using Rts.Ecs.Components;
using Rts.Ecs.Components.Build;
using Rts.Ecs.Components.Resources;
using Rts.Ecs.Components.Unit;
using Rts.Models;
using Rts.Models.Configs;
using Rts.Utils;
using Rts.View.Ui;
using Rts.View.Ui.Build;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rts.Services
{
    public class BuildingService
    {
        private readonly EcsWorld _ecsWorld;
        private readonly CanvasView _canvasView;
        private readonly BuildConfig _buildConfig;
        private readonly SceneData _sceneData;
        private readonly CommonConfig _commonConfig;

        public BuildingService(EcsWorld ecsWorld, CanvasView canvasView, BuildConfig buildConfig, SceneData sceneData,
            CommonConfig commonConfig)
        {
            _ecsWorld = ecsWorld;
            _canvasView = canvasView;
            _buildConfig = buildConfig;
            _sceneData = sceneData;
            _commonConfig = commonConfig;
        }

        public void OnBuildAccept(EcsEntity buildAction)
        {
            var selectBuildPlace = buildAction.Get<SelectBuildPlaceComponent>();

            foreach (var renderer in selectBuildPlace.HintView.Renderers)
                renderer.material = selectBuildPlace.SavedMaterials[renderer];

            foreach (var resourceValue in selectBuildPlace.Config.cost)
                _ecsWorld.UpdateResource(EResourceOperation.Dec, resourceValue.resourceType, resourceValue.value);

            StartBuilding(selectBuildPlace.Config, selectBuildPlace.HintView.transform.position);

            selectBuildPlace.HintView.Recycle();
            buildAction.Destroy();
            _canvasView.UiBuildButtonView.Show();
        }

        public void OnBuildCancel(EcsEntity buildAction)
        {
            var build = buildAction.Get<SelectBuildPlaceComponent>();
            build.HintView.Recycle();

            buildAction.Destroy();
            _canvasView.UiBuildButtonView.Show();
        }

        private void StartBuilding(StructureConfig config, Vector3 buildPosition)
        {
            var entity = _ecsWorld.CreateStructureEntity(config, _commonConfig);
            var instance = _sceneData.structureFactory.Get(config.type);

            instance.Transform.position = buildPosition;
            instance.Entity = entity;
            entity.Get<StructureViewComponent>().Value = instance;
            entity.Get<TransformComponent>().Value = instance.Transform;
            entity.Get<BuildingStatusComponent>() = new BuildingStatusComponent
            {
                Status = EBuildingStatus.InProgress,
                Progress = 0
            };
            instance.ProgressBuilding.SetActive(true);
            instance.CompleteBuilding.SetActive(false);

            SendBuildersAction(entity);
        }

        private void SendBuildersAction(EcsEntity buildEntity)
        {
            var action = new SendBuildersAction
            {
                Building = buildEntity
            };
            _ecsWorld.NewEntity().Get<SendBuildersAction>() = action;
        }

        public StructureConfig GetConfigById(string id)
        {
            foreach (var buildLevel in _buildConfig.unlockedStructures)
            {
                foreach (var buildItem in buildLevel.buildItems)
                {
                    if (buildItem.id == id)
                        return buildItem;
                }
            }

            throw new Exception("config not found");
        }

        public void OnBuildingComplete(EcsEntity buildingEntity)
        {
            var buildingView = buildingEntity.Get<StructureViewComponent>().Value;
            buildingView.ProgressBuilding.SetActive(false);
            buildingView.CompleteBuilding.SetActive(true);

            _ecsWorld.NewEntity().Get<BuildingCompleteAction>();
        }
    }
}