using Rts.Models.Enums;
using Rts.View.Scene.Unit;
using UnityEngine;

namespace Rts.Models.Configs
{
    [CreateAssetMenu(menuName = "Config/Unit")]
    public class UnitConfig : ScriptableObject
    {
        public string unitName;
        public UnitView prefab;
        public EUnitType type;
        public HeroConfig heroConfig;
        public float health = 100f;
        public float defence;
        public float experienceMax = 100f;
        public float stopDistance = 0.1f;
        public float pickupDistance = 0.15f;
        public float mineDistance = 0.15f;
        public float buildDistance = 1.5f;
        public LootConfig lootConfig;
        public Sprite avatar;
        [Space]
        public bool canMining;
        public bool canBuild;
        public float miningSpeed = 1f;
        public float buildSpeed = 1f;
        [Header("Attack")]
        public float attackDistance = 5f;
        public float attackDamage = 10;
        public float attackDuration = .5f;
        public float attackPreDelay = 0.1f;
        public float attackDelay = 1f;
        [Header("Vfx")]
        public float damageVfxDuration = 0.5f;
        public float damageVfxPreDelay = 0.25f;
        public float vfxShootPreDelay = 0.1f;
        public float vfxShootDuration = 0.25f;
        public GameObject vfxDestroyPrefab;
        public float vfxDestroyDuration = 2f;
    }
}