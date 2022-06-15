using UnityEngine;

namespace Rts.Models.Configs
{
    [CreateAssetMenu(menuName = "Config/Loot")]
    public class LootConfig : ScriptableObject
    {
        public ResourceValue[] lootItems;
    }
}