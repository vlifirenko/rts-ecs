using System;
using UnityEngine;

namespace Rts.View.Scene
{
    public abstract class ASceneView : MonoBehaviour
    {
        private Transform _transform;
        private GameObject _gameObject;
        private Collider _collider;

        public void SetVisible(bool visible) => gameObject.SetActive(visible);

        public bool IsVisible => gameObject.activeSelf;

        public Transform Transform => _transform;
        public Collider Collider => _collider;
        public GameObject GameObject => _gameObject;
        
        protected virtual void Awake()
        {
            _transform = transform;
            _gameObject = gameObject;
            _collider = GetComponent<Collider>();
        }
        
        public virtual void Recycle() {}
    }
}