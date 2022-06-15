using UnityEngine;

namespace Rts.View.Scene.Structure
{
    public abstract class ASpawnStructureView : AStructureView
    {
        [SerializeField] private Transform unitSpawnPoint;
        [SerializeField] private float unitSpawnRadius = 3f;

        public Transform SpawnPoint => unitSpawnPoint;
        public float SpawnRadius => unitSpawnRadius;
    }
}