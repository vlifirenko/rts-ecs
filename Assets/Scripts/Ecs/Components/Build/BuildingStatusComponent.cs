namespace Rts.Ecs.Components.Build
{
    public struct BuildingStatusComponent
    {
        public EBuildingStatus Status;
        public float Progress;
    }

    public enum EBuildingStatus
    {
        None = 0,
        InProgress = 10,
        Complete = 20
    }
}