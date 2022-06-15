using System;
using Leopotam.Ecs;
using Rts.Ecs.Components.Resources;
using Rts.Models;
using Rts.Models.Enums;
using Rts.View.Ui;
using UnityEngine;

namespace Rts.Ecs.Systems.Resources
{
    public class UpdateResourceSystem : IEcsRunSystem
    {
        private EcsFilter<UpdateResourceAction> _filter;
        private CanvasView _canvasView;
        private GameData _gameData;

        public void Run()
        {
            foreach (var index in _filter)
            {
                var update = _filter.Get1(index);
                var resourceValue = update.Value;
                var resources = _gameData.Resources;
                var type = resourceValue.resourceType;

                if (update.Operation == EResourceOperation.Set)
                {
                    if (resources.ContainsKey(type))
                        resources.Remove(type);
                    resources.Add(type, resourceValue.value);
                }
                else if (update.Operation == EResourceOperation.Dec)
                {
                    if (!resources.ContainsKey(type))
                        throw new Exception("!resources.ContainsKey(type)");

                    var newValue = resources[type];
                    newValue -= Mathf.Clamp(resourceValue.value, 0, resourceValue.value);
                    resources[type] = newValue;
                }
                else if (update.Operation == EResourceOperation.Inc)
                {
                    if (!resources.ContainsKey(type))
                        throw new Exception("!resources.ContainsKey(type)");

                    resources[type] += resourceValue.value;
                }

                UpdateUi(type, resources[type]);
            }
        }

        private void UpdateUi(EResourceType resourceType, float value)
        {
            foreach (var resourceView in _canvasView.Resources)
            {
                if (resourceView.ResourceType == resourceType)
                {
                    resourceView.ValueText.text = $"Resource X:\n{value}";
                    break;
                }
            }
        }
    }
}