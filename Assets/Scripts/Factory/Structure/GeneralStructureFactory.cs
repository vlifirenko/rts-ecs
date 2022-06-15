using Rts.Factory.Structure;
using Rts.Models.Configs;
using Rts.Models.Enums;
using UnityEngine;

namespace Rts.Factory
{
    [CreateAssetMenu(fileName = "General Structure Factory", menuName = "Factories/Structure/General")]
    public class GeneralStructureFactory : AStructureFactory
    {
        [SerializeField] private StructureConfig[] configs;
        
        protected override StructureConfig GetConfig(EStructureType type)
        {
            foreach (var config in configs)
            {
                if (config.type == type)
                    return config;
            }
            
            Debug.LogError($"No config for: {type}");
            return configs[0];
        }
    }
}