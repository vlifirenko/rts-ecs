using Rts.Models.Configs;
using Rts.Models.Enums;
using UnityEngine;

namespace Rts.Factory.Unit
{
    [CreateAssetMenu(fileName = "General Unit Factory", menuName = "Factories/Unit/General")]
    public class GeneralUnitFactory : AUnitFactory
    {
        [SerializeField] private UnitConfig[] configs;
        
        protected override UnitConfig GetConfig(EUnitType type)
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