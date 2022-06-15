using Leopotam.Ecs;
using NaughtyAttributes;
using Rts.Factory.Structure;
using Rts.Models.Configs;
using Rts.Models.Enums;
using UnityEngine;

namespace Rts.View.Scene.Structure
{
    public abstract class AStructureView : ASceneView, ISelectable, IHoverable
    {
        [SerializeField] private ETeam team;
        [SerializeField] private StructureConfig config;
        [SerializeField] private GameObject selection;
        [SerializeField] private bool isTriggerIntercepted;
        [SerializeField] private Renderer[] renderers;
        [SerializeField] private GameObject progressBuilding;
        [SerializeField] private GameObject completeBuilding;

        public EcsEntity Entity { get; set; }
        public AStructureFactory OriginFactory { get; set; }

        public bool IsTriggerIntercepted => isTriggerIntercepted;
        public Renderer[] Renderers => renderers;
        public ETeam Team => team;
        public StructureConfig Config => config;
        public GameObject Selection => selection;
        public GameObject ProgressBuilding => progressBuilding;
        public GameObject CompleteBuilding => completeBuilding;

        [Button]
        private void UpdateRenderers() => renderers = GetComponentsInChildren<Renderer>(true);

        private void OnTriggerEnter(Collider other) => isTriggerIntercepted = true;

        private void OnTriggerExit(Collider other) => isTriggerIntercepted = false;

        public void Initialize()
        {    
        }

        public override void Recycle() => OriginFactory.Reclaim(this);
    }
}