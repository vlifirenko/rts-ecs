using System;
using System.Collections.Generic;
using Leopotam.Ecs;
using Rts.Ecs.Components.Build;
using Rts.Models;
using Rts.Models.Configs;
using UnityEngine;

namespace Rts.Ecs.Systems.Structure.Building
{
    public class StartSelectBuildPlaceSystem : IEcsRunSystem
    {
        private EcsFilter<SelectBuildPlaceAction> _filter;
        private SceneData _sceneData;
        private CommonConfig _config;
        private BuildConfig _buildConfig;

        public void Run()
        {
            foreach (var index in _filter)
            {
                if (!IsResourcesEnough())
                    return;

                var config = _filter.Get1(index).StructureConfig;
                var ray = _sceneData.mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
                
                if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _config.groundLayerMask))
                {
                    var instance = _sceneData.structureFactory.Get(config.type);
                    instance.Transform.position = hit.point;
                    
                    var entity = _filter.GetEntity(index);
                    var build = new SelectBuildPlaceComponent
                    {
                        Config = config,
                        HintView = instance,
                        SavedMaterials = new Dictionary<Renderer, Material>(),
                    };
                    foreach (var renderer in instance.Renderers)
                        build.SavedMaterials[renderer] = renderer.material;
                    instance.Collider.enabled = false;

                    entity.Get<SelectBuildPlaceComponent>() = build;
                }
                else
                    throw new Exception("cant build on mouse position");
            }
        }

        private bool IsResourcesEnough() => true;
    }
}