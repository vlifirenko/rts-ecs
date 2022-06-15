using Leopotam.Ecs;
using Rts.Ecs.Components.Build;
using Rts.Models;
using Rts.Models.Configs;
using Rts.Services;
using Rts.View.Ui;
using UnityEngine;

namespace Rts.Ecs.Systems.Structure.Building
{
    public class SelectBuildPlaceSystem : IEcsRunSystem
    {
        private EcsFilter<SelectBuildPlaceComponent> _filter;
        private SceneData _sceneData;
        private CommonConfig _config;
        private CanvasView _canvasView;
        private BuildConfig _buildConfig;
        private EcsWorld _ecsWorld;
        private BuildingService _buildingService;

        public void Run()
        {
            foreach (var index in _filter)
            {
                var action = _filter.GetEntity(index);
                if (UnityEngine.Input.GetMouseButtonUp(0))
                {
                    _buildingService.OnBuildAccept(action);
                    return;
                }

                if (UnityEngine.Input.GetMouseButtonUp(1))
                {
                    _buildingService.OnBuildCancel(action);
                    return;
                }

                var build = _filter.Get1(index);
                var ray = _sceneData.mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _config.groundLayerMask))
                    build.HintView.transform.position = hit.point;
                else
                {
                    Debug.LogWarning("cant update build on mouse position");
                    return;
                }

                foreach (var renderer in build.HintView.Renderers)
                {
                    renderer.material = build.HintView.IsTriggerIntercepted
                        ? _buildConfig.buildFailMaterial
                        : _buildConfig.buildSuccessMaterial;
                }
            }
        }
    }
}