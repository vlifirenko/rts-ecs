using Leopotam.Ecs;
using Rts.Factory;
using Rts.Factory.Unit;
using Rts.Models.Configs;
using Rts.Models.Enums;
using UnityEngine;

namespace Rts.View.Scene.Unit
{
    [SelectionBase]
    public class UnitView : ASceneView, ISelectable, IHoverable
    {
        [SerializeField] private UnitConfig config;
        [Space] [SerializeField] private ETeam team;
        [SerializeField] private Animator animator;
        [SerializeField] private GameObject selection;
        [SerializeField] private GameObject target;
        [SerializeField] private ParticleSystem shootVfx;
        [SerializeField] private GameObject damageVfx;
        [SerializeField] private GameObject loot;
        [SerializeField] private GameObject model;

        public EcsEntity Entity { get; set; }
        public AUnitFactory OriginFactory { get; set; }

        public ETeam Team => team;
        public UnitConfig Config => config;
        public ParticleSystem ShootVfx => shootVfx;
        public Animator Animator => animator;
        public GameObject DamageVfx => damageVfx;
        public GameObject Target => target;
        public GameObject Loot => loot;
        public GameObject Model => model;
        public GameObject Selection => selection;

        public void Initialize()
        {
        }

        public override void Recycle() => OriginFactory.Reclaim(this);
    }
}