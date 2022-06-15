using UnityEngine;

namespace Rts.View.Ui.Unit
{
    public class UiHireQueueView : AUiView
    {
        [SerializeField] private UiHireQueueItemView itemPrefab;

        public UiHireQueueItemView ItemPrefab => itemPrefab;
    }
}