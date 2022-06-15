using System;
using Leopotam.Ecs;
using Rts.Ecs.Components.Build;
using Rts.Models;
using Rts.Models.Configs;
using Rts.Services;
using Rts.Services.Control;
using Rts.View.Scene;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Rts.Ecs.Systems.Input
{
    public class MouseRaycastSystem : IEcsRunSystem
    {
        private EcsFilter<SelectBuildPlaceComponent> _filterSelectBuildPlace;
        private ControlService _controlService;
        private SceneData _sceneData;
        private CommonConfig _config;
        
        public void Run()
        {
            HandleHover();

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (UnityEngine.Input.GetMouseButtonDown(0))
                HandleLeftClick();
            if (UnityEngine.Input.GetMouseButtonDown(1))
                HandleRightClick();
        }

        private void HandleHover()
        {
            if (IsBuildingActivate())
                return;

            var ray = _sceneData.mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _config.hoverLayerMask))
            {
                if (hit.transform.TryGetComponent(out IHoverable hoverable))
                    _controlService.AddHover(hoverable);
                else
                    _controlService.RemoveHover();
            }
            else
                _controlService.RemoveHover();
        }

        private void HandleLeftClick()
        {
            var ray = _sceneData.mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
                _controlService.TrySelect(hit.transform);
        }

        private void HandleRightClick()
        {
            var ray = _sceneData.mainCamera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity, _config.interactLayerMask))
                TryInteract(hit);
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, _config.groundLayerMask))
                _controlService.InteractGround(hit.point);
        }

        private void TryInteract(RaycastHit hit)
        {
            if (hit.transform.TryGetComponent(out IInteractable interactable))
                _controlService.InteractObject(interactable);
            else
                throw new Exception("hit hasn't interactable");
        }

        private bool IsBuildingActivate() => _filterSelectBuildPlace.GetEntitiesCount() > 0;
    }
}