using System.Collections.Generic;
using System.Text;
using Leopotam.Ecs;
using Rts.Ecs.Components.Build;
using Rts.Ecs.Components.Character;
using Rts.Ecs.Components.Unit;
using Rts.Models;
using Rts.Models.Enums;
using Rts.Utils;
using Rts.View.Scene.Structure;
using Rts.View.Ui;
using Rts.View.Ui.Unit;
using UnityEngine;

namespace Rts.Services
{
    public class SelectService
    {
        private readonly CanvasView _canvasView;
        private readonly List<UiHireUnitItemView> _items = new List<UiHireUnitItemView>();
        private readonly SceneData _sceneData;
        private readonly EcsWorld _ecsWorld;

        public SelectService(CanvasView canvasView, SceneData sceneData, EcsWorld ecsWorld)
        {
            _canvasView = canvasView;
            _sceneData = sceneData;
            _ecsWorld = ecsWorld;
        }

        public void SelectUnit(EcsEntity entity)
        {
            var unitView = entity.Get<UnitViewComponent>().UnitView;
            var config = entity.Get<UnitConfigComponent>().Value;

            if (unitView.Selection)
                unitView.Selection.SetActive(true);

            var unitSelectView = _canvasView.UISelectView;
            unitSelectView.UnitName.text = config.unitName;
            unitSelectView.AvatarImage.sprite = config.avatar;
            unitSelectView.LevelText.enabled = true;
            unitSelectView.ExpSlider.gameObject.SetActive(true);
        }

        public void SelectCharacter(EcsEntity entity)
        {
            var character = entity.Get<CharacterComponent>();

            if (character.View.Selection)
                character.View.Selection.SetActive(true);

            var selectView = _canvasView.UISelectView;
            selectView.UnitName.text = character.Config.name;
            selectView.AvatarImage.sprite = character.Config.avatar;
            selectView.LevelText.enabled = true;
            selectView.ExpSlider.gameObject.SetActive(true);
        }

        public void SelectStructure(EcsEntity entity)
        {
            var buildingView = entity.Get<StructureViewComponent>().Value;
            var config = entity.Get<StructureConfigComponent>().Value;

            if (buildingView.Selection)
                buildingView.Selection.SetActive(true);

            var unitSelectView = _canvasView.UISelectView;
            unitSelectView.UnitName.text = config.title;
            unitSelectView.AvatarImage.sprite = config.avatar;
            unitSelectView.LevelText.enabled = false;
            unitSelectView.ExpSlider.gameObject.SetActive(false);

            if (_items.Count > 0)
            {
                foreach (var item in _items)
                {
                    item.Button.onClick.RemoveAllListeners();
                    Object.Destroy(item.gameObject);
                }

                _items.Clear();
            }

            if (buildingView is ASpawnStructureView)
                OnUnitSpawnable(entity);
        }

        public void RemoveSelectUnit(EcsEntity entity)
        {
            var unitView = entity.Get<UnitViewComponent>().UnitView;
            if (unitView.Selection)
                unitView.Selection.SetActive(false);
        }

        public void RemoveSelectStructure(EcsEntity entity)
        {
            var buildingView = entity.Get<StructureViewComponent>().Value;

            if (buildingView.Selection)
                buildingView.Selection.SetActive(false);
            if (buildingView is ASpawnStructureView)
                OnRemoveUnitSpawnable();
        }

        public void RemoveSelectCharacter(EcsEntity entity)
        {
            var view = entity.Get<CharacterComponent>().View;
            if (view.Selection)
                view.Selection.SetActive(false);
        }

        private void OnUnitSpawnable(EcsEntity entity)
        {
            _canvasView.UiBuildButtonView.Hide();

            var hireMenu = _canvasView.UnitHireMenuView;
            var config = entity.Get<StructureConfigComponent>().Value;
            var spawnStructure = entity.Get<StructureViewComponent>().Value as ASpawnStructureView;
            foreach (var item in config.hireItems)
            {
                // todo pool
                var instance = Object.Instantiate(hireMenu.HireList.ItemPrefab, hireMenu.HireList.transform);
                var cost = new StringBuilder();
                cost.Append("<i>Cost:</i>");
                foreach (var resource in item.cost)
                    cost.Append($"\n{resource.resourceType} {resource.value}");

                instance.TitleText.text = item.unitConfig.unitName;
                instance.Button.onClick.AddListener(() => OnHireUnitClick(spawnStructure, item.unitConfig.type));
                instance.CostText.text = cost.ToString();
                instance.Show();

                _items.Add(instance);
            }

            hireMenu.Show();
        }

        private void OnRemoveUnitSpawnable()
        {
            _canvasView.UnitHireMenuView.Hide();
            _canvasView.UiBuildButtonView.Show();

            foreach (var item in _items)
            {
                item.Button.onClick.RemoveAllListeners();
                Object.Destroy(item.gameObject);
            }

            _items.Clear();
        }

        private void OnHireUnitClick(ASpawnStructureView spawnStructure, EUnitType unitType)
        {
            var unitView = _sceneData.unitFactory.Get(unitType);
            var position = spawnStructure.SpawnPoint.position;

            position += new Vector3(
                Random.Range(-spawnStructure.SpawnRadius, spawnStructure.SpawnRadius),
                0f,
                Random.Range(-spawnStructure.SpawnRadius, spawnStructure.SpawnRadius));
            unitView.Transform.position = position;
            _ecsWorld.CreateUnitEntity(unitView);
        }
    }
}