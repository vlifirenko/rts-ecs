using UnityEngine;

namespace Rts.Models.Configs
{
    [CreateAssetMenu]
    public class CharacterConfig : ScriptableObject
    {
        public string name;
        public float health = 50;
        public Sprite avatar;
        public float stopDistance = 2;
        public float pickupDistance = 1;
        public float interactDistance = 1;
    }
}