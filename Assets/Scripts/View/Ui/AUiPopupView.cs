using UnityEngine;

namespace Rts.View.Ui
{
    public class AUiPopupView : AUiView
    {
        [SerializeField] private Vector3 offset;

        public Vector3 Offset => offset;
    }
}