using Rts.View.Scene.Unit;
using UnityEngine;

namespace Rts.Ecs.Components.Unit
{
    public struct UnitViewComponent
    {
        public UnitView UnitView;
        public GameObject Selection;
        public ParticleSystem ShootVfx;
        public GameObject DamageVfx;
    }
}