using UnityEngine;

namespace Rts.View.Ui
{
    public abstract class AUiView : MonoBehaviour
    {
        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}