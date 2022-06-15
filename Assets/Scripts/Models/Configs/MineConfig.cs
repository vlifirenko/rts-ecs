using Rts.Models.Enums;
using UnityEngine;

namespace Rts.Models.Configs
{
    [CreateAssetMenu(menuName = "Config/Mine")]
    public class MineConfig : ScriptableObject
    {
        public string title;
        public ResourceValue value;
    }
}