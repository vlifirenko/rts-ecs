using NaughtyAttributes;
using UnityEngine;

namespace Rts.View.Scene
{
    public abstract class Container<T> : MonoBehaviour where T : ASceneView
    {
        [SerializeField] private T[] items;

        public T[] Items => items;

        [Button]
        private void UpdateItems() => items = GetComponentsInChildren<T>();
    }
}