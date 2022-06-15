using System;
using System.Collections.Generic;
using Rts.Models.Enums;
using Rts.View.Scene.Structure;
using UnityEngine.Serialization;

namespace Rts.Models
{
    [Serializable]
    public class GameData
    {
        private Dictionary<EResourceType, float> _resources = new Dictionary<EResourceType, float>();
        private Dictionary<EStructureType, BuildingItem[]> _buildings = new Dictionary<EStructureType, BuildingItem[]>();
        private int _mainBaseLevel;

        public Dictionary<EResourceType, float> Resources
        {
            get => _resources;
            set => _resources = value;
        }

        public Dictionary<EStructureType, BuildingItem[]> Buildings
        {
            get => _buildings;
            set => _buildings = value;
        }

        public int MainBaseLevel
        {
            get => _mainBaseLevel;
            set => _mainBaseLevel = value;
        }
    }

    [Serializable]
    public class BuildingItem
    {
        [FormerlySerializedAs("buildingView")] [FormerlySerializedAs("buildView")] public AStructureView structureView;
    }
}