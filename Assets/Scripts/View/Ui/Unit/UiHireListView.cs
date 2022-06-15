using UnityEngine;

namespace Rts.View.Ui.Unit
{
    public class UiHireListView : AUiView
    {
        [SerializeField] private UiHireUnitItemView itemPrefab;

        public UiHireUnitItemView ItemPrefab => itemPrefab;
    }
}