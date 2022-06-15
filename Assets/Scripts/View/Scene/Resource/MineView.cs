using Leopotam.Ecs;
using Rts.Models.Configs;
using UnityEngine;

namespace Rts.View.Scene.Resource
{
    public class MineView : ASceneView, IHoverable, IInteractable
    {
        [SerializeField] private MineConfig config;
        [SerializeField] private Transform model;

        public Transform Model => model;
        public MineConfig Config => config;
        
        public EcsEntity Entity { get; set; }
        
        public override void Recycle()
        {
            throw new System.NotImplementedException();
        }
    }
}