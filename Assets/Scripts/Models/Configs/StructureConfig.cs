using System;
using Rts.Models.Enums;
using Rts.View.Scene.Structure;
using UnityEngine;

namespace Rts.Models.Configs
{
    [CreateAssetMenu(menuName = "Config/Structure")]
    public class StructureConfig : ScriptableObject
    {
        public string id;
        public string title;
        public string description;
        public AStructureView prefab;
        public EStructureType type;
        public bool isCanBuild = true;
        [NonReorderable] public ResourceValue[] cost;
        public float health = 100;
        public Sprite avatar;
        public float buildDifficult = 5;
        [NonReorderable] public StructureHireItem[] hireItems;
    }

    [Serializable]
    public class StructureHireItem
    {
        public UnitConfig unitConfig;
        [NonReorderable] public ResourceValue[] cost;
    }
}