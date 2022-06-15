using System;
using Leopotam.Ecs;
using Rts.Ecs.Components.Unit;
using Rts.Ecs.Components.Unit.Action;
using Rts.Ecs.Components.Unit.Command;
using Rts.Ecs.Systems.Unit.Command.States;
using Rts.Models.Configs;
using Rts.View.Scene;
using Rts.View.Scene.Character;
using Rts.View.Scene.Structure;
using Rts.View.Scene.Unit;
using UnityEngine;

namespace Rts.Services.Control
{
    public class ControlService : IControlService
    {
        private readonly CommandService _commandService;
        private readonly CommonConfig _commonConfig;

        private bool _isHoverEntity;
        private EcsEntity _hoverEntity;
        private bool _isSelectedEntity;
        private EcsEntity _selectedEntity;

        public ControlService(CommandService commandService, CommonConfig commonCommonConfig)
        {
            _commandService = commandService;
            _commonConfig = commonCommonConfig;
        }

        public void AddHover(IHoverable hoverable)
        {
            if (_isHoverEntity && _hoverEntity != hoverable.Entity)
                RemoveHover();

            _hoverEntity = hoverable.Entity;
            _isHoverEntity = true;
            hoverable.Entity.Get<HoverComponent>();
            hoverable.Entity.Get<HoverAction>();
        }

        public void RemoveHover()
        {
            if (!_isHoverEntity)
                return;
            if (_hoverEntity == null)
                throw new Exception("_hoverEntity == null");

            _hoverEntity.Del<HoverComponent>();
            _hoverEntity.Get<RemoveHoverAction>();
            _isHoverEntity = false;
        }

        public void TrySelect(Transform transform)
        {
            // on UnitView click
            if (transform.TryGetComponent(out UnitView unitView))
            {
                if (unitView.Entity.Get<TeamComponent>().Value != _commonConfig.playerTeam)
                    return;
                if (unitView.Entity.Has<DeadComponent>())
                    return;
                if (_isSelectedEntity && _selectedEntity != unitView.Entity)
                    RemoveSelect();

                _selectedEntity = unitView.Entity;
                _isSelectedEntity = true;
                unitView.Entity.Get<SelectAction>();

                if (_selectedEntity.Has<CommandComponent>() &&
                    _selectedEntity.Get<CommandComponent>().Command is AttackCommand)
                {
                    var target = _selectedEntity.Get<CommandComponent>().Command.Target;
                    target.Get<UnitViewComponent>().UnitView.Target.SetActive(true);
                }
            }
            // on ABuildView click
            else if (transform.TryGetComponent(out AStructureView structureView))
            {
                if (structureView.Entity.IsNull())
                    return;
                if (structureView.Entity.Get<TeamComponent>().Value != _commonConfig.playerTeam)
                    return;
                if (structureView.Entity.Has<DeadComponent>())
                    return;
                if (_isSelectedEntity && _selectedEntity != structureView.Entity)
                    RemoveSelect();

                _selectedEntity = structureView.Entity;
                _isSelectedEntity = true;
                structureView.Entity.Get<SelectedComponent>();
                structureView.Entity.Get<SelectAction>();
            }
            else if (transform.TryGetComponent(out ACharacterView characterView))
            {
                if (characterView.Entity.IsNull())
                    return;
                if (_isSelectedEntity && _selectedEntity != characterView.Entity)
                    RemoveSelect();

                _selectedEntity = characterView.Entity;
                _isSelectedEntity = true;
                characterView.Entity.Get<SelectedComponent>();
                characterView.Entity.Get<SelectAction>();
            }
            else if (_isSelectedEntity)
                RemoveSelect();
        }

        public void RemoveSelect()
        {
            if (_selectedEntity == null)
                throw new Exception("_selectedEntity == null");

            if (_selectedEntity.Has<CommandComponent>() &&
                _selectedEntity.Get<CommandComponent>().Command is AttackCommand)
            {
                var target = _selectedEntity.Get<CommandComponent>().Command.Target;
                target.Get<UnitViewComponent>().UnitView.Target.SetActive(false);
            }

            _selectedEntity.Del<SelectedComponent>();
            _selectedEntity.Get<RemoveSelectAction>();
            _isSelectedEntity = false;
        }

        public void InteractGround(Vector3 point)
        {
            if (!_isSelectedEntity)
                return;
            
            if (_selectedEntity.Has<CommandComponent>() &&
                _selectedEntity.Get<CommandComponent>().Command is AttackCommand
                && _selectedEntity.Has<UnitComponent>())
            {
                var target = _selectedEntity.Get<CommandComponent>().Command.Target;
                target.Get<UnitViewComponent>().UnitView.Target.SetActive(false);
            }

            CommandService.MoveToPoint(_selectedEntity, point);
        }

        public void InteractObject(IInteractable interactable)
        {
            if (!_isSelectedEntity)
                return;
            if (interactable.Entity.Get<TeamComponent>().Value == _commonConfig.playerTeam)
                return;

            _commandService.Interact(_selectedEntity, interactable);
        }
    }
}