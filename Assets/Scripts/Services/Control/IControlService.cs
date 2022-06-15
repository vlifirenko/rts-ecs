using Rts.View.Scene;
using UnityEngine;

namespace Rts.Services.Control
{
    public interface IControlService
    {
        public void AddHover(IHoverable hoverable);
        public void RemoveHover();
        public void TrySelect(Transform transform);
        public void RemoveSelect();
        public void InteractGround(Vector3 point);
        public void InteractObject(IInteractable interactable);
    }
}