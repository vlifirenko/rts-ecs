using Rts.Models.Enums;
using UnityEngine;

namespace Rts.Models.Configs
{
    [CreateAssetMenu]
    public class CommonConfig : ScriptableObject
    {
        public ETeam playerTeam;
        public LayerMask groundLayerMask;
        public LayerMask interactLayerMask;
        public LayerMask hoverLayerMask;
        [Space]
        public ResourceValue[] startResources;
        [Header("Camera")]
        public float cameraSpeed = 10f;
        public float zoomSpeed = 1f;
        public float zoomMin = 30f;
        public float zoomMax = 90f;
    }
}