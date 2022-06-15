namespace Rts.Ecs.Components.Unit
{
    public struct DamageComponent
    {
        public float DamageValue;
        public float Progress;
        public float ProgressMax;
        public bool IsHealthUpdated;
        public bool IsVfxPlayed;
    }
}