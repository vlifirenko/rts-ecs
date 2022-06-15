using Leopotam.Ecs;
using Rts.Models.Configs;
using UnityEngine;
using UnityEngine.AI;

namespace Rts.View.Scene.Character
{
    public abstract class ACharacterView : ASceneView, IHoverable
    {
        [SerializeField] private CharacterConfig config;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private GameObject selection;
        [SerializeField] private Animator animator;
        
        public EcsEntity Entity { get; set; }

        public CharacterConfig Config => config;
        public NavMeshAgent NavMeshAgent => navMeshAgent;
        public GameObject Selection => selection;
        public Animator Animator => animator;
    }
}