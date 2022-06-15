namespace Rts.Ecs.Components.Unit
{
    public struct ShootComponent
    {
        public bool IsShooting;
        public bool IsReadyToShoot;
        public bool IsPreDelay;
        public float Cooldown;
        public float CooldownMax;
        public float PreDelay;
    }
}