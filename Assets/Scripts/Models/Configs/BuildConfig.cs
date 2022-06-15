using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Rts.Models.Configs
{
    [CreateAssetMenu]
    public class BuildConfig : ScriptableObject
    {
        [FormerlySerializedAs("buildLevels")] public UnlockedStructures[] unlockedStructures;
        public Material buildSuccessMaterial;
        public Material buildFailMaterial;
    }

    [Serializable]
    public class UnlockedStructures
    {
        public int level;
        public StructureConfig[] buildItems;
    }
}